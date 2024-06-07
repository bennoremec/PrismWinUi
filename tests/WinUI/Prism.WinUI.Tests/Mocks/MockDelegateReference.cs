using Prism.Events;

namespace Prism.WinUI.Tests.Mocks;

internal class MockDelegateReference : IDelegateReference
{
    public Delegate Target { get; set; }

    public MockDelegateReference()
    {
    }

    public MockDelegateReference(Delegate target)
    {
        Target = target;
    }
}
