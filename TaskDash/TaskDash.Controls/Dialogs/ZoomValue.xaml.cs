using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using TaskDash.Core;
using TaskDash.Core.Models.Tasks;

namespace TaskDash.CustomControls
{
    /// <summary>
    /// Interaction logic for TaskItem.xaml
    /// </summary>
    public partial class ZoomValue : Window
    {
        private readonly ListCollectionView _view;
        private readonly ModelBase _item;

        public ZoomValue(ListCollectionView view, ModelBase item)
        {
            _view = view;
            _item = item;

            InitializeComponent();

            this.DataContext = item;

            this.Loaded += OnEditTaskItemLoaded;
            this.Closed += new EventHandler(ZoomValue_Closed);
        }

        void ZoomValue_Closed(object sender, EventArgs e)
        {
            _view.Refresh();
        }

        void OnEditTaskItemLoaded(object sender, RoutedEventArgs e)
        {
            textBoxEditableValue.Focus();
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
