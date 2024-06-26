using System.Collections.ObjectModel;
using Xunit;

namespace Prism.WinUI.Tests;

public class CollectionExtensionsFixture
{
    [Fact]
    public void CanAddRangeToCollection()
    {
        var col = new Collection<object>();
        var itemsToAdd = new List<object> { "1", "2" };

        col.AddRange(itemsToAdd);

        Assert.Equal(2, col.Count);
        Assert.Equal("1", col[0]);
        Assert.Equal("2", col[1]);
    }
}
