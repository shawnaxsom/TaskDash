using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using TaskDash.Controls;
using TaskDash.Core.Models.Sorting;
using TaskDash.Core.Models.Tasks;
using TaskDash.ViewModels;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace TaskDash
{
    /// <summary>
    /// Interaction logic for AccordianWindow.xaml
    /// </summary>
    public partial class AccordianWindow
    {
        private readonly TaskViewModel _tasks;
        private NotifyIcon _notifyIcon;

        public AccordianWindow(TaskViewModel tasks)
        {
            InitializeComponent();


            _tasks = tasks;

            listBoxTasks.DataContext = _tasks.FilteredTasks;
            comboBoxTagsFilter.DataContext = _tasks.Tasks.TagList;
            comboBoxSortBy.DataContext = TaskComparer.Instance;


            _tasks.FilteredTasks.Filter += OnFilteredTasksFilter;


            Search();
        }

        private void OnFilteredTasksFilter(object sender, FilterEventArgs e)
        {
            e.Accepted = false;
            var task = (Task) e.Item;


            string search = textBoxSearch.Text;
            string tag = (comboBoxTagsFilter.SelectedValue == null ? "" : comboBoxTagsFilter.SelectedValue.ToString());


            if (task.CloselyMatches(search)
                && (checkBoxCurrentFilter.IsChecked == false
                    || task.Current == checkBoxCurrentFilter.IsChecked)
                && (task.Completed == checkBoxCompletedFilter.IsChecked)
                && (tag == String.Empty || task.Tags.ToLower().Contains(tag))
                )
            {
                e.Accepted = true;
            }
        }

        private void OnTextBoxSearchKeyUp(object sender, KeyEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            if (_tasks != null)
            {
                var view = (ListCollectionView) _tasks.FilteredTasks.View;

                var sorter = (TaskComparer) comboBoxSortBy.SelectedValue;
                if (sorter != null)
                {
                    sorter.MatchPhrase = textBoxSearch.Text;
                    view.CustomSort = sorter;
                }

                // Search
                view.Refresh();

                SelectFirstTask();
            }
        }

        private bool Save()
        {
            // Make sure you exit the current box if it has edits, before saving
            textBoxSummary.Focus();
            textBoxKey.Focus();

            _tasks.Save();

            return true;
        }

        private void OnTextBoxSearchKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                listBoxTasks.Focus();
            }
        }

        private void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Save();

            if (_notifyIcon != null)
            {
                _notifyIcon.Dispose();
                _notifyIcon = null;
            }
        }

        private void OnListBoxTasksLoaded(object sender, RoutedEventArgs e)
        {
            SelectFirstTask();
        }

        private void SelectFirstTask()
        {
            if (listBoxTasks.Items.Count > 0)
            {
                listBoxTasks.SelectedItem = listBoxTasks.Items[0];
            }
            else
            {
                listBoxTasks.SelectedItem = null;
                DataContext = null;
            }
        }

        private void OnWindowKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (e.Key == Key.W)
                {
                    Close();
                }
            }
            else
            {
                if (e.Key == Key.Escape)
                {
                    WindowState = WindowState.Minimized;
                }
            }
        }

        private void OnStateChanged(object sender, EventArgs args)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
            }
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            CheckTrayIcon();
        }

        private void CheckTrayIcon()
        {
            ShowTrayIcon(!IsVisible);
        }

        private void ShowTrayIcon(bool show)
        {
            if (_notifyIcon != null)
                _notifyIcon.Visible = show;
        }

        private void checkBoxCurrentFilter_Checked(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void EditTaskItemClick(object sender, RoutedEventArgs e)
        {
            var task = (Task)listBoxTasks.SelectedItem;
            var listBox = (ListBoxWithAddRemove) sender;
            var item = (TaskItem) listBox.SelectedItem;

            var editTaskItem = new EditTaskItem(task.FilteredItems.View, item);
            editTaskItem.Show();
        }

        private void OnListBoxTasksSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var task = (Task) listBoxTasks.SelectedItem;
            if (task != null)
            {
                // TODO: I shouldn't have to do this. How do I do ItemsSource="{Binding Links}" but have ADD button be able to get the Links collection rather than the Tasks collection?
                listBoxLinks.DataContext = task.Links;
                listBoxLogs.DataContext = task.Logs;
                listBoxItems.DataContext = task.FilteredItems;
                DataContext = task;
            }

            UpdatedSelected(e);
        }

        private void UpdatedSelected(SelectionChangedEventArgs e)
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

        private void OnListBoxLogsSelectionChanged(object sender, RoutedEventArgs e)
        {
            var log = (Log) listBoxLogs.SelectedItem;
            textBoxLogEntry.DataContext = log;
        }

        private void OnComboBoxTagsFilterSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void OnButtonStartStopClick(object sender, RoutedEventArgs e)
        {
            var task = (Task) listBoxTasks.SelectedItem;
            if (task != null)
            {
                task.ToggleTimer();
            }
        }

        private void OnComboBoxSortBySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void OnButtonResetClick(object sender, RoutedEventArgs e)
        {
            var task = (Task) listBoxTasks.SelectedItem;
            if (task != null)
            {
                task.ResetTimer();
            }
        }

        private void OnCheckBoxCompletedFilterChecked(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void OnUndockButtonClick(object sender, RoutedEventArgs e)
        {
            if (Save())
            {
                var window = MainWindow.Instance;
                window.Show();

                Close();
            }
        }
    }
}