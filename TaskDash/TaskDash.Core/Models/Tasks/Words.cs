using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using TaskDash.ExtensionMethods;

namespace TaskDash.Core.Models.Tasks
{
    public class Words<T> : Phrases<Word>
    {
        List<string> skipwords;

        public Words()
        {
            InitializeSkipwords();
        }

        public Words(IEnumerable<Phrase> items)
            : base(items)
        {
            InitializeSkipwords();
        }

        private void InitializeSkipwords()
        {
            skipwords = new List<string>
                                 {
                                     "{",
                                     "}",
                                     " ",
                                     "public",
                                     "private",
                                     "internal",
                                     "void",
                                     "if",
                                     "return",
                                     "get",
                                     "set",
                                     "value",
                                     "string",
                                     "new",
                                     "bool",
                                     "datetime",
                                     "int",
                                     "else",
                                     "foreach",
                                     "in",
                                     "using",
                                     "trim",
                                     "replace",
                                     "for",
                                     "dockpanel",
                                     "label",
                                     "binding",
                                     "system",
                                     "none",
                                     "windows",
                                    "namespace",
                                    "controls",
                                    "partial",
                                    "object",
                                    "sender",
                                    "class",
                                    "onpropertychanged",
                                    "isnullorempty",
                                    "intptr",
                                    "empty",
                                    "window",
                                    "getvalue",
                                    "register",
                                    "frameworkpropertymetadata",
                                    "reset",
                                    "addnew",
                                    "count",
                                    "focus",
                                    "search",
                                    "empty",
                                    "window",
                                    "keyboard",
                                    "selectedvalue",
                                    "getvalue",
                                    "isediting",
                                    "setvalue"
                                 };
        }

        public override void Add(string phraseText)
        {
            foreach (string word in CleanUpWords(phraseText).Split(new char[] { ' ' }))
            {
                AddWord(word);
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void AddWord(string wordText)
        {
            wordText = CleanUpWords(wordText);

            if (string.IsNullOrEmpty(wordText)
                || skipwords.Contains(wordText.ToLower().Trim())
                || wordText.Length < 5
                || !wordText.MatchesRegex(@"^[A-Z].*$"))
            {
                return;
            }

            foreach (Phrase word in this)
            {
                if (word.Text == wordText)
                {
                    word.Occurances++;
                    //OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.));
                    return;
                }
            }

            Word newWord = new Word(wordText);
            Add(newWord);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newWord));
        }

        private string CleanUpWords(string wordText)
        {
            //wordText = wordText
            //    .Replace('\r', ' ')
            //    .Replace('\n', ' ')
            //    .Replace(";", string.Empty)
            //    .Replace("=", string.Empty)
            //    .Replace("/", string.Empty)
            //    .Replace("\\", string.Empty)
            //    .Replace("'", string.Empty)
            //    .Replace("\"", string.Empty)
            //    .Replace("<", string.Empty)
            //    .Replace(">", string.Empty)
            //    .Replace("(", string.Empty)
            //    .Replace(")", string.Empty)
            //    .Trim();

            wordText = Regex.Replace(wordText, @"[\W]", " ").Trim();


            return wordText;
        }
    }
}
