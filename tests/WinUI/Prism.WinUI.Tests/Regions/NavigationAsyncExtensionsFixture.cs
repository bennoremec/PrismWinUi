using Moq;
using Prism.Regions;
using Xunit;

namespace Prism.WinUI.Tests.Regions;

public class NavigationAsyncExtensionsFixture
{
    [Fact]
    public void WhenNavigatingWithANullThis_ThenThrows()
    {
        INavigateAsync navigate = null;
        var target = "";

        ExceptionAssert.Throws<ArgumentNullException>(
            () => { navigate.RequestNavigate(target); });
    }

    [Fact]
    public void WhenNavigatingWithANullStringTarget_ThenThrows()
    {
        var navigate = new Mock<INavigateAsync>().Object;
        string target = null;

        ExceptionAssert.Throws<ArgumentNullException>(
            () => { navigate.RequestNavigate(target); });
    }

    [Fact]
    public void WhenNavigatingWithARelativeStringTarget_ThenNavigatesToRelativeUri()
    {
        var navigateMock = new Mock<INavigateAsync>();
        navigateMock
            .Setup(nv =>
                nv.RequestNavigate(
                    It.Is<Uri>(u => !u.IsAbsoluteUri && u.OriginalString == "relative"),
                    It.Is<Action<NavigationResult>>(c => c != null)))
            .Verifiable();

        var target = "relative";

        navigateMock.Object.RequestNavigate(target);

        navigateMock.VerifyAll();
    }

    [Fact]
    public void WhenNavigatingWithAnAbsoluteStringTarget_ThenNavigatesToAbsoluteUri()
    {
        var navigateMock = new Mock<INavigateAsync>();
        navigateMock
            .Setup(nv =>
                nv.RequestNavigate(
                    It.Is<Uri>(u => u.IsAbsoluteUri && u.Host == "test" && u.AbsolutePath == "/path"),
                    It.Is<Action<NavigationResult>>(c => c != null)))
            .Verifiable();

        var target = "http://test/path";

        navigateMock.Object.RequestNavigate(target);

        navigateMock.VerifyAll();
    }

    [Fact]
    public void WhenNavigatingWithANullThisAndAUri_ThenThrows()
    {
        INavigateAsync navigate = null;
        var target = new Uri("test", UriKind.Relative);

        ExceptionAssert.Throws<ArgumentNullException>(
            () => { navigate.RequestNavigate(target); });
    }

    [Fact]
    public void WhenNavigatingWithAUri_ThenNavigatesToUriWithCallback()
    {
        var target = new Uri("relative", UriKind.Relative);

        var navigateMock = new Mock<INavigateAsync>();
        navigateMock
            .Setup(nv =>
                nv.RequestNavigate(
                    target,
                    It.Is<Action<NavigationResult>>(c => c != null)))
            .Verifiable();


        navigateMock.Object.RequestNavigate(target);

        navigateMock.VerifyAll();
    }
}
