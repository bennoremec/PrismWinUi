using Prism.Modularity;

namespace Prism.IocContainer.WinUI.Tests.Support.Mocks;

public class MockModuleInitializer : IModuleInitializer
{
    public bool LoadCalled;

    public void Initialize(IModuleInfo moduleInfo)
    {
        LoadCalled = true;
    }
}
