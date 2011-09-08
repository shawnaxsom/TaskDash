using TaskDash.ExtensionMethods;

namespace TaskDash.Core.Models.Tasks
{
    public class Tag : ModelBase<Tag>
    {
        public string TagName { get; set; }

        public Tag()
            : base()
        {}

        public Tag(string tagName)
            :base()
        {
            TagName = tagName;
        }

        public override bool CloselyMatches(string matchPhrase)
        {
            if (TagName.CloselyMatches(matchPhrase))
            {
                return true;
            }

            return false;
        }

        protected override double GetRanking(string matchPhrase)
        {
            if (TagName.CloselyMatches(matchPhrase))
            {
                return 100;
            }

            return 0;
        }

        public override string EditableValue
        {
            get { return TagName; }
            set { TagName = value; }
        }

        public override string ToString()
        {
            return TagName ?? string.Empty;
        }
    }
}
