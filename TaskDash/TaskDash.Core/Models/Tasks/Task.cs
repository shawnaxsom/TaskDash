using System;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Forms;
using Norm;
using Norm.Attributes;
using Norm.Collections;
using Norm.Configuration;
using TaskDash.Core.ExtensionMethods;
using TaskDash.Core.Services;
using TaskDash.Core.Utilities;
using TaskDash.ExtensionMethods;

namespace TaskDash.Core.Models.Tasks
{
    public class Task : ModelBase<Task>, IMongoDocument
    {
        private readonly TaskStopwatch _stopwatch = new TaskStopwatch();
        private Tasks _observableTasks;
        private IMongoCollection<Task> _taskData;

        public Task()
        {
            _stopwatch = new TaskStopwatch(TimeSpan.Parse(TotalTime));

            _stopwatch.Tick += OnStopwatchTick;
        }

        private void OnStopwatchTick(object sender, EventArgs e)
        {
            Times.Today.Time = _stopwatch.Elapsed.ToString();
            LastObserved = DateTime.Now;

            OnPropertyChanged("TotalTime");
            OnPropertyChanged("RecentTime");
        }

        #region Stopwatch Functionality

        [MongoIgnore]
        public string TotalTime
        {
            get { return Times.TotalTime; }
        }

        [MongoIgnore]
        public string RecentTime
        {
            get { return Times.TimeWithinDaysFormatted(0); }
        }

        public void StartTimer()
        {
            _stopwatch.Start();
        }

        public void StopTimer()
        {
            _stopwatch.Stop();
        }

        public void ToggleTimer()
        {
            _stopwatch.Toggle();
        }

        public void ResetTimer()
        {
            _stopwatch.Reset(TaskStopwatch.ResetType.ToZero);
        }

        #endregion Stopwatch Functionality

        #region Fields

        private readonly TimeSpan OBSERVED_TIME_THRESHHOLD = new TimeSpan(0, 0, 0, 10);
        private DateTime _completedDate = DateTime.MinValue;
        private bool _current;
        private string _description;
        private string _details;
        private DateTime _dueDate = DateTime.Today.AddDays(7);
        private string _key;
        private DateTime _lastObserved;
        private string _nextSteps;
        private string _priority;
        private bool _selected;
        private bool _someday;
        private string _summary;
        private string _tags = String.Empty;

        [MongoIgnore]
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;

                if (value)
                {
                    _stopwatch.Start();
                }
                else
                {
                    _stopwatch.Stop();
                }
            }
        }

        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                OnPropertyChanged("Key");
            }
        }

        public string Tags
        {
            get { return _tags; }
            set
            {
                _tags = value;
                OnPropertyChanged("Tags");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public string Summary
        {
            get { return _summary; }
            set
            {
                _summary = value;
                OnPropertyChanged("Summary");
            }
        }

        public string Details
        {
            get { return _details; }
            set
            {
                _details = value;
                OnPropertyChanged("Details");
            }
        }

        public string NextSteps
        {
            get { return _nextSteps; }
            set
            {
                _nextSteps = value;
                OnPropertyChanged("NextSteps");
            }
        }

        public string Priority
        {
            get { return _priority; }
            set
            {
                _priority = value;
                OnPropertyChanged("Priority");
            }
        }

        public DateTime DueDate
        {
            get { return _dueDate; }
            set
            {
                _dueDate = value;
                OnPropertyChanged("DueDate");
            }
        }

        private DateTime CompletedDate
        {
            get { return _completedDate; }
            set
            {
                _completedDate = value;
                OnPropertyChanged("CompletedDate");
                OnPropertyChanged("Completed");
            }
        }

        [MongoIgnore]
        public bool Completed
        {
            get { return CompletedDate.Date != DateTime.MinValue.Date; }
            set
            {
                if (value)
                {
                    if (AllowCompletion())
                    {
                        CompletedDate = DateTime.Now;
                        Current = false;
                        Someday = false;
                    }
                }
                else
                {
                    CompletedDate = DateTime.MinValue;
                }
            }
        }

        private bool AllowCompletion()
        {
            foreach (TaskItem item in Items)
            {
                if (item.Completed == false)
                {
                    ShowCompletionNotAllowed("An item has not been completed.");
                    return false;
                }
            }

            return true;
        }

        private void ShowCompletionNotAllowed(string reason)
        {
            MessageBox.Show(reason, "Completion Not Allowed");
        }

        public bool Current
        {
            get { return _current; }
            set
            {
                _current = value;
                OnPropertyChanged("Current");
            }
        }

        public bool Someday
        {
            get { return _someday; }
            set
            {
                _someday = value;
                OnPropertyChanged("Someday");
            }
        }

        public DateTime LastObserved
        {
            get { return _lastObserved; }
            set
            {
                if (_stopwatch.ElapsedSinceLastStopped > OBSERVED_TIME_THRESHHOLD)
                {
                    _lastObserved = value;
                    OnPropertyChanged("LastObserved");
                }
            }
        }

        #endregion Fields

        #region Members

        private TaskItems<TaskItem> _items = new TaskItems<TaskItem>();
        private Links<Link> _links = new Links<Link>();
        private Phrases<Phrase> _phrases = new Phrases<Phrase>();
        private Logs<Log> _logs = new Logs<Log>();
        private ModelCollectionBase<RelatedItem> _relatedItems = new ModelCollectionBase<RelatedItem>();
        private TaskTimeOnDays<TaskTimeOnDay> _times = new TaskTimeOnDays<TaskTimeOnDay>();
        private Words<Phrase> _words = new Words<Phrase>();


        [MongoIgnore]
        public CollectionViewSource FilteredLogs
        {
            get
            {
                if (_filteredLogs == null)
                {
                    _filteredLogs = new CollectionViewSource { Source = this.Logs };
                }
                return _filteredLogs;
            }
            private set { _filteredLogs = value; }
        }
        public Logs<Log> Logs
        {
            get { return _logs; }
            set { _logs = value; }
        }

        [MongoIgnore]
        public CollectionViewSource FilteredLinks
        {
            get
            {
                if (_filteredLinks == null)
                {
                    _filteredLinks = new CollectionViewSource { Source = this.Links };
                }
                return _filteredLinks;
            }
            private set { _filteredLinks = value; }
        }
        public Links<Link> Links
        {
            get { return _links; }
            set { _links = value; }
        }

        [MongoIgnore]
        public CollectionViewSource FilteredPhrases
        {
            get
            {
                if (_filteredPhrases == null)
                {
                    _filteredPhrases = new CollectionViewSource { Source = this.Phrases };
                }
                return _filteredPhrases;
            }
            private set { _filteredPhrases = value; }
        }
        public Phrases<Phrase> Phrases
        {
            get { return _phrases; }
            set { _phrases = value; }
        }

        [MongoIgnore]
        public CollectionViewSource FilteredWords
        {
            get
            {
                if (_filteredWords == null)
                {
                    _filteredWords = new CollectionViewSource { Source = this.Words };
                }
                return _filteredWords;
            }
            private set { _filteredWords = value; }
        }
        public Words<Phrase> Words
        {
            get { return _words; }
            set { _words = value; }
        }

        public ModelCollectionBase<RelatedItem> RelatedItems
        {
            get { return _relatedItems; }
            set { _relatedItems = value; }
        }

        private CollectionViewSource _filteredItems;
        private CollectionViewSource _filteredLogs;
        private CollectionViewSource _filteredLinks;
        private CollectionViewSource _filteredPhrases;
        private CollectionViewSource _filteredWords;
        

        [MongoIgnore]
        public CollectionViewSource FilteredItems
        {
            get
            {
                if (_filteredItems == null)
                {
                    _filteredItems = new CollectionViewSource { Source = this.Items };
                }
                return _filteredItems;
            }
            private set { _filteredItems = value; }
        }

        public TaskItems<TaskItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        public TaskTimeOnDays<TaskTimeOnDay> Times
        {
            get { return _times; }
            set
            {
                _times = value;
                OnPropertyChanged("Times");
            }
        }

        #endregion Members

        #region Business Methods

        public string DisplayRank { get; private set; }

        public override string DisplayValue
        {
            get { return ToString(); }
        }

        public DateTime LastDateLogged
        {
            get
            {
                if (Logs == null)
                    return DateTime.MinValue;

                DateTime maxDate = DateTime.MinValue;
                foreach (Log log in Logs)
                {
                    if (log.EntryDate > maxDate)
                    {
                        maxDate = log.EntryDate;
                    }
                }
                return maxDate;
            }
        }

        public override string EditableValue
        {
            get { return Description; }
            set
            {
                Description = value;
                OnPropertyChanged("EditableValue");
                OnPropertyChanged("DisplayValue");
            }
        }

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(Key)
                       && string.IsNullOrEmpty(Description)
                       && string.IsNullOrEmpty(Summary)
                       && string.IsNullOrEmpty(Details)
                       && Logs.Count == 0
                       && Items.Count == 0;
            }
        }

        public void Delete()
        {
            DataManager.RemoveData(this, _taskData);
        }

        public override bool CloselyMatches(string matchPhrase)
        {
            if (matchPhrase == string.Empty)
            {
                return true;
            }

            if (Key.CloselyMatches(matchPhrase)
                || Description.CloselyMatches(matchPhrase)
                || Summary.CloselyMatches(matchPhrase)
                || Details.CloselyMatches(matchPhrase))
            {
                return true;
            }
            else
            {
                if (Logs.CloselyMatches(matchPhrase))
                {
                    return true;
                }
                else if (Links.CloselyMatches(matchPhrase))
                {
                    return true;
                }
                else if (Items.CloselyMatches(matchPhrase))
                {
                    return true;
                }
            }

            return false;
        }

        protected override double GetRanking(string matchPhrase)
        {
            double rank = 0;


            rank += Key.MatchRanking(matchPhrase)
                    + Description.MatchRanking(matchPhrase)
                    + Summary.MatchRanking(matchPhrase)
                    + Details.MatchRanking(matchPhrase);

            if (Logs.CloselyMatches(matchPhrase))
            {
                rank += Logs.MatchRanking(matchPhrase);
            }

            if (Links.CloselyMatches(matchPhrase))
            {
                rank += Links.MatchRanking(matchPhrase);
            }

            if (Items.CloselyMatches(matchPhrase))
            {
                rank += Items.MatchRanking(matchPhrase);
            }


            DisplayRank = (matchPhrase == String.Empty ? String.Empty : rank.ToString());
            return rank;
        }

        public override string ToString()
        {
            var display = new StringBuilder();

            if (!string.IsNullOrEmpty(Key))
            {
                display.Append(Key);
            }

            if (!string.IsNullOrEmpty(DisplayRank))
            {
                display.Append(" (" + DisplayRank + ")");
            }

            if (display.Length > 0)
            {
                display.Append("-");
            }

            display.Append(Description);

            return display.ToString();
        }

        public Tasks GetTasks()
        {
            IMongoCollection<Task> _taskData = DataManager.GetData();
            IQueryable<Task> tasks = _taskData.AsQueryable();

            try
            {
                _observableTasks = new Tasks(tasks);
            }
            catch (MongoConfigurationMapException ex)
            {

            }
            catch (MongoException ex)
            {
                object obj = ex.Data;

            }
            catch (Exception ex)
            {

            }

            return _observableTasks;
        }

        public void Save()
        {
            DataManager.SaveData(this, _taskData);
        }

        #endregion Business Methods

        public void HandleClipboardData(ClipboardMonitorService clipboardMonitor)
        {
            if (clipboardMonitor.ClipboardContainsText
                || clipboardMonitor.ClipboardContainsFileDrops)
            {
                if (clipboardMonitor.ClipboardContainsURI)
                {
                    Links.Add(clipboardMonitor.ClipboardText);
                }
                else if (clipboardMonitor.ClipboardContainsFileDrops)
                {
                    foreach (string file in clipboardMonitor.ClipboardFileDropList)
                    {
                        Links.Add(file);
                    }
                }
                else if (clipboardMonitor.ClipboardContainsPhrase)
                {
                    Phrases.Add(clipboardMonitor.ClipboardText);
                    Words.Add(clipboardMonitor.ClipboardText);
                }
            }
        }
    }
}