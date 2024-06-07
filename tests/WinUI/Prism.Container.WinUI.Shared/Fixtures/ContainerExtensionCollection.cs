using Xunit;

namespace Prism.DryIoc.WinUI.Tests.Fixtures;

public class ContainerExtension
{
}

[CollectionDefinition(nameof(ContainerExtension), DisableParallelization = true)]
public class ContainerExtensionCollection : ICollectionFixture<ContainerExtension>
{
}
