﻿using System.Collections.Specialized;

namespace Prism.WinUI.Tests;

public class CollectionChangedTracker
{
    private readonly List<NotifyCollectionChangedEventArgs> eventList = new List<NotifyCollectionChangedEventArgs>();

    public CollectionChangedTracker(INotifyCollectionChanged collection)
    {
        collection.CollectionChanged += OnCollectionChanged;
    }

    public IEnumerable<NotifyCollectionChangedAction> ActionsFired
    {
        get { return this.eventList.Select(e => e.Action); }
    }

    public IEnumerable<NotifyCollectionChangedEventArgs> NotifyEvents
    {
        get { return this.eventList; }
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        this.eventList.Add(e);
    }
}
