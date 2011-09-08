using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TaskDash.Core
{
    public interface IModelCollection
    {
        void AddNew();
        void RemoveItem(object item);
        int Count { get; }
    }

    public interface IModelCollection<T> : IModelCollection
    {
        T AddNewItem();
        T this[int index] { get; set; }
    }

    public class ModelCollectionBase<T> : ObservableCollection<T>, IModelCollection<T> where T : ModelBase<T>, new()
    {
        public ModelCollectionBase()
            : base()
        { }
        public ModelCollectionBase(IEnumerable<T> items)
            : base(items)
        { }

        public virtual T AddNewItem()
        {
            var item = new T();
            this.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            return item;
        }

        public virtual void AddNew()
        {
            var item = new T();
            this.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public virtual void RemoveItem(object item)
        {
            T removedItem = (T)item;
            //OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItem));
            this.Remove(removedItem);
        }
    }
}
