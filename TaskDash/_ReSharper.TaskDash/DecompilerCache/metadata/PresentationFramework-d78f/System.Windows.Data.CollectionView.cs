// Type: System.Windows.Data.CollectionView
// Assembly: PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\PresentationFramework.dll

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime;
using System.Windows;
using System.Windows.Threading;

namespace System.Windows.Data
{
    public class CollectionView : DispatcherObject, ICollectionView, IEnumerable, INotifyCollectionChanged,
                                  INotifyPropertyChanged
    {
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public CollectionView(IEnumerable collection);

        public virtual int Count { get; }
        public virtual IComparer Comparer { get; }

        public virtual bool NeedsRefresh { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public static object NewItemPlaceholder { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        protected bool IsDynamic { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        protected bool UpdatedOutsideDispatcher { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        protected bool IsRefreshDeferred { get; }
        protected bool IsCurrentInSync { get; }

        #region ICollectionView Members

        public virtual bool Contains(object item);
        public virtual void Refresh();
        public virtual IDisposable DeferRefresh();
        public virtual bool MoveCurrentToFirst();
        public virtual bool MoveCurrentToLast();
        public virtual bool MoveCurrentToNext();
        public virtual bool MoveCurrentToPrevious();
        public virtual bool MoveCurrentTo(object item);
        public virtual bool MoveCurrentToPosition(int position);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        IEnumerator IEnumerable.GetEnumerator();

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        void INotifyCollectionChanged.add_CollectionChanged(NotifyCollectionChangedEventHandler value);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        void INotifyCollectionChanged.remove_CollectionChanged(NotifyCollectionChangedEventHandler value);

        [TypeConverter(typeof (CultureInfoIetfLanguageTagConverter))]
        public virtual CultureInfo Culture { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        public virtual IEnumerable SourceCollection { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public virtual Predicate<object> Filter { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        public virtual bool CanFilter { get; }

        public virtual SortDescriptionCollection SortDescriptions { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public virtual bool CanSort { get; }
        public virtual bool CanGroup { get; }
        public virtual ObservableCollection<GroupDescription> GroupDescriptions { get; }
        public virtual ReadOnlyObservableCollection<object> Groups { get; }
        public virtual object CurrentItem { get; }
        public virtual int CurrentPosition { get; }
        public virtual bool IsCurrentAfterLast { get; }
        public virtual bool IsCurrentBeforeFirst { get; }
        public virtual bool IsEmpty { get; }
        public virtual event CurrentChangingEventHandler CurrentChanging;
        public virtual event EventHandler CurrentChanged;

        #endregion

        #region INotifyPropertyChanged Members

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        void INotifyPropertyChanged.add_PropertyChanged(PropertyChangedEventHandler value);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        void INotifyPropertyChanged.remove_PropertyChanged(PropertyChangedEventHandler value);

        #endregion

        public virtual bool PassesFilter(object item);
        public virtual int IndexOf(object item);
        public virtual object GetItemAt(int index);

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e);
        protected virtual void RefreshOverride();
        protected virtual IEnumerator GetEnumerator();
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args);
        protected void SetCurrent(object newItem, int newPosition);
        protected void SetCurrent(object newItem, int newPosition, int count);
        protected bool OKToChangeCurrent();
        protected void OnCurrentChanging();
        protected virtual void OnCurrentChanging(CurrentChangingEventArgs args);
        protected virtual void OnCurrentChanged();
        protected virtual void ProcessCollectionChanged(NotifyCollectionChangedEventArgs args);
        protected void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args);
        protected virtual void OnBeginChangeLogging(NotifyCollectionChangedEventArgs args);
        protected void ClearChangeLog();
        protected void RefreshOrDefer();

        protected virtual event NotifyCollectionChangedEventHandler CollectionChanged;
        protected virtual event PropertyChangedEventHandler PropertyChanged;
    }
}
