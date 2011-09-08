using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Threading;

namespace TaskDash.Core.Models.Tasks
{
    public class Phrases<T> : ModelCollectionBase<Phrase>
    {
        private DispatcherTimer _timer;

        public Phrases(IEnumerable<Phrase> items) : base(items)
        {
            _timer = new DispatcherTimer(DispatcherPriority.Normal);
            _timer.Tick += ReduceOccurances;
            _timer.Interval = new TimeSpan(0, 0, 1, 0);
        }

        private void ReduceOccurances(object sender, EventArgs e)
        {
            foreach (Phrase phrase in this)
            {
                phrase.Occurances--;
            }
        }

        public Phrases()
        {
        }

        public override Phrase AddNewItem()
        {
            Phrase phrase = base.AddNewItem();

            this[Count - 1].PropertyChanged += OnPropertyChanged;

            return phrase;
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

        public virtual void Add(string phraseText)
        {
            foreach (Phrase phrase in this)
            {
                if (phrase.Text == phraseText)
                {
                    phrase.Occurances++;
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    return;
                }
            }

            this.Add(new Phrase(phraseText));
        }


        public override void AddNew()
        {
            base.AddNew();

            this[Count - 1].PropertyChanged += OnPropertyChanged;
        }
    }
}
