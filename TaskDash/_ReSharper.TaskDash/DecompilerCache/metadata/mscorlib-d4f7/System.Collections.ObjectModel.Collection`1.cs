// Type: System.Collections.ObjectModel.Collection`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.InteropServices;

namespace System.Collections.ObjectModel
{
    [ComVisible(false)]
    [DebuggerTypeProxy(typeof (Mscorlib_CollectionDebugView<T>))]
    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
    public class Collection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
    {
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public Collection();

        public Collection(IList<T> list);
        protected IList<T> Items { get; }

        #region IList Members

        void ICollection.CopyTo(Array array, int index);
        int IList.Add(object value);
        bool IList.Contains(object value);
        int IList.IndexOf(object value);
        void IList.Insert(int index, object value);
        void IList.Remove(object value);
        bool ICollection.IsSynchronized { get; }
        object ICollection.SyncRoot { get; }
        object IList.this[int index] { get; set; }
        bool IList.IsReadOnly { get; }
        bool IList.IsFixedSize { get; }

        #endregion

        #region IList<T> Members

        public void Add(T item);

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public void Clear();

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public void CopyTo(T[] array, int index);

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public bool Contains(T item);

        public IEnumerator<T> GetEnumerator();

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public int IndexOf(T item);

        public void Insert(int index, T item);
        public bool Remove(T item);
        public void RemoveAt(int index);

        IEnumerator IEnumerable.GetEnumerator();

        public int Count { [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        get; }

        public T this[int index] { [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        get; set; }

        bool ICollection<T>.IsReadOnly { get; }

        #endregion

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        protected virtual void ClearItems();

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        protected virtual void InsertItem(int index, T item);

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        protected virtual void RemoveItem(int index);

        protected virtual void SetItem(int index, T item);
    }
}
