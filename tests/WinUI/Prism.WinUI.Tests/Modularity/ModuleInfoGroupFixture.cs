using Prism.Modularity;
using Xunit;

namespace Prism.WinUI.Tests.Modularity;

public class ModuleInfoGroupFixture
{
    [Fact]
    public void ShouldForwardValuesToModuleInfo()
    {
        var group = new ModuleInfoGroup();
        group.Ref = "MyCustomGroupRef";
        var moduleInfo = new ModuleInfo();
        Assert.Null(moduleInfo.Ref);

        group.Add(moduleInfo);

        Assert.Equal(group.Ref, moduleInfo.Ref);
    }
}
