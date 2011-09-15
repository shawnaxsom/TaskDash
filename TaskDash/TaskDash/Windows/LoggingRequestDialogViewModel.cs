using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TaskDash.Core;
using TaskDash.Core.ExtensionMethods;
using TaskDash.Core.Models.Tasks;
using TaskDash.ViewModels;

namespace TaskDash
{
    public class LoggingRequestDialogViewModel : ViewModelBase<MainWindowViewModel>
    {
        public LoggingRequestDialogViewModel(Window window, Logs<Log> currentTaskLogs)
        {
            CurrentTaskLogs = currentTaskLogs;

            _onLoggingRequestDialogClickCommand = new OnLoggingRequestDialogClickCommand(window, this);
        }

        public Logs<Log> CurrentTaskLogs { get; private set; }

        public Log MostRecentLog
        {
            get { return CurrentTaskLogs.GetMostRecentLog(); }
        }

        public string NewLogEntry { get; set; }
        public bool DeserveBreak { get; set; }
        private string _breakTime = "00:03:00";
        public string BreakTime
        {
            get { return _breakTime; }
            set { _breakTime = value; }
        }

        public Visibility ShowBreakTime
        {
            get { return DeserveBreak.ToVisible(); }
            set { DeserveBreak = Bools.FromVisible(value); }
        }

        public OnLoggingRequestDialogClickCommand OnLoggingRequestDialogClickCommand
        {
            get { return _onLoggingRequestDialogClickCommand; }
        }

        private OnLoggingRequestDialogClickCommand _onLoggingRequestDialogClickCommand;
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
                throw new NotImplementedException();
            }
            else
            {
                _window.Close();
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
