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
using TaskDash.Controls;
using TaskDash.Core.Models.Tasks;

namespace TaskDash.Windows.Main
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
            if (DockingState == MainWindow.WindowDockingState.AddingDockingControls)
            {
                var source = (TextBoxWithDescription)e.Source;

                _dockWindow.AddControl(source);
            }
        }
    }
}
