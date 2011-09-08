namespace TaskDash.Core.Models.Tasks
{
    public class TagList : ModelCollectionBase<Tag>
    {
        public bool Contains(string tagName)
        {
            foreach (Tag tag in this)
            {
                if (tag.TagName == tagName)
                {
                    return true;
                }
            }

            return false;
        }

        public void Add(string tagName)
        {
            this.Add(new Tag(tagName));
        }
    }
}
