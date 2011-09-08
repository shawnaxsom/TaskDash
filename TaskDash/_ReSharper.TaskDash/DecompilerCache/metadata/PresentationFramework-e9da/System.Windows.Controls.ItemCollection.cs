// Type: System.Windows.Controls.ItemCollection
// Assembly: PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\Profile\Client\PresentationFramework.dll

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime;
using System.Windows;
using System.Windows.Data;

namespace System.Windows.Controls
{
    [Localizability(LocalizationCategory.Ignore)]
    public sealed class ItemCollection : CollectionView, IList, ICollection, IEnumerable,
                                         IEditableCollectionViewAddNewItem, IEditableCollectionView, IItemProperties,
                                         IWeakEventListener
    {
        static ItemCollection();
        internal ItemCollection(DependencyObject modelParent);
        internal ItemCollection(FrameworkElement modelParent, int capacity);
        public override bool IsEmpty { get; }
        public override IEnumerable SourceCollection { get; }
        public override bool NeedsRefresh { get; }
        public override SortDescriptionCollection SortDescriptions { get; }
        public override bool CanSort { get; }
        public override Predicate<object> Filter { get; set; }
        public override bool CanFilter { get; }
        public override bool CanGroup { get; }
        public override ObservableCollection<GroupDescription> GroupDescriptions { get; }
        public override ReadOnlyObservableCollection<object> Groups { get; }

        public override int CurrentPosition { get; }
        public override object CurrentItem { get; }
        public override bool IsCurrentAfterLast { get; }
        public override bool IsCurrentBeforeFirst { get; }
        internal DependencyObject ModelParent { get; }
        internal FrameworkElement ModelParentFE { get; }

        internal IEnumerable ItemsSource { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        internal bool IsUsingItemsSource { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        internal CollectionView CollectionView { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        internal IEnumerator LogicalChildren { get; }

        #region IEditableCollectionViewAddNewItem Members

        object IEditableCollectionView.AddNew();
        void IEditableCollectionView.CommitNew();
        void IEditableCollectionView.CancelNew();
        void IEditableCollectionView.RemoveAt(int index);
        void IEditableCollectionView.Remove(object item);
        void IEditableCollectionView.EditItem(object item);
        void IEditableCollectionView.CommitEdit();
        void IEditableCollectionView.CancelEdit();
        object IEditableCollectionViewAddNewItem.AddNewItem(object newItem);
        NewItemPlaceholderPosition IEditableCollectionView.NewItemPlaceholderPosition { get; set; }
        bool IEditableCollectionView.CanAddNew { get; }
        bool IEditableCollectionView.IsAddingNew { get; }
        object IEditableCollectionView.CurrentAddItem { get; }
        bool IEditableCollectionView.CanRemove { get; }
        bool IEditableCollectionView.CanCancelEdit { get; }
        bool IEditableCollectionView.IsEditingItem { get; }
        object IEditableCollectionView.CurrentEditItem { get; }
        bool IEditableCollectionViewAddNewItem.CanAddNewItem { get; }

        #endregion

        #region IItemProperties Members

        ReadOnlyCollection<ItemPropertyInfo> IItemProperties.ItemProperties { get; }

        #endregion

        #region IList Members

        public int Add(object newItem);
        public void Clear();
        public override bool Contains(object containItem);
        public void CopyTo(Array array, int index);
        public override int IndexOf(object item);
        public void Insert(int insertIndex, object insertItem);
        public void Remove(object removeItem);
        public void RemoveAt(int removeIndex);
        public override int Count { get; }
        public object this[int index] { get; set; }
        bool ICollection.IsSynchronized { get; }
        object ICollection.SyncRoot { get; }

        bool IList.IsFixedSize { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        bool IList.IsReadOnly { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        #endregion

        #region IWeakEventListener Members

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e);

        #endregion

        public override bool MoveCurrentToFirst();
        public override bool MoveCurrentToNext();
        public override bool MoveCurrentToPrevious();
        public override bool MoveCurrentToLast();
        public override bool MoveCurrentTo(object item);
        public override bool MoveCurrentToPosition(int position);
        protected override IEnumerator GetEnumerator();
        public override object GetItemAt(int index);
        public override bool PassesFilter(object item);
        protected override void RefreshOverride();
        public override IDisposable DeferRefresh();
        internal void SetItemsSource(IEnumerable value);
        internal void ClearItemsSource();
        internal void BeginInit();
        internal void EndInit();
    }
}
