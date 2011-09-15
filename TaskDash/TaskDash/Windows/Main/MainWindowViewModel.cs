using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TaskDash.Core;
using TaskDash.Core.Models.Tasks;
using TaskDash.Core.Services;
using TaskDash.CustomControls;
using TaskDash.UserControls;
using TaskDash.UserControls.Tasks;
using TaskDash.ViewModels;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace TaskDash
{
    /// <summary>
    /// http://en.wikipedia.org/wiki/Model_View_ViewModel
    /// </summary>
    public class MainWindowViewModel : ViewModelBase<MainWindowViewModel>
    {
        private readonly MainWindow _view;
        private readonly TaskListUserControlView _userControlViewList;
        private readonly TaskDetailsView _viewDetails;
        private NotifyIcon _notifyIcon;
        private DockWindow _dockWindow;
        private WindowState _storedWindowState = WindowState.Normal;

        public MainWindowViewModel(MainWindow view, TaskListUserControlView userControlViewList, TaskDetailsView viewDetails)
        {
            _view = view;
            _userControlViewList = userControlViewList;
            _viewDetails = viewDetails;

            _autoLogger = new AutoLogger()
                              {
                                  TimeBetweenPrompts = "00:00:05"
                              };
            _autoLogger.LoggingRequested += new LoggingRequestedEventHandler(OnAutoLoggerLoggingRequested);
            _autoLogger.Start();


            _clipboardMonitor = new ClipboardMonitorService();
            _clipboardMonitor.ClipboardData += _clipboardMonitor_ClipboardData;


            LoadTrayIcon();
            ShowTrayIcon(true);
        }

        //protected BitmapImage Icon { get { return new BitmapImage(new Uri(@"C:\Users\Shawn.Axsom\Desktop\TaskDash.ico")); } }

        public TaskDetailsView ViewDetails
        {
            [DebuggerStepThrough]
            get { return _viewDetails; }
        }

        public TaskListUserControlView UserControlViewList
        {
            [DebuggerStepThrough]
            get { return _userControlViewList; }
        }

        private void ShowTrayIcon(bool show)
        {
            if (_notifyIcon != null)
                _notifyIcon.Visible = show;
        }

        private void LoadTrayIcon()
        {
            _notifyIcon = new NotifyIcon
            {
                BalloonTipTitle = Properties.Resources.MainWindow_LoadTrayIcon_TaskDash_,
                Text = Properties.Resources.MainWindow_LoadTrayIcon_TaskDash_,
                //Icon = new Icon(@"C:\Users\Shawn.Axsom\Desktop\TaskDash.ico")
            };

            _notifyIcon.Click += OnNotifyIconClick;
        }

        private void OnNotifyIconClick(object sender, EventArgs e)
        {
            if (DockingState == MainWindow.WindowDockingState.Normal
                || DockingState == MainWindow.WindowDockingState.AddingDockingControls)
            {
                _view.Show();
                WindowState = StoredWindowState;
                Activate();
            }
            else
            {
                _dockWindow.Show();
                _dockWindow.Activate();
            }
        }

        public MainWindow.WindowDockingState DockingState
        {
            get
            {
                if (_dockWindow != null)
                {
                    if (WindowState == WindowState.Normal
                        || WindowState == WindowState.Maximized)
                    {
                        return MainWindow.WindowDockingState.AddingDockingControls;
                    }
                    else
                    {
                        return MainWindow.WindowDockingState.Docked;
                    }
                }
                else
                {
                    return MainWindow.WindowDockingState.Normal;
                }
            }
        }

        protected WindowState WindowState
        {
            get { return _view.WindowState; }
            set { _view.WindowState = value; }
        }

        private void Activate()
        {
            _view.Activate();
        }

   


        private void _clipboardMonitor_ClipboardData(object sender, RoutedEventArgs e)
        {
            e.Handled = true;


            Task task = _view.SelectedTask;

            if (task == null) return;

            task.HandleClipboardData(_clipboardMonitor);
        }

        private void OnAutoLoggerLoggingRequested(object sender, LoggingRequestedEventHandlerArgs args)
        {
            if (SelectedTask == null) return;

            LoggingRequestDialog dialog = new LoggingRequestDialog(SelectedTask.Logs);
            dialog.ShowDialog();
        }

        public Task SelectedTask
        {
            get { return _viewDetails.ViewModel.SelectedTask; }
        }

        public List<System.Windows.Controls.Control> DefaultDockingControls
        {
            get
            {
                List<System.Windows.Controls.Control> controls = new List<System.Windows.Controls.Control>();
                controls.Add(_viewDetails.TextBoxNextSteps);
                controls.Add(_viewDetails.ListBoxItems);
                controls.Add(_viewDetails.ListBoxLogs);
                controls.Add(_viewDetails.TextBoxLogEntry);
                return controls;
            }
        }

        public Tasks Tasks
        {
            get { return _userControlViewList.ViewModel.Tasks; }
        }

        public WindowState StoredWindowState
        {
            get { return _storedWindowState; }
            set { _storedWindowState = value; }
        }

        private AutoLogger _autoLogger;
        private ClipboardMonitorService _clipboardMonitor;
        
        public void Search()
        {
            _userControlViewList.ViewModel.Search();
        }

        private void AddDefaultDockingControls()
        {
            if (_dockWindow == null) return;


            //_dockWindow.AddControl(textBoxNextSteps);
            //_dockWindow.AddControl(listBoxItems);
            //_dockWindow.AddControl(listBoxLogs);
            //_dockWindow.AddControl(textBoxLogEntry);
            //_dockWindow.AddControl(listBoxLinks);
            _dockWindow.AddControls(DefaultDockingControls);
        }

        public void OnWindowKeyDown(object sender, KeyEventArgs e)
        {
            _userControlViewList.OnWindowKeyDown(sender, e);
            _viewDetails.OnWindowKeyDown(sender, e);
        }

        public void HandleAccordianButtonClick()
        {
            //var window = new AccordianWindow(_viewModel)
            //                 {
            //                     WindowStartupLocation = WindowStartupLocation.CenterOwner
            //                 };
            //window.Show();
            //Hide();

            if (DockingState == MainWindow.WindowDockingState.Normal)
            {
                _dockWindow = new DockWindow(_view)
                {
                    WindowStartupLocation = WindowStartupLocation.Manual,
                    Top = 0,
                    Height = SystemParameters.PrimaryScreenHeight - 30
                };
                _dockWindow.Left = SystemParameters.PrimaryScreenWidth - _dockWindow.Width;
                _dockWindow.Show();

                _dockWindow.Closed += OnDockWindowClosed;

                AddDefaultDockingControls();
            }
            else
            {
                _view.Hide();
                _dockWindow.Activate();
            }
        }

        private void OnDockWindowClosed(object sender, EventArgs e)
        {
            _dockWindow = null;
        }

        public void HandleListBoxWithAddRemoveMouseDown(object sender, RoutedEventArgs e)
        {
            if (DockingState == MainWindow.WindowDockingState.AddingDockingControls)
            {
                var source = (ListBoxWithAddRemove)e.Source;

                _dockWindow.AddControl(source);
            }
        }
    }
}
