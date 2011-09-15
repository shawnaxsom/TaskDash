using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskDash.Core.Models;
using TaskDash.Core.Models.Tasks;

namespace TaskDash
{
    /// <summary>
    /// Interaction logic for TaskItem.xaml
    /// </summary>
    public partial class EditTaskItem : Window
    {
        private readonly ICollectionView _view;
        private TaskItem _taskItem;

        public EditTaskItem(ICollectionView view, TaskItem taskItem)
        {
            InitializeComponent();

            this.DataContext = taskItem;
            _view = view;
            _taskItem = taskItem;

            this.Loaded += OnEditTaskItemLoaded;
            this.Closed += OnClosed;
        }

        private void OnClosed(object sender, EventArgs e)
        {
            _view.Refresh();
        }

        void OnEditTaskItemLoaded(object sender, RoutedEventArgs e)
        {
            textBoxNotes.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (e.Key == Key.W)
                {
                    this.Close();
                }
            }
            else
            {
                if (e.Key == Key.Escape)
                {
                    this.Close();
                }
            }
        }
    }
}
