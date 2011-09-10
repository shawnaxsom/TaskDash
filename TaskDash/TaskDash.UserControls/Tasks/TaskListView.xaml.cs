using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskDash.Core.Models.Sorting;
using TaskDash.Core.Models.Tasks;

namespace TaskDash.UserControls
{
    /// <summary>
    /// Interaction logic for TaskListView.xaml
    /// </summary>
    public partial class TaskListView : UserControl
    {
        private TaskListViewModel _viewModel;

        public TaskListView()
        {
            InitializeComponent();

            listBoxTasks.DataContext = _viewModel.FilteredTasks; // TODO: Is there any way to bind this behind the scenes?
            comboBoxTagsFilter.DataContext = _viewModel.Tasks.TagList;
            comboBoxSortBy.DataContext = TaskComparer.Instance;

            _viewModel.FilteredTasks.Filter += OnFilteredTasksFilter;
        }

        private void OnComboBoxTagsFilterSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OnTextBoxSearchKeyUp(object sender, KeyEventArgs e)
        {
            _viewModel.Search();

            if (e.Key == Key.Enter)
            {
                _viewModel.SelectFirstTask();
            }
        }

        private void OnCheckBoxCurrentFilterChecked(object sender, RoutedEventArgs e)
        {
            _viewModel.Search();
        }

        private void OnComboBoxSortBySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.Search();
        }

        private void OnCheckBoxCompletedFilterChecked(object sender, RoutedEventArgs e)
        {
            _viewModel.Search();
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
            _viewModel.Search();
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

        private void OnTextBoxSearchKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _viewModel.SelectFirstTask();
            }
        }

        private void OnListBoxTasksLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectFirstTask();
            //textBoxKey.Focus();
        }
    }
}
