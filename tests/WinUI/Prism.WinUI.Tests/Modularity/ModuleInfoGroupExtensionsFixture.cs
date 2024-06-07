using Prism.Ioc;
using Prism.Modularity;
using Xunit;

namespace Prism.WinUI.Tests.Modularity;

/// <summary>
/// Summary description for ModuleInfoGroupExtensionsFixture
/// </summary>
public class ModuleInfoGroupExtensionsFixture
{
    [Fact]
    public void ShouldAddModuleToModuleInfoGroup()
    {
        var moduleName = "MockModule";
        var groupInfo = new ModuleInfoGroup();
        groupInfo.AddModule(moduleName, typeof(MockModule));

        Assert.Single(groupInfo);
        Assert.Equal(moduleName, groupInfo.ElementAt(0).ModuleName);
    }

    [Fact]
    public void ShouldSetModuleTypeCorrectly()
    {
        var groupInfo = new ModuleInfoGroup();
        groupInfo.AddModule("MockModule", typeof(MockModule));

        Assert.Single(groupInfo);
        Assert.Equal(typeof(MockModule).AssemblyQualifiedName, groupInfo.ElementAt(0).ModuleType);
    }

    [Fact]
    public void NullTypeThrows()
    {
        var ex = Assert.Throws<ArgumentNullException>(() =>
        {
            var groupInfo = new ModuleInfoGroup();
            groupInfo.AddModule("NullModule", null);
        });
    }

    [Fact]
    public void ShouldSetDependencies()
    {
        var dependency1 = "ModuleA";
        var dependency2 = "ModuleB";

        var groupInfo = new ModuleInfoGroup();
        groupInfo.AddModule("MockModule", typeof(MockModule), dependency1, dependency2);

        Assert.NotNull(groupInfo.ElementAt(0).DependsOn);
        Assert.Equal(2, groupInfo.ElementAt(0).DependsOn.Count);
        Assert.Contains(dependency1, groupInfo.ElementAt(0).DependsOn);
        Assert.Contains(dependency2, groupInfo.ElementAt(0).DependsOn);
    }

    [Fact]
    public void ShouldUseTypeNameIfNoNameSpecified()
    {
        var groupInfo = new ModuleInfoGroup();
        groupInfo.AddModule(typeof(MockModule));

        Assert.Single(groupInfo);
        Assert.Equal(typeof(MockModule).Name, groupInfo.ElementAt(0).ModuleName);
    }


    public class MockModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
