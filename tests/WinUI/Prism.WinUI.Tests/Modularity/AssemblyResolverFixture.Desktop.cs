using Prism.Modularity;
using Xunit;

namespace Prism.WinUI.Tests.Modularity;

public class AssemblyResolverFixture : IDisposable
{
    private const string ModulesDirectory1 = @".\DynamicModules\MocksModulesAssemblyResolve";

    public AssemblyResolverFixture()
    {
        CleanUpDirectories();
    }

    private void CleanUpDirectories()
    {
        CompilerHelper.CleanUpDirectory(ModulesDirectory1);
    }

    [Fact]
    public void ShouldThrowOnInvalidAssemblyFilePath()
    {
        var exceptionThrown = false;
        using (var resolver = new AssemblyResolver())
        {
            try
            {
                resolver.LoadAssemblyFrom(null);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.True(exceptionThrown);


            try
            {
                resolver.LoadAssemblyFrom("file://InexistentFile.dll");
                exceptionThrown = false;
            }
            catch (FileNotFoundException)
            {
                exceptionThrown = true;
            }

            Assert.True(exceptionThrown);


            try
            {
                resolver.LoadAssemblyFrom("InvalidUri.dll");
                exceptionThrown = false;
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.True(exceptionThrown);
        }
    }

    [Fact]
    public void ShouldResolveTypeFromAbsoluteUriToAssembly()
    {
        var assemblyPath = CompilerHelper.GenerateDynamicModule("ModuleInLoadedFromContext1", "Module", ModulesDirectory1 + @"\ModuleInLoadedFromContext1.dll");
        var uriBuilder = new UriBuilder
        {
            Host = string.Empty,
            Scheme = Uri.UriSchemeFile,
            Path = Path.GetFullPath(assemblyPath),
        };
        var assemblyUri = uriBuilder.Uri;
        using (var resolver = new AssemblyResolver())
        {
            var resolvedType =
                Type.GetType(
                    "TestModules.ModuleInLoadedFromContext1Class, ModuleInLoadedFromContext1, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
            Assert.Null(resolvedType);

            resolver.LoadAssemblyFrom(assemblyUri.ToString());

            resolvedType =
                Type.GetType(
                    "TestModules.ModuleInLoadedFromContext1Class, ModuleInLoadedFromContext1, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
            Assert.NotNull(resolvedType);
        }
    }

    [Fact]
    public void ShouldResolvePartialAssemblyName()
    {
        var assemblyPath = CompilerHelper.GenerateDynamicModule("ModuleInLoadedFromContext2", "Module", ModulesDirectory1 + @"\ModuleInLoadedFromContext2.dll");
        var uriBuilder = new UriBuilder
        {
            Host = string.Empty,
            Scheme = Uri.UriSchemeFile,
            Path = Path.GetFullPath(assemblyPath),
        };
        var assemblyUri = uriBuilder.Uri;
        using (var resolver = new AssemblyResolver())
        {
            resolver.LoadAssemblyFrom(assemblyUri.ToString());

            var resolvedType =
                Type.GetType("TestModules.ModuleInLoadedFromContext2Class, ModuleInLoadedFromContext2");

            Assert.NotNull(resolvedType);

            resolvedType =
                Type.GetType("TestModules.ModuleInLoadedFromContext2Class, ModuleInLoadedFromContext2, Version=0.0.0.0");

            Assert.NotNull(resolvedType);

            resolvedType =
                Type.GetType("TestModules.ModuleInLoadedFromContext2Class, ModuleInLoadedFromContext2, Version=0.0.0.0, Culture=neutral");

            Assert.NotNull(resolvedType);
        }
    }

    public void Dispose()
    {
        CleanUpDirectories();
    }
}
