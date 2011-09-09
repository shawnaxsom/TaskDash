using System.ComponentModel;
using System.Windows;

namespace TaskDash.ViewModels
{
    public class ViewModelBase<T> : DependencyObject
    {
        public int i { get; protected set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}