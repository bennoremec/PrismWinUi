﻿using System;
#if HAS_WINUI
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml;

#else
using System.Windows.Markup;
#endif


namespace Prism.Ioc
{
    /// <summary>
    /// Provides Types and Services registered with the Container
    /// <example>
    /// <para>
    /// Usage as markup extension:
    /// <![CDATA[
    ///   <TextBlock
    ///     Text="{Binding
    ///       Path=Foo,
    ///       Converter={prism:ContainerProvider {x:Type local:MyConverter}}}" />
    /// ]]>
    /// </para>
    /// <para>
    /// Usage as XML element:
    /// <![CDATA[
    ///   <Window>
    ///     <Window.DataContext>
    ///       <prism:ContainerProvider Type="{x:Type local:MyViewModel}" />
    ///     </Window.DataContext>
    ///   </Window>
    /// ]]>
    /// </para>
    /// </example>
    /// </summary>
    public class ContainerProviderExtension : MarkupExtension
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerProviderExtension"/> class.
        /// </summary>
        public ContainerProviderExtension()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerProviderExtension"/> class.
        /// </summary>
        /// <param name="type">The type to Resolve</param>
        public ContainerProviderExtension(Type type)
        {
            Type = type;
        }

        /// <summary>
        /// The type to Resolve
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// The Name used to register the type with the Container
        /// </summary>
        public string Name { get; set; }

#if HAS_WINUI
        /// <summary>
        /// Provide resolved object from <see cref="ContainerLocator"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        protected override object ProvideValue(IXamlServiceProvider serviceProvider)
        {
            return ResolveObject();
        }
#else
        /// <summary>
        /// Provide resolved object from <see cref="ContainerLocator"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return ResolveObject();
        }
#endif
        private object ResolveObject()
        {
            return string.IsNullOrEmpty(Name)
                ? ContainerLocator.Container?.Resolve(Type)
                : ContainerLocator.Container?.Resolve(Type, Name);
        }
    }
}
