using Prism.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

#if HAS_UWP
using Windows.UI.Xaml;
#elif HAS_WINUI
using Microsoft.UI.Xaml;
#else
using System.Windows;
#endif

namespace Prism.Regions.Behaviors
{
    /// <summary>
    /// Behavior that creates a new <see cref="IRegion"/>, when the control that will host the <see cref="IRegion"/> (see <see cref="TargetElement"/>)
    /// is added to the VisualTree. This behavior will use the <see cref="RegionAdapterMappings"/> class to find the right type of adapter to create
    /// the region. After the region is created, this behavior will detach.
    /// </summary>
    /// <remarks>
    /// Attached property value inheritance is not available in Silverlight, so the current approach walks up the visual tree when requesting a region from a region manager.
    /// The <see cref="RegionManagerRegistrationBehavior"/> is now responsible for walking up the Tree.
    /// </remarks>
    public class DelayedRegionCreationBehavior
    {
        private readonly RegionAdapterMappings regionAdapterMappings;
        private WeakReference elementWeakReference;
        private bool regionCreated;

        private static ICollection<DelayedRegionCreationBehavior> _instanceTracker = new Collection<DelayedRegionCreationBehavior>();
        private object _trackerLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="DelayedRegionCreationBehavior"/> class.
        /// </summary>
        /// <param name="regionAdapterMappings">
        /// The region adapter mappings, that are used to find the correct adapter for
        /// a given controltype. The controltype is determined by the <see name="TargetElement"/> value.
        /// </param>
        public DelayedRegionCreationBehavior(RegionAdapterMappings regionAdapterMappings)
        {
            this.regionAdapterMappings = regionAdapterMappings;
            this.RegionManagerAccessor = new DefaultRegionManagerAccessor();
        }

        /// <summary>
        /// Sets a class that interfaces between the <see cref="RegionManager"/> 's static properties/events and this behavior,
        /// so this behavior can be tested in isolation.
        /// </summary>
        /// <value>The region manager accessor.</value>
        public IRegionManagerAccessor RegionManagerAccessor { get; set; }

        /// <summary>
        /// The element that will host the Region.
        /// </summary>
        /// <value>The target element.</value>
        public DependencyObject TargetElement
        {
            get { return this.elementWeakReference != null ? this.elementWeakReference.Target as DependencyObject : null; }
            set { this.elementWeakReference = new WeakReference(value); }
        }

        /// <summary>
        /// Start monitoring the <see cref="RegionManager"/> and the <see cref="TargetElement"/> to detect when the <see cref="TargetElement"/> becomes
        /// part of the Visual Tree. When that happens, the Region will be created and the behavior will <see cref="Detach"/>.
        /// </summary>
        public void Attach()
        {
            this.RegionManagerAccessor.UpdatingRegions += this.OnUpdatingRegions;
            this.WireUpTargetElement();
        }

        /// <summary>
        /// Stop monitoring the <see cref="RegionManager"/> and the  <see cref="TargetElement"/>, so that this behavior can be garbage collected.
        /// </summary>
        public void Detach()
        {
            this.RegionManagerAccessor.UpdatingRegions -= this.OnUpdatingRegions;
            this.UnWireTargetElement();
        }

        /// <summary>
        /// Called when the <see cref="RegionManager"/> is updating it's <see cref="RegionManager.Regions"/> collection.
        /// </summary>
        /// <remarks>
        /// This method has to be public, because it has to be callable using weak references in silverlight and other partial trust environments.
        /// </remarks>
        /// <param name="sender">The <see cref="RegionManager"/>. </param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers", Justification = "This has to be public in order to work with weak references in partial trust or Silverlight environments.")]
        public void OnUpdatingRegions(object sender, EventArgs e)
        {
            this.TryCreateRegion();
        }

        private void TryCreateRegion()
        {
            DependencyObject targetElement = this.TargetElement;
            if (targetElement == null)
            {
                this.Detach();
                return;
            }

#if !HAS_WINUI
            if (targetElement.CheckAccess())
#endif
            {
                this.Detach();

                if (!this.regionCreated)
                {
                    string regionName = this.RegionManagerAccessor.GetRegionName(targetElement);
                    CreateRegion(targetElement, regionName);
                    this.regionCreated = true;
                }
            }
        }

        /// <summary>
        /// Method that will create the region, by calling the right <see cref="IRegionAdapter"/>.
        /// </summary>
        /// <param name="targetElement">The target element that will host the <see cref="IRegion"/>.</param>
        /// <param name="regionName">Name of the region.</param>
        /// <returns>The created <see cref="IRegion"/></returns>
        protected virtual IRegion CreateRegion(DependencyObject targetElement, string regionName)
        {
            if (targetElement == null)
                throw new ArgumentNullException(nameof(targetElement));

            try
            {
                // Build the region
                IRegionAdapter regionAdapter = this.regionAdapterMappings.GetMapping(targetElement.GetType());
                IRegion region = regionAdapter.Initialize(targetElement, regionName);

                return region;
            }
            catch (Exception ex)
            {
                throw new RegionCreationException(string.Format(CultureInfo.CurrentCulture, Resources.RegionCreationException, regionName, ex), ex);
            }
        }

        private void ElementLoaded(object sender, RoutedEventArgs e)
        {
            this.UnWireTargetElement();
            this.TryCreateRegion();
        }

        private void WireUpTargetElement()
        {
            FrameworkElement element = this.TargetElement as FrameworkElement;
            if (element != null)
            {
                element.Loaded += this.ElementLoaded;
                return;
            }

#if !HAS_UWP && !HAS_WINUI
            FrameworkContentElement fcElement = this.TargetElement as FrameworkContentElement;
            if (fcElement != null)
            {
                fcElement.Loaded += this.ElementLoaded;
                return;
            }
#endif

            //if the element is a dependency object, and not a FrameworkElement, nothing is holding onto the reference after the DelayedRegionCreationBehavior
            //is instantiated inside RegionManager.CreateRegion(DependencyObject element). If the GC runs before RegionManager.UpdateRegions is called, the region will
            //never get registered because it is gone from the updatingRegionsListeners list inside RegionManager. So we need to hold on to it. This should be rare.
            DependencyObject depObj = this.TargetElement as DependencyObject;
            if (depObj != null)
            {
                Track();
                return;
            }
        }

        private void UnWireTargetElement()
        {
            FrameworkElement element = this.TargetElement as FrameworkElement;
            if (element != null)
            {
                element.Loaded -= this.ElementLoaded;
                return;
            }

#if !HAS_UWP && !HAS_WINUI
            FrameworkContentElement fcElement = this.TargetElement as FrameworkContentElement;
            if (fcElement != null)
            {
                fcElement.Loaded -= this.ElementLoaded;
                return;
            }
#endif

            DependencyObject depObj = this.TargetElement as DependencyObject;
            if (depObj != null)
            {
                Untrack();
                return;
            }
        }


        /// <summary>
        /// Add the instance of this class to <see cref="_instanceTracker"/> to keep it alive
        /// </summary>
        private void Track()
        {
            lock (_trackerLock)
            {
                if (!_instanceTracker.Contains(this))
                {
                    _instanceTracker.Add(this);
                }
            }
        }

        /// <summary>
        /// Remove the instance of this class from <see cref="_instanceTracker"/>
        /// so it can eventually be garbage collected
        /// </summary>
        private void Untrack()
        {
            lock (_trackerLock)
            {
                _instanceTracker.Remove(this);
            }
        }
    }
}
