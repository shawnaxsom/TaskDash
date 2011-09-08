using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace TaskDash.Core.Models.Tasks
{
    public class Links<T> : ModelCollectionBase<Link>
    {
        public override Link AddNewItem()
        {
            Link link = base.AddNewItem();

            this[Count - 1].PropertyChanged += OnPropertyChanged;

            return link;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "TotalTime"
                && e.PropertyName != "RecentTime"
                && e.PropertyName != "LastObserved")
            {
                //RefreshTaskList();
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                OnPropertyChanged(new PropertyChangedEventArgs("TagList"));
            }
        }

        public void Add(string linkText)
        {
            foreach (Link link in this)
            {
                if (link.URI == linkText)
                {
                    link.Occurances++;
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    return;
                }
            }

            this.Add(new Link(linkText));
        }


        public override void AddNew()
        {
            base.AddNew();

            this[Count - 1].PropertyChanged += OnPropertyChanged;
        }
    }
}
