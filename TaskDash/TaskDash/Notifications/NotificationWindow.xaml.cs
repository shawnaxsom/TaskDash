using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TaskDash.Controls.ExtensionMethods;

namespace TaskDash.Notifications
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow
    {
        private readonly Notification _notification;

        public int OtherWindowCount { private get; set; }

        public NotificationWindow(Notification notification)
        {
            InitializeComponent();

            textBlockTitle.Text = notification.Description;
            textBlockDescription.Load(notification.Commands);

            _notification = notification;
            DataContext = _notification;

            foreach (var command in notification.Commands)
            {
                Button button = new Button();
                button.Click += CloseNotification;
                button.HorizontalAlignment = HorizontalAlignment.Center;
                button.Load(command);
                stackPanelButtons.Children.Add(button);
            }
        }

        public void BeginInvoke()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                var source = PresentationSource.FromVisual(this);

                if (source == null || source.CompositionTarget == null) return;


                var transform = source.CompositionTarget.TransformFromDevice;
                var corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));
                var topRightCorner = transform.Transform(new Point(workingArea.Right, workingArea.Top));

                Left = corner.X - ActualWidth - 100;

                double calculatedTop = (OtherWindowCount * -ActualHeight) + corner.Y - ActualHeight;

                if (calculatedTop < topRightCorner.Y)
                {
                    double amountOver = corner.Y - ((OtherWindowCount + 1) * ActualHeight);
                    double windowCountOverTop = -amountOver / (ActualHeight);

                    Top = (windowCountOverTop * -ActualHeight) + corner.Y - ActualHeight;
                }
                else
                {
                    Top = (OtherWindowCount * -ActualHeight) + corner.Y - ActualHeight;
                }
            }));
        }


        public ICommand CloseCommand { private get; set; }

        private void DismissNotification(object sender, RoutedEventArgs e)
        {
            if (!_notification.IsDelayed)
            {
                _notification.Dismiss();
            }
            Close();
        }

        private void CloseNotification(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MakeOpaque(object sender, MouseEventArgs e)
        {
            storyBoardFadeWindow.Stop();
            storyBoardFadeWindow.SetValue(OpacityProperty, 1.0);
            storyBoardFadeWindow.Resume();
        }

        private void OnStoryBoardFadeWindowCompleted(object sender, EventArgs e)
        {
            Close();
        }

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CloseCommand != null)
            {
                CloseCommand.Execute(null);
            }
        }
    }
}
