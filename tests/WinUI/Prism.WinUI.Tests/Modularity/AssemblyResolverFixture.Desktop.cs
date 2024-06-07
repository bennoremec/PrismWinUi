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

    public void Dispose()
    {
        CleanUpDirectories();
    }
}
