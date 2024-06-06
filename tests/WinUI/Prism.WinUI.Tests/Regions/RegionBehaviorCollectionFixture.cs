using Prism.Regions;
using Prism.WinUI.Tests.Mocks;
using Xunit;

namespace Prism.WinUI.Tests.Regions;

public class RegionBehaviorCollectionFixture
{
    [Fact]
    public void CanAttachRegionBehaviors()
    {
        var behaviorCollection = new RegionBehaviorCollection(new MockPresentationRegion());

        var mock1 = new MockRegionBehavior();
        var mock1Attached = false;
        mock1.OnAttach = () => mock1Attached = true;
        behaviorCollection.Add("Mock1", mock1);

        var mock2 = new MockRegionBehavior();
        var mock2Attached = false;
        mock2.OnAttach = () => mock2Attached = true;
        behaviorCollection.Add("Mock2", mock2);

        Assert.True(mock1Attached);
        Assert.True(mock2Attached);
    }

    [Fact]
    public void ShouldAddRegionWhenAddingBehavior()
    {
        var region = new MockPresentationRegion();
        var behaviorCollection = new RegionBehaviorCollection(region);
        var behavior = new MockRegionBehavior();

        behaviorCollection.Add("Mock", behavior);

        Assert.NotNull(behavior.Region);
        Assert.Same(region, behavior.Region);
    }
}
