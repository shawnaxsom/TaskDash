using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TaskDash.Core.Models.Sorting;
using TaskDash.Core.Models.Tasks;
using TaskDash.UserControls.Tasks;
using TaskDash.ViewModels;
using TaskNS = TaskDash.Core.Models.Tasks;

namespace TaskDash.UserControls
{
    public class TaskListViewModel : ViewModelBase<TaskListViewModel>
    {
        public TaskListViewModel(TaskListUserControlView userControlView)
        {
            _userControlView = userControlView;

            Tasks = new Task().GetTasks();
            FilteredTasks = new CollectionViewSource { Source = this.Tasks };
        }

        public CollectionViewSource FilteredTasks { get; set; }

        public void Search()
        {
            if (_tasks == null) return;

            var view = (ListCollectionView)FilteredTasks.View;

            if (view.IsAddingNew)
            {
                view.CommitNew();
            }

            TaskComparer sorter = SortBy ?? TaskComparer.Default;

            if (sorter.MainMethod != TaskComparer.TaskComparingMethod.None)
            {
                sorter.MatchPhrase = SearchTerms;
                view.CustomSort = sorter;
            }

            // Search
            view.Refresh();
        }




        public TaskComparer SortBy
        {
            get { return (TaskComparer)GetValue(SortByProperty); }
            set { SetValue(SortByProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SortBy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SortByProperty =
            DependencyProperty.Register("SortBy", typeof(TaskComparer), typeof(TaskListViewModel), new UIPropertyMetadata(TaskComparer.Default));




        public string SearchTerms
        {
            get { return (string)GetValue(SearchTermsProperty); }
            set { SetValue(SearchTermsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchTerms.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchTermsProperty =
            DependencyProperty.Register("SearchTerms", typeof(string), typeof(TaskListViewModel), new UIPropertyMetadata(string.Empty));



        private TaskNS.Tasks _tasks;
        private TaskListUserControlView _userControlView;

        public TaskNS.Tasks Tasks
        {
            get { return _tasks; }
            private set
            {
                _tasks = value;
                i = 0;
            }
        }

        private void AddNewTask()
        {
            //Tasks.Add(new Task() { _Id = new ObjectId() i.ToString() });
            Tasks.Add(new Task());
            i++;
        }

        public void Save()
        {
            Tasks.Save();
        }

        private void OnFilteredItemsFilter(object sender, FilterEventArgs e)
        {
            e.Accepted = false;
            var taskItem = (TaskItem)e.Item;
            bool? showCompleted = IsCheckedItemsCompletedFilter;

            string search = SearchTerms;
            string tag = (SelectedTagsFilter == null ? "" : SelectedTagsFilter.ToString());


            if (taskItem.Completed == showCompleted)
            {
                e.Accepted = true;
            }
        }





        public Tag SelectedTagsFilter
        {
            get { return (Tag)GetValue(SelectedTagsFilterProperty); }
            set { SetValue(SelectedTagsFilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedTagsFilter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTagsFilterProperty =
            DependencyProperty.Register("SelectedTagsFilter", typeof(Tag), typeof(TaskListViewModel), new UIPropertyMetadata(null));




        public bool? IsCheckedItemsCompletedFilter
        {
            get { return (bool?)GetValue(IsCheckedItemsCompletedFilterProperty); }
            set { SetValue(IsCheckedItemsCompletedFilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCheckedItemsCompletedFilter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckedItemsCompletedFilterProperty =
            DependencyProperty.Register("IsCheckedItemsCompletedFilter", typeof(bool?), typeof(TaskListViewModel), new UIPropertyMetadata(null));



        internal void SelectFirstTask()
        {
            if (_userControlView.listBoxTasks.Items.Count > 0)
            {
                ListBoxItem item = _userControlView.listBoxTasks.GetFirstListBoxItemFromListBox();
                // Force refresh of selection. 
                // Otherwise the program starts up without anything selected.
                item.IsSelected = false;
                item.IsSelected = true;
                item.Focus();
            }
            else
            {
                _userControlView.listBoxTasks.SelectedItem = null;
                //DataContext = null;
            }
        }
    }
}
