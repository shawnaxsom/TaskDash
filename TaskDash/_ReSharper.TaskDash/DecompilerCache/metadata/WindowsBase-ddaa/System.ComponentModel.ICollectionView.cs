// Type: System.ComponentModel.ICollectionView
// Assembly: WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\WindowsBase.dll

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;

namespace System.ComponentModel
{
    public interface ICollectionView : IEnumerable, INotifyCollectionChanged
    {
        CultureInfo Culture { get; set; }
        IEnumerable SourceCollection { get; }
        Predicate<object> Filter { get; set; }
        bool CanFilter { get; }
        SortDescriptionCollection SortDescriptions { get; }
        bool CanSort { get; }
        bool CanGroup { get; }
        ObservableCollection<GroupDescription> GroupDescriptions { get; }
        ReadOnlyObservableCollection<object> Groups { get; }
        bool IsEmpty { get; }
        object CurrentItem { get; }
        int CurrentPosition { get; }
        bool IsCurrentAfterLast { get; }
        bool IsCurrentBeforeFirst { get; }
        bool Contains(object item);
        void Refresh();
        IDisposable DeferRefresh();
        bool MoveCurrentToFirst();
        bool MoveCurrentToLast();
        bool MoveCurrentToNext();
        bool MoveCurrentToPrevious();
        bool MoveCurrentTo(object item);
        bool MoveCurrentToPosition(int position);
        event CurrentChangingEventHandler CurrentChanging;
        event EventHandler CurrentChanged;
    }
}
