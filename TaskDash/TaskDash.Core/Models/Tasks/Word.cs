using System;
using System.Windows;
using TaskDash.ExtensionMethods;

namespace TaskDash.Core.Models.Tasks
{
    public class Word : Phrase<Word>
    {
        public Word(string word)
        {
            this.Text = word;
        }
    }
}
