using System.ComponentModel;
using Prism.Regions;

namespace Prism.WinUI.Tests.Mocks;

internal class MockRegion : IRegion
{
    public event PropertyChangedEventHandler PropertyChanged;
    public Func<string, object> GetViewStringDelegate { get; set; }

    private readonly MockViewsCollection views = new MockViewsCollection();

    public IViewsCollection Views
    {
        get { return views; }
    }

    public IViewsCollection ActiveViews
    {
        get { throw new NotImplementedException(); }
    }

    public object Context
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }
    }

    public NavigationParameters NavigationParameters
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }
    }

    public string Name { get; set; }

    public IRegionManager Add(object view)
    {
        this.views.Add(view);
        return null;
    }

    public IRegionManager Add(object view, string viewName)
    {
        return Add(view);
    }

    public IRegionManager Add(object view, string viewName, bool createRegionManagerScope)
    {
        throw new NotImplementedException();
    }

    public void Remove(object view)
    {
        throw new NotImplementedException();
    }

    public void Activate(object view)
    {
        throw new NotImplementedException();
    }

    public void Deactivate(object view)
    {
        throw new NotImplementedException();
    }

    public object GetView(string viewName)
    {
        return GetViewStringDelegate(viewName);
    }

    public IRegionManager RegionManager { get; set; }

    public IRegionBehaviorCollection Behaviors
    {
        get { throw new NotImplementedException(); }
    }

    public bool Navigate(Uri source)
    {
        throw new NotImplementedException();
    }


    public void RequestNavigate(Uri target, Action<NavigationResult> navigationCallback)
    {
        throw new NotImplementedException();
    }

    public void RequestNavigate(Uri target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
    {
        throw new NotImplementedException();
    }

    public void RemoveAll()
    {
        throw new NotImplementedException();
    }

    public IRegionNavigationService NavigationService
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }
    }


    public Comparison<object> SortComparison
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }
    }
}
