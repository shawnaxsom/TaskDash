using System;
using System.Windows.Controls;

namespace TaskDash.CustomControls
{
    /// <summary>
    /// Interaction logic for ListBoxLinks.xaml
    /// </summary>
    public partial class ListBoxTasks : ListBoxWithAddRemove
    {
        public ListBoxTasks()
        {
            InitializeComponent();
        }

        private IMainWindow _parentWindow;
        public IMainWindow ParentWindow
        {
            get { return _parentWindow; }
            set
            {
                _parentWindow = value;
                this.SelectionChanged += OnListBoxTasksSelectionChanged;
            }
        }

        private void OnListBoxTasksSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //_parentWindow.OnListBoxTasksSelectionChanged(sender, e);
        }
    }
}
