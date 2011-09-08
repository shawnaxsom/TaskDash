using System;
using System.Linq;
using System.Collections;
using System.Windows;
using System.Windows.Threading;
using TaskDash.Commands;

namespace TaskDash.Notifications
{
    public class NotificationQueue : Queue
    {
        private readonly object lockObject = new object();
        private DispatcherTimer _timer;
        private bool _debug; // Set this to true to quickly fire off the same notifications over and over.

        public NotificationQueue()
        {
            _timer = new DispatcherTimer(DispatcherPriority.Normal);
            _timer.Tick += CheckForNotifications;
            _timer.Interval = new TimeSpan(0, 0, 2);
            if (_debug)
            {
                _timer.Interval = new TimeSpan(0, 0, 0, 2);
            }


            DisplayedNotifications = new NotificationList();
        }

        private void CheckForNotifications(object sender, EventArgs e)
        {
            Notification notification = (Notification)this.DequeueNextReady();

            if (notification != null)
            {
                ShowPopup(notification);
            }
        }


        public NotificationList DisplayedNotifications { get; set; }

        private Notification DequeueNextReady()
        {
            if (this.Count == 0)
            {
                return null;
            }

            lock (lockObject)
            {
                int i = 0;
                Notification notification = (Notification)this.Dequeue();
                this.Enqueue(notification);

                while (i < this.Count
                    && notification != null)
                {
                    // Put notification back onto the end of the queue
                    if (notification.IsReady)
                    {
                        this.Enqueue(notification);
                        return notification;
                    }
                    else
                    {
                        i++;

                        notification = (Notification)this.Dequeue();
                    }
                }

                return null;
            }
        }

        public override bool Contains(object obj)
        {
            if (_debug == true) return false;

            Notification notification = (Notification)obj;

            foreach (Notification existingNotification in this)
            {
                if (notification.Equals(existingNotification))
                {
                    return true;
                }
            }

            return false;
        }
        public bool ContainsOrDisplays(object obj)
        {
            if (_debug) return false;

            if (Contains(obj) || Displays(obj))
            {
                return true;
            }

            return false;
        }

        private bool Displays(object obj)
        {
            if (_debug) return false;

            Notification notification = (Notification)obj;

            lock (DisplayedNotifications.Lock)
            {
                if (this.DisplayedNotifications.Cast<Notification>()
                    .Any(notification.Equals))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Enqueues only if not contained or displayed. For public use.
        /// </summary>
        /// <param name="obj"></param>
        public override void Enqueue(object obj)
        {
            lock (lockObject)
            {
                if (this.ContainsOrDisplays((Notification)obj))
                {
                    return;
                }

                base.Enqueue(obj);


                if (!_timer.IsEnabled)
                {
                    _timer.Start();
                }
            }
        }

        private void ShowPopup(Notification notification)
        {
            if (notification == null
                || notification.IsDismissed)
            {
                return;
            }

            if (this.Displays(notification))
            {
                return;
            }

            DisplayedNotifications.Add(notification);

            lock (lockObject)
            {
                if (notification.IsDismissed)
                {
                    return;
                }

                

                _timer.Dispatcher.Invoke(
                    DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            NotificationWindow notifier = new NotificationWindow(notification)
                                    {
                                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                        OtherWindowCount = DisplayedNotifications.CountBeforeReset - 1,
                                        CloseCommand = new RemoveNotificationCommand(notification, DisplayedNotifications)
                                    };
                            notifier.BeginInvoke();
                            notifier.Show();
                        }));
            }

        }
    }
}
