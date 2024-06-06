using Prism.Regions;
using Prism.Regions.Behaviors;
using Prism.WinUI.Tests.Mocks;
using Xunit;

namespace Prism.WinUI.Tests.Regions.Behaviors;

public class SyncRegionContextWithHostBehaviorFixture
{
    [StaFact]
    public void ShouldForwardRegionContextValueToHostControl()
    {
        var region = new MockPresentationRegion();

        var behavior = new SyncRegionContextWithHostBehavior();
        behavior.Region = region;
        DependencyObject mockDependencyObject = new MockDependencyObject();
        behavior.HostControl = mockDependencyObject;

        behavior.Attach();
        Assert.Null(region.Context);
        RegionContext.GetObservableContext(mockDependencyObject).Value = "NewValue";

        Assert.Equal("NewValue", region.Context);
    }

    [StaFact]
    public void ShouldUpdateHostControlRegionContextValueWhenContextOfRegionChanges()
    {
        var region = new MockPresentationRegion();

        var behavior = new SyncRegionContextWithHostBehavior();
        behavior.Region = region;
        DependencyObject mockDependencyObject = new MockDependencyObject();
        behavior.HostControl = mockDependencyObject;

        var observableRegionContext = RegionContext.GetObservableContext(mockDependencyObject);

        behavior.Attach();
        Assert.Null(observableRegionContext.Value);
        region.Context = "NewValue";

        Assert.Equal("NewValue", observableRegionContext.Value);
    }

    [StaFact]
    public void ShouldGetInitialValueFromHostAndSetOnRegion()
    {
        var region = new MockPresentationRegion();

        var behavior = new SyncRegionContextWithHostBehavior();
        behavior.Region = region;
        DependencyObject mockDependencyObject = new MockDependencyObject();
        behavior.HostControl = mockDependencyObject;

        RegionContext.GetObservableContext(mockDependencyObject).Value = "NewValue";

        Assert.Null(region.Context);
        behavior.Attach();
        Assert.Equal("NewValue", region.Context);
    }

    [StaFact]
    public void AttachShouldNotThrowWhenHostControlNull()
    {
        var region = new MockPresentationRegion();

        var behavior = new SyncRegionContextWithHostBehavior();
        behavior.Region = region;
        behavior.Attach();
    }

    [StaFact]
    public void AttachShouldNotThrowWhenHostControlNullAndRegionContextSet()
    {
        var region = new MockPresentationRegion();

        var behavior = new SyncRegionContextWithHostBehavior();
        behavior.Region = region;
        behavior.Attach();
        region.Context = "Changed";
    }

    [StaFact]
    public void ChangingRegionContextObservableObjectValueShouldAlsoChangeRegionContextDependencyProperty()
    {
        var region = new MockPresentationRegion();

        var behavior = new SyncRegionContextWithHostBehavior();
        behavior.Region = region;
        DependencyObject hostControl = new MockDependencyObject();
        behavior.HostControl = hostControl;

        behavior.Attach();

        Assert.Null(RegionManager.GetRegionContext(hostControl));
        RegionContext.GetObservableContext(hostControl).Value = "NewValue";

        Assert.Equal("NewValue", RegionManager.GetRegionContext(hostControl));
    }

    [StaFact]
    public void AttachShouldChangeRegionContextDependencyProperty()
    {
        var region = new MockPresentationRegion();

        var behavior = new SyncRegionContextWithHostBehavior();
        behavior.Region = region;
        DependencyObject hostControl = new MockDependencyObject();
        behavior.HostControl = hostControl;

        RegionContext.GetObservableContext(hostControl).Value = "NewValue";

        Assert.Null(RegionManager.GetRegionContext(hostControl));
        behavior.Attach();
        Assert.Equal("NewValue", RegionManager.GetRegionContext(hostControl));
    }

    [StaFact]
    public void SettingHostControlAfterAttachThrows()
    {
        var ex = Assert.Throws<InvalidOperationException>(() =>
        {
            var behavior = new SyncRegionContextWithHostBehavior();
            DependencyObject hostControl1 = new MockDependencyObject();
            behavior.HostControl = hostControl1;

            behavior.Attach();
            DependencyObject hostControl2 = new MockDependencyObject();
            behavior.HostControl = hostControl2;
        });
    }
}
