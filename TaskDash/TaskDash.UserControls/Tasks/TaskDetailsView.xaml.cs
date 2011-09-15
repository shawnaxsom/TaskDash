using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TaskDash.CustomControls;
using TaskDash.Core.Models.Tasks;

namespace TaskDash.UserControls.Tasks
{
    /// <summary>
    /// Interaction logic for TaskDetailsView.xaml
    /// </summary>
    public partial class TaskDetailsView : UserControlViewBase
    {
        public TaskDetailsView(ITaskList taskList)
        {
            InitializeComponent();

            taskList.SelectedTaskChanged += new SelectionChangedEventHandler(OnSelectedTaskChanged);
        }

        public void OnSelectedTaskChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == 0) return;

            Task task = e.AddedItems[0];
            CurrentTask = task;
        }


        private static readonly DependencyProperty CurrentTaskProperty = DependencyProperty.Register(
            "CurrentTask", typeof(Task), typeof(TaskDetailsView), new PropertyMetadata(null));

        private TaskDetailsViewModel _viewModel;

        public Task CurrentTask
        {
            get { return (Task) GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }

        private void OnButtonStartStopClick(object sender, RoutedEventArgs e)
        {
            var task = CurrentTask;
            if (task != null)
            {
                task.ToggleTimer();
            }
        }

        void OnFilteredLogsFilter(object sender, FilterEventArgs e)
        {
            e.Accepted = false;
            var taskLog = (Log)e.Item;

            string tag = (comboBoxLogTagsFilter.SelectedValue == null ? "" : comboBoxLogTagsFilter.SelectedValue.ToString());


            if (string.IsNullOrEmpty(tag)
                || taskLog.Tags.ToLower().Contains(tag.ToLower()))
            {
                e.Accepted = true;
            }
        }

        private void OnCheckBoxItemsCompletedFilterChecked(object sender, RoutedEventArgs e)
        {
            _viewModel.RefreshItems();
        }

        private void OnButtonResetClick(object sender, RoutedEventArgs e)
        {
            var task = CurrentTask;
            if (task != null)
            {
                task.ResetTimer();
            }
        }

        private void TextBoxWithDescriptionControlFocused(object sender, RoutedEventArgs e)
        {
            // TODO: Add this capability back in
            //if (DockingState == MainWindow.WindowDockingState.AddingDockingControls)
            //{
            //    var source = (TextBoxWithDescription)e.Source;

            //    _dockWindow.AddControl(source);
            //}
        }

        private void OnComboBoxLogTagsFilterSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnButtonIssueTrackerClick(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenIssueTracker();
        }

        private void OnListBoxItemsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _viewModel.ShowEditTaskItemDialog();
            }
        }

        private void EditTaskItemClick(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowEditTaskItemDialog();
        }

        public TextBoxWithDescription TextBoxLogEntry
        {
            get { return textBoxLogEntry; }
        }

        public ListBoxWithAddRemove ListBoxLogs
        {
            get {
                return listBoxLogs;
            }
            set {
                listBoxLogs = value;
            }
        }

        public ListBoxWithAddRemove ListBoxItems
        {
            get {
                return listBoxItems;
            }
            set {
                listBoxItems = value;
            }
        }

        public TextBoxWithDescription TextBoxNextSteps
        {
            get {
                return textBoxNextSteps;
            }
            set {
                textBoxNextSteps = value;
            }
        }

        public TextBoxWithDescription TextBoxDetails
        {
            get {
                return textBoxDetails;
            }
            set {
                textBoxDetails = value;
            }
        }

        public TextBox TextBoxKey
        {
            get { return textBoxKey; }
        }

        private void OnButtonIssueTrackerSearchClick(object sender, RoutedEventArgs e)
        {
            _viewModel.FindInIssueTracker();
        }

        public void OnWindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled) return;

            if (e.Key == Key.I)
            {
                AddItemOrFocus(listBoxItems);
                e.Handled = true;
            }
            else if (e.Key == Key.L)
            {
                AddItemOrFocus(listBoxLogs);
                if (!IsEditing
                    && (!Keyboard.IsKeyDown(Key.LeftShift)
                        && !Keyboard.IsKeyDown(Key.RightShift)))
                {
                    textBoxLogEntry.Focus();
                    e.Handled = true;
                }
            }
        }
    }
}
