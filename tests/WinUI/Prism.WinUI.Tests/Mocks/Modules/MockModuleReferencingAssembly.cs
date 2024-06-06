using Prism.Ioc;
using Prism.Modularity;

namespace Prism.WinUI.Tests.Mocks.Modules
{
    public class MockModuleReferencingAssembly : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            MockReferencedModule instance = new MockReferencedModule();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            throw new NotImplementedException();
        }
    }
}
