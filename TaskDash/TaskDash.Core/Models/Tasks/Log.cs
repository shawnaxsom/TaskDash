using System;
using TaskDash.ExtensionMethods;

namespace TaskDash.Core.Models.Tasks
{
    public class Log : ModelBase<Log>
    {
        private DateTime _entryDate = DateTime.Now;
        private string _tags = string.Empty;

        public DateTime EntryDate
        {
            get { return _entryDate; }
            set { _entryDate = value; }
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

        public string Entry { get; set; }

        public override bool CloselyMatches(string matchPhrase)
        {
            if (Entry.CloselyMatches(matchPhrase))
            {
                return true;
            }

            return false;
        }

        protected override double GetRanking(string matchPhrase)
        {
            if (Entry.CloselyMatches(matchPhrase))
            {
                return 100;
            }

            return 0;
        }

        public override string EditableValue
        {
            get { return this.EntryDate.ToString(); }
            set { this.EntryDate = DateTime.Parse(value); }
        } 
    }
}
