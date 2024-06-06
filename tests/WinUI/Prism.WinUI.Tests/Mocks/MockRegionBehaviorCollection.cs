using System.Collections;
using Prism.Regions;

namespace Prism.WinUI.Tests.Mocks;

internal class MockRegionBehaviorCollection : Dictionary<string, IRegionBehavior>, IRegionBehaviorCollection
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
