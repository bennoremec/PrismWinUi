using Prism.Ioc;
using Prism.Modularity;

namespace Prism.WinUI.Tests.Mocks.Modules
{
    public class MockModuleA : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            throw new NotImplementedException();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            throw new NotImplementedException();
        }
    }

    public class DummyClass
    {
    }
}
