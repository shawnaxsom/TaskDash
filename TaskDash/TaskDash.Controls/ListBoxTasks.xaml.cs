namespace TaskDash.Controls
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
                this.SelectionChanged += _parentWindow.OnListBoxTasksSelectionChanged;
            }
        }
    }
}
