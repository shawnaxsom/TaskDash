using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Norm.Attributes;

namespace TaskDash.Core.Models.Tasks
{
    public class Tasks : ModelCollectionBase<Task>
    {
        private TagList _tagList;
        public Tasks(IEnumerable<Task> tasks)
            : base(tasks)
        {
            //this.CollectionChanged += new NotifyCollectionChangedEventHandler(OnCollectionChanged);
            //this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Tasks_PropertyChanged );

            RegisterPropertyChanged();
            RefreshTagList();
        }

        [MongoIgnore]
        public TagList TagList
        {
            get { return _tagList; }
            private set
            {
                _tagList = value;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                OnPropertyChanged(new PropertyChangedEventArgs("TagList"));
            }
        }

        public override Task AddNewItem()
        {
            Task task = base.AddNewItem();

            this[Count - 1].PropertyChanged += Tasks_PropertyChanged;

            return task;
        }

        private void RegisterPropertyChanged()
        {
            foreach (Task task in this)
            {
                task.PropertyChanged += Tasks_PropertyChanged;
            }
        }

        private void Tasks_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "TotalTime"
                && e.PropertyName != "RecentTime"
                && e.PropertyName != "LastObserved")
            {
                RefreshTagList();
                //RefreshTaskList();
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                OnPropertyChanged(new PropertyChangedEventArgs("TagList"));
            }
        }

        

        private void RefreshTagList()
        {
            if (_tagList == null)
                _tagList = new TagList();
            else
                _tagList.Clear();

            _tagList.Add(new Tag());

            foreach (Task task in this)
            {
                if (!string.IsNullOrEmpty(task.Tags))
                {
                    string[] taskTags = task.Tags.Split(',');

                    foreach (string tag in taskTags)
                    {
                        string cleanTag = tag.Trim().ToLower();

                        if (!_tagList.Contains(cleanTag))
                        {
                            _tagList.Add(cleanTag);
                        }
                    }
                }
            }
        }

        public override void RemoveItem(object item)
        {
            Task task = (Task) item;

            if (task == null)
            {
                return;
            }

            if (task.IsEmpty
                || task.Completed)
            {
                base.RemoveItem(item);

                if (item is IMongoDocument)
                {
                    // Save the document deletion
                    (item as IMongoDocument).Delete();
                }
            }
            else
            {
                task.Completed = true;
            }
        }
    }
}