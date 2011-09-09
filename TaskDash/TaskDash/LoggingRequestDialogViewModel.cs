using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TaskDash.Core;
using TaskDash.Core.ExtensionMethods;
using TaskDash.Core.Models.Tasks;

namespace TaskDash
{
    public class LoggingRequestDialogViewModel : ViewModelBase<MainWindowViewModel>
    {
        public LoggingRequestDialogViewModel(Logs<Log> currentTaskLogs)
        {
            CurrentTaskLogs = currentTaskLogs;
        }

        public Logs<Log> CurrentTaskLogs { get; private set; }

        public Log MostRecentLog
        {
            get { return CurrentTaskLogs.GetMostRecentLog(); }
        }

        public string NewLogEntry { get; private set; }
        public bool DeserveBreak { get; private set; }
        private string _breakTime = "00:05:00";
        public string BreakTime
        {
            get { return _breakTime; }
            private set { _breakTime = value; }
        }

        public Visibility ShowBreakTime
        {
            get { return DeserveBreak.ToVisible(); }
        }

        public OnLoggingRequestDialogClickCommand _onLoggingRequestDialogClickCommand;
    }

    public class OnLoggingRequestDialogClickCommand : ICommand
    {
        private readonly Window _window;
        private readonly LoggingRequestDialogViewModel _viewModel;

        public OnLoggingRequestDialogClickCommand(
            Window window,
            LoggingRequestDialogViewModel viewModel)
        {
            _window = window;
            _viewModel = viewModel;
        }

        public void Execute(object parameter)
        {
            CreateNewLog();

            HandleBreak();
        }

        private void HandleBreak()
        {
            if (_viewModel.DeserveBreak)
            {
                
            }
            else
            {
                
            }
        }

        private void CreateNewLog()
        {
            if (string.IsNullOrEmpty(_viewModel.NewLogEntry))
            {
                return;
            }

            Log newLog = new Log()
                             {
                                 EntryDate = DateTime.Now,
                                 Entry = _viewModel.NewLogEntry
                             };

            _viewModel.CurrentTaskLogs.Add(newLog);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
