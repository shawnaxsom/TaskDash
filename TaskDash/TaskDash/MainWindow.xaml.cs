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
using TaskDash.Controls;
using TaskDash.Core.Models.Sorting;
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
        private readonly NotificationManager _notificationManager;
        private readonly TaskViewModel _tasks;
        private NotifyIcon _notifyIcon;
        private WindowState _storedWindowState = WindowState.Normal;
        private List<Control> _controlCycle;

        private readonly SaveService _saveService;
        private ClipboardMonitorService _clipboardMonitor;
        private bool _docking;
        private DockWindow _dockWindow;

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

            _tasks = new TaskViewModel();

            
            _clipboardMonitor = new ClipboardMonitorService();
            _clipboardMonitor.ClipboardData += new RoutedEventHandler(_clipboardMonitor_ClipboardData);

            DataContext = _tasks;
            listBoxTasks.DataContext = _tasks.FilteredTasks; // TODO: Is there any way to bind this behind the scenes?
            comboBoxTagsFilter.DataContext = _tasks.Tasks.TagList;
            comboBoxSortBy.DataContext = TaskComparer.Instance;


            _notificationManager = new NotificationManager(_tasks);
            _notificationManager.Start();

            _tasks.FilteredTasks.Filter += OnFilteredTasksFilter;

            Search();

            _saveService = new SaveService(_tasks);
            _saveService.Start();


            _controlCycle = new List<Control>
                                {
                                    listBoxTasks, 
                                    listBoxItems, 
                                    listBoxLogs
                                };
        }

        private void SetParentWindow()
        {
            listBoxTasks.ParentWindow = this;
        }

        void _clipboardMonitor_ClipboardData(object sender, RoutedEventArgs e)
        {
            e.Handled = true;


            Task task = SelectedTask;

            if (task == null) return;

            task.HandleClipboardData(_clipboardMonitor);
        }

        public static MainWindow Instance { get; private set; }

        private void OnFilteredTasksFilter(object sender, FilterEventArgs e)
        {
            e.Accepted = false;
            var task = (Task)e.Item;


            string search = textBoxSearch.Text;
            string tag = (comboBoxTagsFilter.SelectedValue == null ? "" : comboBoxTagsFilter.SelectedValue.ToString());


            if (task.CloselyMatches(search)
                && (checkBoxCurrentFilter.IsChecked == false
                    || task.Current == checkBoxCurrentFilter.IsChecked)
                && (task.Someday == checkBoxSomedayFilter.IsChecked)
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

            if (e.Key == Key.Enter)
            {
                SelectFirstTask();
            }
        }

        private void Search()
        {
            if (_tasks == null) return;

            var view = (ListCollectionView)_tasks.FilteredTasks.View;

            if (view.IsAddingNew)
            {
                view.CommitNew();
            }

            TaskComparer sorter = (TaskComparer)comboBoxSortBy.SelectedValue ?? TaskComparer.Default;

            if (sorter.MainMethod != TaskComparer.TaskComparingMethod.None)
            {
                sorter.MatchPhrase = textBoxSearch.Text;
                view.CustomSort = sorter;
            }

            // Search
            view.Refresh();
        }

        private void Save()
        {
            // Make sure you exit the current box if it has edits, before saving
            textBoxDetails.Focus();
            textBoxKey.Focus();

            _saveService.Save();
        }

        private void OnTextBoxSearchKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SelectFirstTask();
            }
        }

        private void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Save();
        }

        private void OnListBoxTasksLoaded(object sender, RoutedEventArgs e)
        {
            SelectFirstTask();
            textBoxKey.Focus();
        }

        private void SelectFirstTask()
        {
            if (listBoxTasks.Items.Count > 0)
            {
                ListBoxItem item = listBoxTasks.GetFirstListBoxItemFromListBox();
                // Force refresh of selection. 
                // Otherwise the program starts up without anything selected.
                item.IsSelected = false;
                item.IsSelected = true;
                item.Focus();
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
                        this.WindowState = WindowState.Minimized;
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
                    if (!this.IsEditing
                        && (!Keyboard.IsKeyDown(Key.LeftShift)
                            && !Keyboard.IsKeyDown(Key.RightShift)))
                    {
                        textBoxLogEntry.Focus();
                    }
                }
                else if (!this.IsEditing
                        && e.Key == Key.Oem2) // Forward Slash
                {
                    textBoxSearch.Focus();
                }
                else if (!this.IsEditing
                        && (e.Key == Key.N))
                {
                    Cycle(1);
                }
                else if (!this.IsEditing
                        && (e.Key == Key.P))
                {
                    Cycle(-1);
                }
                else if (!this.IsEditing
                        && (e.Key == Key.F5))
                {
                    Refresh();
                }
            }
        }

        private void Refresh()
        {
            Search();

            //RefreshTaskBindings();
        }

        private void Cycle(int indexChange)
        {
            Control control = (Control) Keyboard.FocusedElement;
            //Control parentControl = (Control) control.Template.VisualTree.;
            //VirtualizingStackPanel parentControl = (VirtualizingStackPanel)VisualTreeHelper.GetParentVisualTreeHelper.GetParent(VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(control)));

            var parentControl = VisualTreeHelper.GetParent(control);

            while (parentControl != null
                && parentControl.GetType() != typeof(ListBoxWithAddRemove))
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
            if (!this.IsEditing)
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

        /// <summary>
        /// Checks if focused control is a text box.
        /// This includes text boxes in user controls.
        /// </summary>
        public bool IsEditing
        {
            get
            {
                Type type = Keyboard.FocusedElement.GetType();

                return type == typeof(TextBox);
            }
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

        private void CheckTrayIcon()
        {
            ShowTrayIcon(!IsVisible);
        }

        private void ShowTrayIcon(bool show)
        {
            if (_notifyIcon != null)
                _notifyIcon.Visible = show;
        }

        private void OnCheckBoxCurrentFilterChecked(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void OnCheckBoxSomedayFilterChecked(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void EditTaskItemClick(object sender, RoutedEventArgs e)
        {
            ShowEditTaskItemDialog();
        }

        private void ShowEditTaskItemDialog()
        {
            var task = SelectedTask;
            var listBox = listBoxItems;
            var item = (TaskItem)listBox.SelectedItem;

            var editTaskItem = new EditTaskItem(task.FilteredItems.View, item);
            editTaskItem.Show();
        }

        public void OnListBoxTasksSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //RefreshTaskBindings();

            UpdatedSelected(e);
        }

        private void RefreshTaskBindings()
        {
            var task = SelectedTask;
            if (task != null)
            {
                // TODO: I shouldn't have to do this. 
                // TODO: How do I do ItemsSource="{Binding Links}" but have ADD button be able to get the Links collection rather than the Tasks collection?
                DataContext = task;
                
                var phrasesView = (ListCollectionView)task.FilteredPhrases.View;
                if (phrasesView.IsAddingNew)
                {
                    phrasesView.CommitNew();
                }
                phrasesView.SortDescriptions.Add(new SortDescription("Occurances", ListSortDirection.Descending));
                listBoxPhrases.DataContext = task.FilteredPhrases;

                var wordsView = (ListCollectionView)task.FilteredWords.View;
                if (wordsView.IsAddingNew)
                {
                    wordsView.CommitNew();
                }
                wordsView.SortDescriptions.Add(new SortDescription("Occurances", ListSortDirection.Descending));
                listBoxWords.DataContext = task.FilteredWords;

                var linksView = (ListCollectionView)task.FilteredLinks.View;
                if (linksView.IsAddingNew)
                {
                    linksView.CommitNew();
                }
                linksView.SortDescriptions.Add(new SortDescription("Occurances", ListSortDirection.Descending));
                listBoxLinks.DataContext = task.FilteredLinks;

                var logsview = (ListCollectionView)task.FilteredLogs.View;
                if (logsview.IsAddingNew)
                {
                    logsview.CommitNew();
                }
                listBoxLogs.DataContext = task.FilteredLogs;
                task.FilteredLogs.Filter += OnFilteredLogsFilter;

                task.Logs.RefreshLogTagList();
                comboBoxLogTagsFilter.DataContext = task.Logs;

                linksView = (ListCollectionView)task.FilteredItems.View;
                if (linksView.IsAddingNew)
                {
                    linksView.CommitNew();
                }
                listBoxItems.DataContext = task.FilteredItems;
                task.FilteredItems.Filter += OnFilteredItemsFilter;
            }
        }

        void OnFilteredItemsFilter(object sender, FilterEventArgs e)
        {
            e.Accepted = false;
            var taskItem = (TaskItem)e.Item;
            bool? showCompleted = checkBoxItemsCompletedFilter.IsChecked;

            string search = textBoxSearch.Text;
            string tag = (comboBoxTagsFilter.SelectedValue == null ? "" : comboBoxTagsFilter.SelectedValue.ToString());


            if (taskItem.Completed == showCompleted)
            {
                e.Accepted = true;
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


        private static void UpdatedSelected(SelectionChangedEventArgs e)
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
            var log = (Log)listBoxLogs.SelectedItem;
            textBoxLogTags.DataContext = log;
            textBoxLogEntry.DataContext = log;
        }

        private void OnComboBoxTagsFilterSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshLogs();
        }

        private void OnButtonStartStopClick(object sender, RoutedEventArgs e)
        {
            var task = SelectedTask;
            if (task != null)
            {
                task.ToggleTimer();
            }
        }

        public Task SelectedTask
        {
            get { return (Task) listBoxTasks.SelectedItem; }
        }

        private void OnComboBoxSortBySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void OnButtonResetClick(object sender, RoutedEventArgs e)
        {
            var task = SelectedTask;
            if (task != null)
            {
                task.ResetTimer();
            }
        }

        private void OnCheckBoxCompletedFilterChecked(object sender, RoutedEventArgs e)
        {
            Search();
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
                                      Height = SystemParameters.PrimaryScreenHeight-30
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

        private void AddDefaultDockingControls()
        {
            if (_dockWindow == null) return;


            _dockWindow.AddControl(textBoxNextSteps);
            _dockWindow.AddControl(listBoxItems);
            _dockWindow.AddControl(listBoxLogs);
            _dockWindow.AddControl(textBoxLogEntry);
            _dockWindow.AddControl(listBoxLinks);
        }

        void _dockWindow_Closed(object sender, EventArgs e)
        {
            _dockWindow = null;
        }

        private void OnCheckBoxItemsCompletedFilterChecked(object sender, RoutedEventArgs e)
        {
            RefreshItems();
        }

        private void RefreshItems()
        {
            if (_tasks == null) return;

            var task = SelectedTask;
            if (task == null) return;


            var view = (ListCollectionView)task.FilteredItems.View;

            // Search
            view.Refresh();

            //SelectFirstTask();
        }

        private void RefreshLogs()
        {
            if (_tasks == null) return;

            var task = SelectedTask;
            if (task == null) return;


            var view = (ListCollectionView)task.FilteredLogs.View;

            // Search
            view.Refresh();
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

        private void OnListBoxLogsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter
                && !listBoxLogs.IsEditingSelectedItem)
            {
                textBoxLogEntry.Focus();
            }
        }

        private void ListBoxWithAddRemoveMouseDown(object sender, RoutedEventArgs e)
        {
            if (DockingState == WindowDockingState.AddingDockingControls)
            {
                var source = (ListBoxWithAddRemove) e.Source;

                _dockWindow.AddControl(source);
            }
        }

        private void TextBoxWithDescriptionControlFocused(object sender, RoutedEventArgs e)
        {
            if (DockingState == WindowDockingState.AddingDockingControls)
            {
                var source = (TextBoxWithDescription)e.Source;

                _dockWindow.AddControl(source);
            }
        }

        public enum WindowDockingState
        {
            Normal,
            AddingDockingControls,
            Docked
        }
        public WindowDockingState DockingState
        {
            get
            {
                if (_dockWindow != null)
                {
                    if (WindowState == System.Windows.WindowState.Normal
                        || WindowState == System.Windows.WindowState.Maximized)
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

        private void ListBoxWithAddRemoveControlFocused(object sender, RoutedEventArgs e)
        {
            if (DockingState == WindowDockingState.AddingDockingControls)
            {
                var source = (ListBoxWithAddRemove)e.Source;

                _dockWindow.AddControl(source);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonIssueTracker_Click(object sender, RoutedEventArgs e)
        {
            OpenIssueTracker();
        }

        public void OpenIssueTracker()
        {
            Process.Start("http://devjira/browse/" + SelectedTask.Key);
        }

        public void Dispose()
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
                _notifyIcon = null;
            }
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}