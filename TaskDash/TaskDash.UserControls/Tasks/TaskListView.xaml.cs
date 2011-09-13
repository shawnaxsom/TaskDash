using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TaskDash.Core.Models.Sorting;
using TaskDash.Core.Models.Tasks;
using TaskDash.CustomControls;

namespace TaskDash.UserControls.Tasks
{
    /// <summary>
    /// Interaction logic for TaskListView.xaml
    /// </summary>
    public partial class TaskListUserControlView : UserControlViewBase
    {
        public TaskListViewModel ViewModel { get; private set; }

        public ListBoxTasks ListBoxTasks
        {
            get { return listBoxTasks; }
        }

        public TaskListUserControlView()
        {
            InitializeComponent();

            ViewModel = new TaskListViewModel(this);


            listBoxTasks.DataContext = ViewModel.FilteredTasks; // TODO: Is there any way to bind this behind the scenes?
            comboBoxTagsFilter.DataContext = ViewModel.Tasks.TagList;
            comboBoxSortBy.DataContext = TaskComparer.Instance;

            ViewModel.FilteredTasks.Filter += OnFilteredTasksFilter;
        }

        private void OnComboBoxTagsFilterSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OnTextBoxSearchKeyUp(object sender, KeyEventArgs e)
        {
            ViewModel.Search();

            if (e.Key == Key.Enter)
            {
                ViewModel.SelectFirstTask();
            }
        }

        private void OnCheckBoxCurrentFilterChecked(object sender, RoutedEventArgs e)
        {
            ViewModel.Search();
        }

        private void OnComboBoxSortBySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.Search();
        }

        private void OnCheckBoxCompletedFilterChecked(object sender, RoutedEventArgs e)
        {
            ViewModel.Search();
        }

        public void OnListBoxTasksSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //RefreshTaskBindings();

            UpdatedSelected(e);
        }

        private static void UpdatedSelected(SelectionChangedEventArgs e)
        {
            // This happens to modify the stopwatch
            // TODO: Is there any way to make this a binding instead of an event update?
            foreach (Task task in e.AddedItems)
            {
                task.Selected = true;
            }

            foreach (Task task in e.RemovedItems)
            {
                task.Selected = false;
            }
        }

        private void OnCheckBoxSomedayFilterChecked(object sender, RoutedEventArgs e)
        {
            ViewModel.Search();
        }

        private void OnFilteredTasksFilter(object sender, FilterEventArgs e)
        {
            e.Accepted = false;
            var task = (Task)e.Item;


            string search = textBoxSearch.Text;
            string tag = (comboBoxTagsFilter.SelectedValue == null ? "" : comboBoxTagsFilter.SelectedValue.ToString());


            if (task.CloselyMatches(search)
                && (checkBoxCurrentFilter.IsChecked == false
                    || task.Current == checkBoxCurrentFilter.IsChecked)
                && (task.Someday == checkBoxSomedayFilter.IsChecked)
                && (task.Completed == checkBoxCompletedFilter.IsChecked)
                && (tag == String.Empty || task.Tags.ToLower().Contains(tag))
                )
            {
                e.Accepted = true;
            }
        }

        private void OnListBoxTasksKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter
                && !listBoxTasks.IsEditingSelectedItem)
            {
                //textBoxKey.Focus();
            }
        }

        private void OnTextBoxSearchKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ViewModel.SelectFirstTask();
            }
        }

        private void OnListBoxTasksLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectFirstTask();
            //textBoxKey.Focus();
        }

        public void OnWindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled) return;

            if (e.Key == Key.T)
            {
                AddItemOrFocus(listBoxTasks);
                e.Handled = true;
            }
            else if (!IsEditing
                     && e.Key == Key.Oem2) // Forward Slash
            {
                textBoxSearch.Focus();
                e.Handled = true;
            }
        }
    }
}
