using System;
using System.Diagnostics;
using System.IO;
using TaskDash.ExtensionMethods;

namespace TaskDash.Core.Models.Tasks
{
    public class Link : ModelBase<Link>
    {
        public Link()
        {
        }

        public Link(string uri)
        {
            URI = uri;
        }

        public string URI { get; set; }
        private int _occurances = 1;
        public int Occurances
        {
            get { return _occurances; }
            set
            {
                _occurances = value;
                OnPropertyChanged("Occurances");
                OnPropertyChanged("DisplayValue");
            }
        }

        public override bool CloselyMatches(string matchPhrase)
        {
            if (URI.CloselyMatches(matchPhrase))
            {
                return true;
            }

            return false;
        }

        protected override double GetRanking(string matchPhrase)
        {
            if (URI.CloselyMatches(matchPhrase))
            {
                return 100;
            }

            return 0;
        }

        public override string DisplayValue
        {
            get
            {
                if (this.EditableValue == null) return "";

                if (this.IsUrl)
                {
                    return GetUrlDisplay();
                }
                else if (this.IsFile)
                {
                    return GetFileDisplay();
                }

                throw new NotImplementedException();
            }
        }

        protected bool IsFile
        {
            get { return !Uri.IsWellFormedUriString(this.URI, UriKind.RelativeOrAbsolute); }
        }

        protected bool IsUrl
        {
            get { return Uri.IsWellFormedUriString(this.URI, UriKind.RelativeOrAbsolute); }
        }

        private string GetFileDisplay()
        {
            string link = EditableValue;
            string fileName = Path.GetFileName(EditableValue);
            string path = Path.GetDirectoryName(link);


            int lastSlash = path.TrimEnd(new char[] { '\\' }).LastIndexOf("\\");

            string value = fileName;
            value +=
                " ("
                + path.Substring(0, Math.Min(20, path.Length))
                + " ... "
                + path.Substring(lastSlash, path.Length - lastSlash)
                + ")";

            return value;
        }

        private string GetUrlDisplay()
        {
            if (this.EditableValue.Length <= 30)
            {
                return this.EditableValue;
            }
            else
            {
                string value = this.EditableValue.Replace(@"http://", "").Substring(0, Math.Min(22, EditableValue.Length));
                value += " ... ";

                int lastSlash = EditableValue.TrimEnd(new char[] { '/' }).LastIndexOf("/");
                int lengthOf = EditableValue.Length - lastSlash;
                lengthOf = Math.Min(lengthOf, 15);

                int startOfEnd = EditableValue.Length - lengthOf;

                value += EditableValue.Substring(startOfEnd, EditableValue.Length - startOfEnd);
                //value += " (" + this.Occurances.ToString() + ")";
                return value;
            }
        }

        public override string EditableValue
        {
            get { return URI; }
            set
            {
                URI = value;
                OnPropertyChanged("EditableValue");
                OnPropertyChanged("DisplayValue");
            }
        }

        public void Execute()
        {
            try
            {
                if (Uri.IsWellFormedUriString(URI.Replace(@"\\", @"\"), UriKind.RelativeOrAbsolute)
                    && (URI.Contains("."))) // IC-71307 was being picked up without this
                {
                    Process.Start(URI);
                }
            }
            catch (Exception ex)
            { }
        }

        public void ExecutePath()
        {
            Process.Start(Path.GetDirectoryName(URI) ?? URI);
        }
    }
}
