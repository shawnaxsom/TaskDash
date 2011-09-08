using System;
using System.Windows;
using TaskDash.ExtensionMethods;

namespace TaskDash.Core.Models.Tasks
{
    public class Phrase<T> : Phrase
    {
    }
    public class Phrase : ModelBase<Phrase>
    {
        public Phrase(string text)
        {
            Text = text;
        }

        public Phrase()
        {
        }

        public string Text { get; set; }
        
        private int _occurances = 50;
        public int Occurances
        {
            get { return _occurances; }
            set
            {
                if (value > 0)
                {
                    _occurances = value;
                    OnPropertyChanged("Occurances");
                    OnPropertyChanged("DisplayValue");
                }
            }
        }

        public override bool CloselyMatches(string matchPhrase)
        {
            if (Text.CloselyMatches(matchPhrase))
            {
                return true;
            }

            return false;
        }

        protected override double GetRanking(string matchPhrase)
        {
            if (Text.CloselyMatches(matchPhrase))
            {
                return 100;
            }

            return 0;
        }

        public override string EditableValue
        {
            get { return Text; }
            set { Text = value; }
        }

        public override string DisplayValue
        {
            get
            {
                string trimmedValue = this.EditableValue.Replace('\r', ' ').Replace('\n', ' ');

                string value = trimmedValue.Substring(0, Math.Min(40, trimmedValue.Length));

                return value;
            }
        }
    }
}
