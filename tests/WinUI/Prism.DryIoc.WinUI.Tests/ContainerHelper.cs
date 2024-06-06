using DryIoc;
using Prism.Ioc;

namespace Prism.DryIoc.WinUI.Tests;

public static class ContainerHelper
{
    private static Rules CreateContainerRules() => Rules.Default.WithAutoConcreteTypeResolution()
        .With(Made.Of(FactoryMethod.ConstructorWithResolvableArguments))
        .WithDefaultIfAlreadyRegistered(IfAlreadyRegistered.Replace);

    public static IContainer CreateContainer() =>
        new Container(CreateContainerRules());

    public static IContainerExtension CreateContainerExtension() =>
        new DryIocContainerExtension(CreateContainer());

    public static IContainer GetBaseContainer(this IContainerExtension container) =>
        ((IContainerProvider) container).GetContainer();

    public static IContainer GetBaseContainer(this IContainerProvider container) =>
        container.GetContainer();

    public static Type ContainerExtensionType => typeof(DryIocContainerExtension);

    public static Type BaseContainerType => typeof(Container);

    public static Type BaseContainerInterfaceType = typeof(IContainer);

    public static Type RegisteredFrameworkException = typeof(ContainerException);
}
