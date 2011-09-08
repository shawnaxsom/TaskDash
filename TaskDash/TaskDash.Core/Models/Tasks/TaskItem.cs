using System;
using TaskDash.ExtensionMethods;

namespace TaskDash.Core.Models.Tasks
{
    public class TaskItem : ModelBase<TaskItem>, ICompletable
    {
        private string _description = string.Empty;

        private string _notes;

        private bool _completed;
        public bool Completed
        {
            get { return _completed; }
            set
            {
                _completed = value;
                OnPropertyChanged("Completed");
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

        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged("Notes");
            }
        }

        public bool Selected { get; set; }

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

        public override bool CloselyMatches(string matchPhrase)
        {
            if (Description.CloselyMatches(matchPhrase))
            {
                return true;
            }

            return false;
        }

        protected override double GetRanking(string matchPhrase)
        {
            if (Description.CloselyMatches(matchPhrase))
            {
                return 100;
            }

            return 0;
        }

        public override string ToString()
        {
            return Description;
        }

        public override string DisplayValue
        {
            get
            {
                var displayValue = _description;
                if (!string.IsNullOrEmpty(_notes))
                {
                    displayValue += " (" + _notes.Substring(0, Math.Min(30, _notes.Length)) + ")";
                }
                return displayValue;
            }
        }
    }

    public interface ICompletable
    {
        bool Completed { get; set; }
    }
}