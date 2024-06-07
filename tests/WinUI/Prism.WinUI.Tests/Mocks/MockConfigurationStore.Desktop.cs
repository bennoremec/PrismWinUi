using Prism.Modularity;

namespace Prism.WinUI.Tests.Mocks;

public class MockConfigurationStore : IConfigurationStore
{
    private readonly ModulesConfigurationSection _section = new ModulesConfigurationSection();

    public ModuleConfigurationElement[] Modules
    {
        set { _section.Modules = new ModuleConfigurationElementCollection(value); }
    }

    public ModulesConfigurationSection RetrieveModuleConfigurationSection()
    {
        return _section;
    }
}
