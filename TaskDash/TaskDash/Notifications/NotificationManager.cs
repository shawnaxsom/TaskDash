using System;
using System.Windows.Threading;
using TaskDash.Commands;
using TaskDash.Core.Models.Tasks;
using TaskDash.ViewModels;

namespace TaskDash.Notifications
{
    public class NotificationManager
    {
        private readonly TaskViewModel _taskViewModel;
        private readonly DispatcherTimer _timer;
        private readonly NotificationQueue _notificationQueue;
        private DateTime _lastChecked = DateTime.Now;

        public NotificationManager(TaskViewModel taskViewModel)
        {
            _taskViewModel = taskViewModel;

            _notificationQueue = new NotificationQueue();

            _timer = new DispatcherTimer(DispatcherPriority.Normal);
            _timer.Tick += CheckForNotifications;
            _timer.Interval = new TimeSpan(0, 0, 0, 1);
        }

        public void Start()
        {
            _timer.Start();
        }

        private void CheckForNotifications(object sender, EventArgs eventArgs)
        {
            if (IsNewDay())
            {
                // Clear any notifications that may have been dismissed. Allow them to alert again today.
                _notificationQueue.Clear();
            }

            foreach (Task task in _taskViewModel.Tasks)
            {
                if (task.DueDate.Date == DateTime.Today)
                {
                    Notification notification = new Notification(
                        "A task is due today\n" + task.Key + " " + task.Description,
                        new OpenTaskCommand(task) as ITaskDashCommand,
                        new DelayTaskCommand(task) as ITaskDashCommand
                            );
                    
                    ShowPopup(notification);
                }
            }
        }

        private bool IsNewDay()
        {
            return (_lastChecked.Date != DateTime.Today.Date);
        }

        private void ShowPopup(Notification notification)
        {
            _notificationQueue.Enqueue(notification);
        }
    }
}
