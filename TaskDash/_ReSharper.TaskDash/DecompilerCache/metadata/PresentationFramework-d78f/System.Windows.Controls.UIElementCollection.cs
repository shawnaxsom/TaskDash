// Type: System.Windows.Controls.UIElementCollection
// Assembly: PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\PresentationFramework.dll

using System;
using System.Collections;
using System.Runtime;
using System.Windows;

namespace System.Windows.Controls
{
    public class UIElementCollection : IList, ICollection, IEnumerable
    {
        public UIElementCollection(UIElement visualParent, FrameworkElement logicalParent);
        public virtual int Capacity { get; set; }
        public virtual UIElement this[int index] { get; set; }

        #region IList Members

        public virtual void CopyTo(Array array, int index);
        public virtual void Clear();
        public virtual void RemoveAt(int index);
        int IList.Add(object value);
        bool IList.Contains(object value);
        int IList.IndexOf(object value);
        void IList.Insert(int index, object value);
        void IList.Remove(object value);
        public virtual IEnumerator GetEnumerator();
        public virtual int Count { get; }
        public virtual bool IsSynchronized { get; }
        public virtual object SyncRoot { get; }
        bool IList.IsFixedSize { get; }
        bool IList.IsReadOnly { get; }

        object IList.this[int index] { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        #endregion

        public virtual void CopyTo(UIElement[] array, int index);
        public virtual int Add(UIElement element);
        public virtual int IndexOf(UIElement element);
        public virtual void Remove(UIElement element);
        public virtual bool Contains(UIElement element);
        public virtual void Insert(int index, UIElement element);
        public virtual void RemoveRange(int index, int count);
        protected void SetLogicalParent(UIElement element);
        protected void ClearLogicalParent(UIElement element);
    }
}
