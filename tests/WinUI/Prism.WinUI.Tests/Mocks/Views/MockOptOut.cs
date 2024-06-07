using Prism.Mvvm;

namespace Prism.WinUI.Tests.Mocks.Views;

public class MockOptOut : FrameworkElement
{
    public MockOptOut()
    {
        ViewModelLocator.SetAutoWireViewModel(this, false);
    }
}
