using Prism.Mvvm;
using Prism.WinUI.Tests.Mocks.ViewModels;
using Prism.WinUI.Tests.Mocks.Views;
using Xunit;

namespace Prism.WinUI.Tests.Mvvm;

public class ViewModelLocatorFixture
{
    [StaFact]
    public void ShouldLocateViewModelWithDefaultSettings()
    {
        ResetViewModelLocationProvider();

        var view = new Mock();
        Assert.Null(view.DataContext);

        ViewModelLocator.SetAutoWireViewModel(view, true);
        Assert.NotNull(view.DataContext);
        Assert.IsType<MockViewModel>(view.DataContext);
    }

    [StaFact]
    public void ShouldLocateViewModelWithDefaultSettingsForViewsThatEndWithView()
    {
        ResetViewModelLocationProvider();

        var view = new MockView();
        Assert.Null(view.DataContext);

        ViewModelLocator.SetAutoWireViewModel(view, true);
        Assert.NotNull(view.DataContext);
        Assert.IsType<MockViewModel>(view.DataContext);
    }

    [StaFact]
    public void ShouldUseCustomDefaultViewModelFactoryWhenSet()
    {
        ResetViewModelLocationProvider();

        var view = new Mock();
        Assert.Null(view.DataContext);

        var mockObject = new object();
        ViewModelLocationProvider.SetDefaultViewModelFactory(viewType => mockObject);

        ViewModelLocator.SetAutoWireViewModel(view, true);
        Assert.NotNull(view.DataContext);
        ReferenceEquals(view.DataContext, mockObject);
    }

    [StaFact]
    public void ShouldUseCustomDefaultViewTypeToViewModelTypeResolverWhenSet()
    {
        ResetViewModelLocationProvider();

        var view = new Mock();
        Assert.Null(view.DataContext);

        ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType => typeof(ViewModelLocatorFixture));

        ViewModelLocator.SetAutoWireViewModel(view, true);
        Assert.NotNull(view.DataContext);
        Assert.IsType<ViewModelLocatorFixture>(view.DataContext);
    }

    [StaFact]
    public void ShouldUseCustomFactoryWhenSet()
    {
        ResetViewModelLocationProvider();

        var view = new Mock();
        Assert.Null(view.DataContext);

        var viewModel = "Test String";
        ViewModelLocationProvider.Register(view.GetType().ToString(), () => viewModel);

        ViewModelLocator.SetAutoWireViewModel(view, true);
        Assert.NotNull(view.DataContext);
        ReferenceEquals(view.DataContext, viewModel);
    }

    internal static void ResetViewModelLocationProvider()
    {
        var staticType = typeof(ViewModelLocationProvider);
        var ci = staticType.TypeInitializer;
        var parameters = new object[0];
        ci.Invoke(null, parameters);
    }
}
