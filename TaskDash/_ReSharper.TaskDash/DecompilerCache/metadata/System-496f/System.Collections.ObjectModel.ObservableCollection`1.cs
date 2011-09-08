// Type: System.Collections.ObjectModel.ObservableCollection`1
// Assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime;
using System.Runtime.CompilerServices;

namespace System.Collections.ObjectModel
{
    [TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
    [Serializable]
    public class ObservableCollection<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public ObservableCollection();
        public ObservableCollection(List<T> list);
        public ObservableCollection(IEnumerable<T> collection);

        #region INotifyCollectionChanged Members

        public virtual event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region INotifyPropertyChanged Members

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        void INotifyPropertyChanged.add_PropertyChanged(PropertyChangedEventHandler value);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        void INotifyPropertyChanged.remove_PropertyChanged(PropertyChangedEventHandler value);

        #endregion

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Move(int oldIndex, int newIndex);

        protected override void ClearItems();
        protected override void RemoveItem(int index);
        protected override void InsertItem(int index, T item);
        protected override void SetItem(int index, T item);
        protected virtual void MoveItem(int oldIndex, int newIndex);
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e);
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e);
        protected IDisposable BlockReentrancy();
        protected void CheckReentrancy();
        protected virtual event PropertyChangedEventHandler PropertyChanged;
    }
}
