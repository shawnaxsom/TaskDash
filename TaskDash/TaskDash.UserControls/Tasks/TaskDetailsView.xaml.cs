using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using TaskDash.CustomControls;
using TaskDash.Core.Models.Tasks;
using TaskDash.UserControls.Tasks;

namespace TaskDash.UserControls
{
    /// <summary>
    /// Interaction logic for TaskDetailsView.xaml
    /// </summary>
    public partial class TaskDetailsView : UserControl
    {
        public TaskDetailsView()
        {
            InitializeComponent();
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

        


        private void OnButtonIssueTrackerSearchClick(object sender, RoutedEventArgs e)
        {
            _viewModel.FindInIssueTracker();
        }
        

    }
}
