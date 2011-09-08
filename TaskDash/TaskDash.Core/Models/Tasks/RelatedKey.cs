using System;
using TaskDash.ExtensionMethods;

namespace TaskDash.Core.Models.Tasks
{
    public class RelatedItem : ModelBase<RelatedItem>
    {
        private String SearchKey { get; set; }

        public override bool CloselyMatches(string matchPhrase)
        {
            if (SearchKey.CloselyMatches(matchPhrase))
            {
                return true;
            }

            return false;
        }

        protected override double GetRanking(string matchPhrase)
        {
            if (SearchKey.CloselyMatches(matchPhrase))
            {
                return 100;
            }

            return 0;
        }

        public override string EditableValue
        {
            get { return this.SearchKey; }
            set { this.SearchKey = value; }
        } 
    }
}
