using System;
using Microsoft.UI.Xaml;
using Prism.Ioc;

namespace Prism.Container.Wpf.Mocks;

internal partial class NullLoggerBootstrapper : PrismBootstrapper
{
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        throw new NotImplementedException();
    }

    protected override DependencyObject CreateShell()
    {
        throw new NotImplementedException();
    }
}
