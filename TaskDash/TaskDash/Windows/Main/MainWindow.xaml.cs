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

        private readonly List<Control> _controlCycle;

        private readonly NotificationManager _notificationManager;
        private readonly SaveService _saveService;
        private readonly MainWindowViewModel _viewModel;
        private bool _docking;

        static MainWindow()
        {
            Instance = new MainWindow();
        }

        public MainWindow()
        {
            InitializeComponent();


            SetParentWindow();


            

            _viewModel = new MainWindowViewModel(this, taskListView, taskDetailsView);


            DataContext = _viewModel;


            _notificationManager = new NotificationManager(_viewModel);
            _notificationManager.Start();

            _viewModel.Search();

            _saveService = new SaveService(_viewModel.Tasks);
            _saveService.Start();


            _controlCycle = new List<Control>
                                {
                                    _viewModel.UserControlViewList.ListBoxTasks,
                                    _viewModel.ViewDetails.ListBoxItems,
                                    _viewModel.ViewDetails.ListBoxLogs
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
            get { return (Task) ListBoxTasks.SelectedItem; }
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
            ListBoxTasks.ParentWindow = this;
        }

        protected ListBoxTasks ListBoxTasks
        {
            get { return _viewModel.UserControlViewList.ListBoxTasks; }
        }


        private void Save()
        {
            // Make sure you exit the current box if it has edits, before saving
            _viewModel.ViewDetails.TextBoxDetails.Focus();
            _viewModel.ViewDetails.TextBoxKey.Focus();

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
            _viewModel.OnWindowKeyDown(sender, e);

            if (e.Handled) return;



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
                if (!IsEditing
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
                _viewModel.StoredWindowState = WindowState;
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
                ListBoxTasks.Focus();
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

        //private void RefreshTaskBindings()
        //{
        //    Task task = SelectedTask;
        //    if (task != null)
        //    {
        //        // TODO: I shouldn't have to do this. 
        //        // TODO: How do I do ItemsSource="{Binding Links}" but have ADD button be able to get the Links collection rather than the Tasks collection?
        //        DataContext = task;

        //        var phrasesView = (ListCollectionView) task.FilteredPhrases.View;
        //        if (phrasesView.IsAddingNew)
        //        {
        //            phrasesView.CommitNew();
        //        }
        //        phrasesView.SortDescriptions.Add(new SortDescription("Occurances", ListSortDirection.Descending));
        //        _viewModel.ViewDetails.listBoxPhrases.DataContext = task.FilteredPhrases;

        //        var wordsView = (ListCollectionView) task.FilteredWords.View;
        //        if (wordsView.IsAddingNew)
        //        {
        //            wordsView.CommitNew();
        //        }
        //        wordsView.SortDescriptions.Add(new SortDescription("Occurances", ListSortDirection.Descending));
        //        _viewModel.ViewDetails.listBoxWords.DataContext = task.FilteredWords;

        //        var linksView = (ListCollectionView) task.FilteredLinks.View;
        //        if (linksView.IsAddingNew)
        //        {
        //            linksView.CommitNew();
        //        }
        //        linksView.SortDescriptions.Add(new SortDescription("Occurances", ListSortDirection.Descending));
        //        _viewModel.ViewDetails.DataContext = task.FilteredLinks;

        //        var logsview = (ListCollectionView) task.FilteredLogs.View;
        //        if (logsview.IsAddingNew)
        //        {
        //            logsview.CommitNew();
        //        }
        //        _viewModel.ViewDetails.listBoxLogs.DataContext = task.FilteredLogs;
        //        task.FilteredLogs.Filter += OnFilteredLogsFilter;

        //        task.Logs.RefreshLogTagList();
        //        _viewModel.ViewDetails.comboBoxLogTagsFilter.DataContext = task.Logs;

        //        linksView = (ListCollectionView) task.FilteredItems.View;
        //        if (linksView.IsAddingNew)
        //        {
        //            linksView.CommitNew();
        //        }
        //        _viewModel.ViewDetails.listBoxItems.DataContext = task.FilteredItems;
        //        task.FilteredItems.Filter += OnFilteredItemsFilter;
        //    }
        //}

        

        //private void OnListBoxLogsSelectionChanged(object sender, RoutedEventArgs e)
        //{
        //    var log = (Log) listBoxLogs.SelectedItem;
        //    textBoxLogTags.DataContext = log;
        //    textBoxLogEntry.DataContext = log;
        //}

        private void OnAccordianButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.HandleAccordianButtonClick();
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
            _viewModel.HandleListBoxWithAddRemoveMouseDown(sender, e);
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