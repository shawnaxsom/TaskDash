using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TaskDash.CustomControls;
using TaskDash.Core.Models.Tasks;
using TaskDash.Core.Services;
using TaskDash.Notifications;
using TaskDash.ViewModels;
using Control = System.Windows.Controls.Control;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using TextBox = System.Windows.Controls.TextBox;

namespace TaskDash
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow, IDisposable
    {
        #region WindowDockingState enum

        public enum WindowDockingState
        {
            Normal,
            AddingDockingControls,
            Docked
        }

        #endregion

        private readonly ClipboardMonitorService _clipboardMonitor;
        private readonly List<Control> _controlCycle;

        private readonly NotificationManager _notificationManager;
        private readonly SaveService _saveService;
        private readonly MainWindowViewModel _tasks;
        private DockWindow _dockWindow;
        private bool _docking;
        private WindowState _storedWindowState = WindowState.Normal;

        static MainWindow()
        {
            Instance = new MainWindow();
        }

        public MainWindow()
        {
            InitializeComponent();


            SetParentWindow();


            Icon = new BitmapImage(new Uri(@"C:\Users\Shawn.Axsom\Desktop\TaskDash.ico"));
            LoadTrayIcon();
            ShowTrayIcon(true);

            _tasks = new MainWindowViewModel();


            _clipboardMonitor = new ClipboardMonitorService();
            _clipboardMonitor.ClipboardData += _clipboardMonitor_ClipboardData;

            DataContext = _tasks;


            _notificationManager = new NotificationManager(_tasks);
            _notificationManager.Start();

            _viewModel.Search();

            _saveService = new SaveService(_tasks);
            _saveService.Start();


            _controlCycle = new List<Control>
                                {
                                    listBoxTasks,
                                    listBoxItems,
                                    listBoxLogs
                                };
        }

        public static MainWindow Instance { get; private set; }

        /// <summary>
        /// Checks if focused control is a text box.
        /// This includes text boxes in user controls.
        /// </summary>
        public bool IsEditing
        {
            get
            {
                Type type = Keyboard.FocusedElement.GetType();

                return type == typeof (TextBox);
            }
        }

        public Task SelectedTask
        {
            get { return (Task) listBoxTasks.SelectedItem; }
        }

        public WindowDockingState DockingState
        {
            get
            {
                if (_dockWindow != null)
                {
                    if (WindowState == WindowState.Normal
                        || WindowState == WindowState.Maximized)
                    {
                        return WindowDockingState.AddingDockingControls;
                    }
                    else
                    {
                        return WindowDockingState.Docked;
                    }
                }
                else
                {
                    return WindowDockingState.Normal;
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            //if (_notifyIcon != null)
            //{
            //    _notifyIcon.Visible = false;
            //    _notifyIcon.Dispose();
            //    _notifyIcon = null;
            //}
        }

        #endregion

        #region IMainWindow Members

        
        #endregion

        private void SetParentWindow()
        {
            listBoxTasks.ParentWindow = this;
        }

        

        private void Save()
        {
            // Make sure you exit the current box if it has edits, before saving
            textBoxDetails.Focus();
            textBoxKey.Focus();

            _saveService.Save();
        }

        #region Event Handlers
        private void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Save();
        }


        private void OnWindowKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (e.Key == Key.W)
                {
                    Close();
                }
                else if (e.Key == Key.S)
                {
                    Save();
                }
            }
            else
            {
                if (e.Key == Key.Escape)
                {
                    if (listBoxTasks.ChildHasFocus)
                    {
                        WindowState = WindowState.Minimized;
                    }
                    else
                    {
                        listBoxTasks.Focus();
                    }
                }
                else if (e.Key == Key.T)
                {
                    AddItemOrFocus(listBoxTasks);
                }
                else if (e.Key == Key.I)
                {
                    AddItemOrFocus(listBoxItems);
                }
                else if (e.Key == Key.L)
                {
                    AddItemOrFocus(listBoxLogs);
                    if (!IsEditing
                        && (!Keyboard.IsKeyDown(Key.LeftShift)
                            && !Keyboard.IsKeyDown(Key.RightShift)))
                    {
                        textBoxLogEntry.Focus();
                    }
                }
                else if (!IsEditing
                         && e.Key == Key.Oem2) // Forward Slash
                {
                    textBoxSearch.Focus();
                }
                else if (!IsEditing
                         && (e.Key == Key.N))
                {
                    Cycle(1);
                }
                else if (!IsEditing
                         && (e.Key == Key.P))
                {
                    Cycle(-1);
                }
                else if (!IsEditing
                         && (e.Key == Key.F5))
                {
                    Refresh();
                }
            }
        }

        private void OnStateChanged(object sender, EventArgs args)
        {
            if (WindowState == WindowState.Minimized)
            {
                //Hide();
            }
            else
            {
                _storedWindowState = WindowState;
            }
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            //CheckTrayIcon();
        }

        #endregion

        private void Refresh()
        {
            //Search();

            //RefreshTaskBindings();
        }

        private void Cycle(int indexChange)
        {
            var control = (Control) Keyboard.FocusedElement;
            //Control parentControl = (Control) control.Template.VisualTree.;
            //VirtualizingStackPanel parentControl = (VirtualizingStackPanel)VisualTreeHelper.GetParentVisualTreeHelper.GetParent(VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(control)));

            DependencyObject parentControl = VisualTreeHelper.GetParent(control);

            while (parentControl != null
                   && parentControl.GetType() != typeof (ListBoxWithAddRemove))
            {
                parentControl = VisualTreeHelper.GetParent(parentControl);
            }

            var listBoxControl = (parentControl as ListBoxWithAddRemove);

            if (!_controlCycle.Contains(listBoxControl))
            {
                listBoxTasks.Focus();
            }
            else
            {
                int newIndex = _controlCycle.IndexOf(listBoxControl) + indexChange;
                if (newIndex > _controlCycle.Count - 1)
                {
                    newIndex -= _controlCycle.Count;
                }
                else if (newIndex < 0)
                {
                    newIndex += _controlCycle.Count;
                }

                _controlCycle[newIndex].Focus();
            }
        }

        private void AddItemOrFocus(ListBoxWithAddRemove listBox)
        {
            if (!IsEditing)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift)
                    || Keyboard.IsKeyDown(Key.RightShift))
                {
                    listBox.Focus();
                }
                else
                {
                    listBox.SimulateClick(listBox.AddButton);
                }
            }
        }

        private void OnNotifyIconClick(object sender, EventArgs e)
        {
            if (DockingState == WindowDockingState.Normal
                || DockingState == WindowDockingState.AddingDockingControls)
            {
                Show();
                WindowState = _storedWindowState;
                Activate();
            }
            else
            {
                _dockWindow.Show();
                _dockWindow.Activate();
            }
        }

        
        

        private void EditTaskItemClick(object sender, RoutedEventArgs e)
        {
            ShowEditTaskItemDialog();
        }

        private void ShowEditTaskItemDialog()
        {
            Task task = SelectedTask;
            var listBox = listBoxItems;
            var item = (TaskItem) listBox.SelectedItem;

            var editTaskItem = new EditTaskItem(task.FilteredItems.View, item);
            editTaskItem.Show();
        }

        private void RefreshTaskBindings()
        {
            Task task = SelectedTask;
            if (task != null)
            {
                // TODO: I shouldn't have to do this. 
                // TODO: How do I do ItemsSource="{Binding Links}" but have ADD button be able to get the Links collection rather than the Tasks collection?
                DataContext = task;

                var phrasesView = (ListCollectionView) task.FilteredPhrases.View;
                if (phrasesView.IsAddingNew)
                {
                    phrasesView.CommitNew();
                }
                phrasesView.SortDescriptions.Add(new SortDescription("Occurances", ListSortDirection.Descending));
                listBoxPhrases.DataContext = task.FilteredPhrases;

                var wordsView = (ListCollectionView) task.FilteredWords.View;
                if (wordsView.IsAddingNew)
                {
                    wordsView.CommitNew();
                }
                wordsView.SortDescriptions.Add(new SortDescription("Occurances", ListSortDirection.Descending));
                listBoxWords.DataContext = task.FilteredWords;

                var linksView = (ListCollectionView) task.FilteredLinks.View;
                if (linksView.IsAddingNew)
                {
                    linksView.CommitNew();
                }
                linksView.SortDescriptions.Add(new SortDescription("Occurances", ListSortDirection.Descending));
                listBoxLinks.DataContext = task.FilteredLinks;

                var logsview = (ListCollectionView) task.FilteredLogs.View;
                if (logsview.IsAddingNew)
                {
                    logsview.CommitNew();
                }
                listBoxLogs.DataContext = task.FilteredLogs;
                task.FilteredLogs.Filter += OnFilteredLogsFilter;

                task.Logs.RefreshLogTagList();
                comboBoxLogTagsFilter.DataContext = task.Logs;

                linksView = (ListCollectionView) task.FilteredItems.View;
                if (linksView.IsAddingNew)
                {
                    linksView.CommitNew();
                }
                listBoxItems.DataContext = task.FilteredItems;
                task.FilteredItems.Filter += OnFilteredItemsFilter;
            }
        }

        

        //private void OnListBoxLogsSelectionChanged(object sender, RoutedEventArgs e)
        //{
        //    var log = (Log) listBoxLogs.SelectedItem;
        //    textBoxLogTags.DataContext = log;
        //    textBoxLogEntry.DataContext = log;
        //}

        private void AddDefaultDockingControls()
        {
            if (_dockWindow == null) return;


            //_dockWindow.AddControl(textBoxNextSteps);
            //_dockWindow.AddControl(listBoxItems);
            //_dockWindow.AddControl(listBoxLogs);
            //_dockWindow.AddControl(textBoxLogEntry);
            //_dockWindow.AddControl(listBoxLinks);
            _dockWindow.AddControls(_viewModel.DefaultDockingControls);
        }

        private void OnAccordianButtonClick(object sender, RoutedEventArgs e)
        {
            //var window = new AccordianWindow(_tasks)
            //                 {
            //                     WindowStartupLocation = WindowStartupLocation.CenterOwner
            //                 };
            //window.Show();
            //Hide();

            if (DockingState == WindowDockingState.Normal)
            {
                _dockWindow = new DockWindow(this)
                                  {
                                      WindowStartupLocation = WindowStartupLocation.Manual,
                                      Top = 0,
                                      Height = SystemParameters.PrimaryScreenHeight - 30
                                  };
                _dockWindow.Left = SystemParameters.PrimaryScreenWidth - _dockWindow.Width;
                _dockWindow.Show();

                _dockWindow.Closed += _dockWindow_Closed;

                AddDefaultDockingControls();
            }
            else
            {
                Hide();
                _dockWindow.Activate();
            }
        }

        private void _dockWindow_Closed(object sender, EventArgs e)
        {
            _dockWindow = null;
        }

        private void OnListBoxTasksKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter
                && !listBoxTasks.IsEditingSelectedItem)
            {
                textBoxKey.Focus();
            }
        }

        private void OnListBoxItemsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ShowEditTaskItemDialog();
            }
        }

        //private void OnListBoxLogsKeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter
        //        && !listBoxLogs.IsEditingSelectedItem)
        //    {
        //        textBoxLogEntry.Focus();
        //    }
        //}

        private void OnWindowClosed(object sender, EventArgs e)
        {
            Dispose();
        }

        private void ListBoxWithAddRemoveMouseDown(object sender, RoutedEventArgs e)
        {
            if (DockingState == WindowDockingState.AddingDockingControls)
            {
                var source = (ListBoxWithAddRemove) e.Source;

                _dockWindow.AddControl(source);
            }
        }

        //private void ListBoxWithAddRemoveControlFocused(object sender, RoutedEventArgs e)
        //{
        //    if (DockingState == WindowDockingState.AddingDockingControls)
        //    {
        //        var source = (ListBoxWithAddRemove) e.Source;

        //        _dockWindow.AddControl(source);
        //    }
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        

        internal void SelectTask(Task Task)
        {
            throw new NotImplementedException();
        }

        public void OpenIssueTracker()
        {
            throw new NotImplementedException();
        }
    }
}