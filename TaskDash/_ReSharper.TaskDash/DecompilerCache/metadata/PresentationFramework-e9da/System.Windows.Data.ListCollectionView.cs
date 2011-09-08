// Type: System.Windows.Data.ListCollectionView
// Assembly: PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\Profile\Client\PresentationFramework.dll

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime;

namespace System.Windows.Data
{
    public class ListCollectionView : CollectionView, IComparer, IEditableCollectionViewAddNewItem,
                                      IEditableCollectionView, IItemProperties
    {
        public ListCollectionView(IList list);
        public override bool CanGroup { get; }
        public override ObservableCollection<GroupDescription> GroupDescriptions { get; }
        public override ReadOnlyObservableCollection<object> Groups { get; }
        public override SortDescriptionCollection SortDescriptions { get; }
        public override bool CanSort { get; }
        public override bool CanFilter { get; }
        public override Predicate<object> Filter { get; set; }

        public IComparer CustomSort { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        [DefaultValue(null)]
        public virtual GroupDescriptionSelectorCallback GroupBySelector { get; set; }

        public override int Count { get; }
        public override bool IsEmpty { get; }
        public bool IsDataInGroupOrder { get; set; }
        protected bool UsesLocalArray { get; }

        protected IList InternalList { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        protected IComparer ActiveComparer { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        protected Predicate<object> ActiveFilter { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        protected bool IsGrouping { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        protected int InternalCount { get; }

        #region IComparer Members

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        int IComparer.Compare(object o1, object o2);

        #endregion

        #region IEditableCollectionViewAddNewItem Members

        public object AddNew();
        public object AddNewItem(object newItem);
        public void CommitNew();
        public void CancelNew();
        public void RemoveAt(int index);
        public void Remove(object item);
        public void EditItem(object item);
        public void CommitEdit();
        public void CancelEdit();

        public NewItemPlaceholderPosition NewItemPlaceholderPosition { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        public bool CanAddNew { get; }
        public bool CanAddNewItem { get; }
        public bool IsAddingNew { get; }
        public object CurrentAddItem { get; }
        public bool CanRemove { get; }
        public bool CanCancelEdit { get; }
        public bool IsEditingItem { get; }

        public object CurrentEditItem { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        #endregion

        #region IItemProperties Members

        public ReadOnlyCollection<ItemPropertyInfo> ItemProperties { get; }

        #endregion

        protected override void RefreshOverride();
        public override bool Contains(object item);
        public override bool MoveCurrentToPosition(int position);
        public override bool PassesFilter(object item);
        public override int IndexOf(object item);
        public override object GetItemAt(int index);
        protected virtual int Compare(object o1, object o2);
        protected override IEnumerator GetEnumerator();
        protected override void OnBeginChangeLogging(NotifyCollectionChangedEventArgs args);
        protected override void ProcessCollectionChanged(NotifyCollectionChangedEventArgs args);
        protected int InternalIndexOf(object item);
        protected object InternalItemAt(int index);
        protected bool InternalContains(object item);
        protected IEnumerator InternalGetEnumerator();
    }
}
