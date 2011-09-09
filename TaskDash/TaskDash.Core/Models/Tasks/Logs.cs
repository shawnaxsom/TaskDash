using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Norm.Attributes;

namespace TaskDash.Core.Models.Tasks
{
    public class Logs<T> : ModelCollectionBase<Log>
    {
        private TagList _logTagList;

        [MongoIgnore]
        public TagList LogTagList
        {
            get { return _logTagList; }
            private set
            {
                _logTagList = value;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                OnPropertyChanged(new PropertyChangedEventArgs("LogTagList"));
            }
        }

        public Logs() 
        {
            //RegisterPropertyChanged();
            //RefreshLogTagList();
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                this[Count - 1].PropertyChanged += OnLogsPropertyChanged;
            }
        }

        public override Log AddNewItem()
        {
            Log log = base.AddNewItem();

            //this[Count - 1].PropertyChanged += OnLogsPropertyChanged;

            return log;
        }

        private void RegisterPropertyChanged()
        {
            foreach (Log log in this)
            {
                log.PropertyChanged += OnLogsPropertyChanged;
            }
        }

        private void OnLogsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "TotalTime"
                && e.PropertyName != "RecentTime"
                && e.PropertyName != "LastObserved")
            {
                RefreshLogTagList();
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                OnPropertyChanged(new PropertyChangedEventArgs("LogTagList"));
            }
        }

        public void RefreshLogTagList()
        {
            if (_logTagList == null)
                _logTagList = new TagList();
            else
                _logTagList.Clear();

            _logTagList.Add(new Tag());

            foreach (Log log in this)
            {
                if (!string.IsNullOrEmpty(log.Tags))
                {
                    string[] tags = log.Tags.Split(',');

                    foreach (string tag in tags)
                    {
                        string cleanTag = tag.Trim().ToLower();

                        if (!_logTagList.Contains(cleanTag))
                        {
                            _logTagList.Add(cleanTag);
                        }
                    }
                }
            }
        }

        public Log GetMostRecentLog()
        {
            var logs =
                from l in this
                orderby l.EntryDate descending
                select l;

            return logs.ElementAt(0);
        }
    }
}
