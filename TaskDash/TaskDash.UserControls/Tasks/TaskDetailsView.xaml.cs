﻿using System;
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

            _viewModel = new TaskDetailsViewModel(this, taskList);

            DataContext = ViewModel;
        }

        private TaskDetailsViewModel _viewModel;

        private void OnButtonStartStopClick(object sender, RoutedEventArgs e)
        {
            var task = ViewModel.SelectedTask;
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
            ViewModel.RefreshItems();
        }

        private void OnButtonResetClick(object sender, RoutedEventArgs e)
        {
            var task = ViewModel.SelectedTask;
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
            ViewModel.OpenIssueTracker();
        }

        private void OnListBoxItemsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ViewModel.ShowEditTaskItemDialog();
            }
        }

        private void EditTaskItemClick(object sender, RoutedEventArgs e)
        {
            ViewModel.ShowEditTaskItemDialog();
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

        public TaskDetailsViewModel ViewModel
        {
            get { return _viewModel; }
        }

        private void OnButtonIssueTrackerSearchClick(object sender, RoutedEventArgs e)
        {
            ViewModel.FindInIssueTracker();
        }

        public void OnWindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled) return;

            if (!IsEditing
                     && e.Key == Key.I)
            {
                AddItemOrFocus(listBoxItems);
                e.Handled = true;
            }
            else if (!IsEditing
                     && e.Key == Key.L)
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

        private void listBoxLogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO: This shouldn't be necessary, do it in XAML
            if (e.AddedItems.Count == 0)
            {
                _viewModel.SelectedLog = null;
            }
            else
            {
                _viewModel.SelectedLog = (Log) e.AddedItems[0];
            }
        }
    }
}
