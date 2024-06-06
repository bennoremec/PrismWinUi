using Microsoft.UI.Xaml.Controls;
using Prism.Regions;
using Prism.Regions.Behaviors;
using Xunit;

namespace Prism.WinUI.Tests.Regions;

public class SelectorRegionAdapterFixture
{
    [StaFact]
    public void AdapterAddsSelectorItemsSourceSyncBehavior()
    {
        var control = new ListBox();
        IRegionAdapter adapter = new TestableSelectorRegionAdapter();

        var region = adapter.Initialize(control, "Region1");
        Assert.NotNull(region);

        Assert.IsType<SelectorItemsSourceSyncBehavior>(region.Behaviors["SelectorItemsSourceSyncBehavior"]);
    }


    [StaFact]
    public void AdapterDoesNotPreventRegionFromBeingGarbageCollected()
    {
        var selector = new ListBox();
        var model = new object();
        IRegionAdapter adapter = new SelectorRegionAdapter(null);

        var region = adapter.Initialize(selector, "Region1");
        region.Add(model);

        var regionWeakReference = new WeakReference(region);
        var controlWeakReference = new WeakReference(selector);
        Assert.True(regionWeakReference.IsAlive);
        Assert.True(controlWeakReference.IsAlive);

        region = null;
        selector = null;
        GC.Collect();
        GC.Collect();

        Assert.False(regionWeakReference.IsAlive);
        Assert.False(controlWeakReference.IsAlive);
    }

    [StaFact]
    public void ActivatingTheViewShouldUpdateTheSelectedItem()
    {
        var selector = new ListBox();
        var view1 = new object();
        var view2 = new object();

        IRegionAdapter adapter = new SelectorRegionAdapter(null);

        var region = adapter.Initialize(selector, "Region1");
        region.Add(view1);
        region.Add(view2);

        Assert.NotEqual(view1, selector.SelectedItem);

        region.Activate(view1);

        Assert.Equal(view1, selector.SelectedItem);

        region.Activate(view2);

        Assert.Equal(view2, selector.SelectedItem);
    }

    [StaFact]
    public void DeactivatingTheSelectedViewShouldUpdateTheSelectedItem()
    {
        var selector = new ListBox();
        var view1 = new object();
        IRegionAdapter adapter = new SelectorRegionAdapter(null);
        var region = adapter.Initialize(selector, "Region1");
        region.Add(view1);

        region.Activate(view1);

        Assert.Equal(view1, selector.SelectedItem);

        region.Deactivate(view1);

        Assert.NotEqual(view1, selector.SelectedItem);
    }


    private class TestableSelectorRegionAdapter : SelectorRegionAdapter
    {
        public TestableSelectorRegionAdapter()
            : base(null)
        {
        }


        protected override IRegion CreateRegion()
        {
            return new Region();
        }
    }
}
