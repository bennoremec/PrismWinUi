using Prism.Regions;

namespace Prism.WinUI.Tests.Mocks;

public class MockRegionBehavior : IRegionBehavior
{
    public IRegion Region { get; set; }

    public Func<object> OnAttach;

    public void Attach()
    {
        if (OnAttach != null)
            OnAttach();
    }
}
