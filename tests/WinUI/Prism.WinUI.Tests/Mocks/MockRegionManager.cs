using System.Collections;
using System.Collections.Specialized;
using Prism.Regions;

namespace Prism.WinUI.Tests.Mocks;

internal class MockRegionManager : IRegionManager
{
    internal MockRegionCollection MockRegionCollection
    {
        get { return Regions as MockRegionCollection; }
    }

    public IRegionCollection Regions { get; } = new MockRegionCollection();

    public IRegionManager CreateRegionManager()
    {
        throw new NotImplementedException();
    }

    public IRegionManager AddToRegion(string regionName, object view)
    {
        throw new NotImplementedException();
    }

    public IRegionManager RegisterViewWithRegion(string regionName, Type viewType)
    {
        throw new NotImplementedException();
    }

    public IRegionManager RegisterViewWithRegion(string regionName, Func<object> getContentDelegate)
    {
        throw new NotImplementedException();
    }

    public void RequestNavigate(string regionName, Uri source, Action<NavigationResult> navigationCallback)
    {
        throw new NotImplementedException();
    }

    public void RequestNavigate(string regionName, Uri source)
    {
        throw new NotImplementedException();
    }

    public void RequestNavigate(string regionName, string source, Action<NavigationResult> navigationCallback)
    {
        throw new NotImplementedException();
    }

    public void RequestNavigate(string regionName, string source)
    {
        throw new NotImplementedException();
    }

    public void RequestNavigate(string regionName, Uri target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
    {
        throw new NotImplementedException();
    }

    public void RequestNavigate(string regionName, string target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
    {
        throw new NotImplementedException();
    }

    public void RequestNavigate(string regionName, Uri target, NavigationParameters navigationParameters)
    {
        throw new NotImplementedException();
    }

    public void RequestNavigate(string regionName, string target, NavigationParameters navigationParameters)
    {
        throw new NotImplementedException();
    }

    public bool Navigate(Uri source)
    {
        throw new NotImplementedException();
    }

    public IRegionManager AddToRegion(string regionName, string viewName)
    {
        throw new NotImplementedException();
    }

    public IRegionManager RegisterViewWithRegion(string regionName, string viewName)
    {
        throw new NotImplementedException();
    }
}

internal class MockRegionCollection : List<IRegion>, IRegionCollection
{
    IEnumerator<IRegion> IEnumerable<IRegion>.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IRegion this[string regionName]
    {
        get { return this[0]; }
    }

    void IRegionCollection.Add(IRegion region)
    {
        this.Add(region);
    }

    public bool Remove(string regionName)
    {
        throw new NotImplementedException();
    }

    public bool ContainsRegionWithName(string regionName)
    {
        return true;
    }

    public void Add(string regionName, IRegion region)
    {
        throw new NotImplementedException();
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;
}
