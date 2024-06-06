using Prism.Common;
using Xunit;

namespace Prism.WinUI.Tests;

public class ListDictionaryFixture
{
    private static ListDictionary<string, object> list;

    public ListDictionaryFixture()
    {
        list = new ListDictionary<string, object>();
    }

    [Fact]
    public void AddThrowsIfKeyNull()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => { list.Add(null, new object()); });
    }

    [Fact]
    public void AddThrowsIfValueNull()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => { list.Add("", null); });
    }

    [Fact]
    public void CanAddValue()
    {
        var value1 = new object();
        var value2 = new object();

        list.Add("foo", value1);
        list.Add("foo", value2);

        Assert.Equal(2, list["foo"].Count);
        Assert.Same(value1, list["foo"][0]);
        Assert.Same(value2, list["foo"][1]);
    }

    [Fact]
    public void CanIndexValuesByKey()
    {
        list.Add("foo", new object());
        list.Add("foo", new object());

        Assert.Equal(2, list["foo"].Count);
    }

    [Fact]
    public void ThrowsIfRemoveKeyNull()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => { list.RemoveValue(null, new object()); });
    }

    [Fact]
    public void CanRemoveValue()
    {
        var value = new object();

        list.Add("foo", value);
        list.RemoveValue("foo", value);

        Assert.Equal(0, list["foo"].Count);
    }

    [Fact]
    public void CanRemoveValueFromAllLists()
    {
        var value = new object();
        list.Add("foo", value);
        list.Add("bar", value);

        list.RemoveValue(value);

        Assert.Equal(0, list.Values.Count);
    }

    [Fact]
    public void RemoveNonExistingValueNoOp()
    {
        list.Add("foo", new object());

        list.RemoveValue("foo", new object());
    }

    [Fact]
    public void RemoveNonExistingKeyNoOp()
    {
        list.RemoveValue("foo", new object());
    }

    [Fact]
    public void ThrowsIfRemoveListKeyNull()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => { list.Remove(null); });
    }

    [Fact]
    public void CanRemoveList()
    {
        list.Add("foo", new object());
        list.Add("foo", new object());

        var removed = list.Remove("foo");

        Assert.True(removed);
        Assert.Equal(0, list.Keys.Count);
    }

    [Fact]
    public void CanSetList()
    {
        var values = new List<object>();
        values.Add(new object());
        list.Add("foo", new object());
        list.Add("foo", new object());

        list["foo"] = values;

        Assert.Equal(1, list["foo"].Count);
    }

    [Fact]
    public void CanEnumerateKeyValueList()
    {
        var count = 0;
        list.Add("foo", new object());
        list.Add("foo", new object());

        foreach (var pair in list)
        {
            foreach (var value in pair.Value)
            {
                count++;
            }

            Assert.Equal("foo", pair.Key);
        }

        Assert.Equal(2, count);
    }

    [Fact]
    public void CanGetFlatListOfValues()
    {
        list.Add("foo", new object());
        list.Add("foo", new object());
        list.Add("bar", new object());

        var values = list.Values;

        Assert.Equal(3, values.Count);
    }

    [Fact]
    public void IndexerAccessAlwaysSucceeds()
    {
        var values = list["foo"];

        Assert.NotNull(values);
    }

    [Fact]
    public void ThrowsIfContainsKeyNull()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => { list.ContainsKey(null); });
    }

    [Fact]
    public void CanAskContainsKey()
    {
        Assert.False(list.ContainsKey("foo"));
    }

    [Fact]
    public void CanAskContainsValueInAnyList()
    {
        var obj = new object();
        list.Add("foo", new object());
        list.Add("bar", new object());
        list.Add("baz", obj);

        var contains = list.ContainsValue(obj);

        Assert.True(contains);
    }

    [Fact]
    public void CanClearDictionary()
    {
        list.Add("foo", new object());
        list.Add("bar", new object());
        list.Add("baz", new object());

        list.Clear();

        Assert.Empty(list);
    }

    [Fact]
    public void CanGetFilteredValuesByKeys()
    {
        list.Add("foo", new object());
        list.Add("bar", new object());
        list.Add("baz", new object());

        var filtered = list.FindAllValuesByKey(delegate(string key) { return key.StartsWith("b"); });

        var count = 0;
        foreach (var obj in filtered)
        {
            count++;
        }

        Assert.Equal(2, count);
    }

    [Fact]
    public void CanGetFilteredValues()
    {
        list.Add("foo", DateTime.Now);
        list.Add("bar", new object());
        list.Add("baz", DateTime.Today);

        var filtered = list.FindAllValues(delegate(object value) { return value is DateTime; });
        var count = 0;
        foreach (var obj in filtered)
        {
            count++;
        }

        Assert.Equal(2, count);
    }
}
