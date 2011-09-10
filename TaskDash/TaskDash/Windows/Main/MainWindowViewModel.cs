using System.Windows;
using System.Windows.Data;
using TaskDash.Core;
using TaskDash.Core.Models.Tasks;
using TaskDash.ViewModels;

namespace TaskDash
{
    /// <summary>
    /// http://en.wikipedia.org/wiki/Model_View_ViewModel
    /// </summary>
    public class MainWindowViewModel : ViewModelBase<MainWindowViewModel>
    {
        public MainWindowViewModel()
        {
            Tasks = new Task().GetTasks();
            FilteredTasks = new CollectionViewSource { Source = this.Tasks };

            _autoLogger = new AutoLogger()
                              {
                                  TimeBetweenPrompts = "00:00:10"
                              };
            _autoLogger.LoggingRequested += new LoggingRequestedEventHandler(_autoLogger_LoggingRequested);
            _autoLogger.Start();
        }

        private void LoadTrayIcon()
        {
            _notifyIcon = new NotifyIcon
            {
                BalloonTipTitle = Properties.Resources.MainWindow_LoadTrayIcon_TaskDash_,
                Text = Properties.Resources.MainWindow_LoadTrayIcon_TaskDash_,
                Icon = new Icon(@"C:\Users\Shawn.Axsom\Desktop\TaskDash.ico")
            };

            _notifyIcon.Click += OnNotifyIconClick;
        }

        private void _clipboardMonitor_ClipboardData(object sender, RoutedEventArgs e)
        {
            e.Handled = true;


            Task task = SelectedTask;

            if (task == null) return;

            task.HandleClipboardData(_clipboardMonitor);
        }

        private void _autoLogger_LoggingRequested(object sender, LoggingRequestedEventHandlerArgs args)
        {
            //TODO: CurrentTask is always null
            if (CurrentTask == null) return;

            LoggingRequestDialogViewModel viewModel = new LoggingRequestDialogViewModel(CurrentTask.Logs);
            LoggingRequestDialog dialog = new LoggingRequestDialog(viewModel);
            dialog.Show();
        }

        //private Task _currentTask;
        //public Task CurrentTask
        //{
        //    get { return _currentTask; }
        //    set
        //    {
        //        _currentTask = value;
        //        OnPropertyChanged("CurrentTask");
        //        OnPropertyChanged("CurrentTask.Key");
        //        OnPropertyChanged("Key");
        //    }
        //}
        private static readonly DependencyProperty CurrentTaskProperty =
           DependencyProperty.Register("CurrentTask", typeof(Task), typeof(MainWindowViewModel),
                                       new PropertyMetadata(null));
        public Task CurrentTask
        {
            get { return (Task) GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }
        
        private AutoLogger _autoLogger;

        
    }
}
