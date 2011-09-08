using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskDash.Notifications;

namespace TaskDash.Commands
{
    class RemoveNotificationCommand : BaseCommand
    {
        NotificationList _notifications;
        Notification _notification;

        public RemoveNotificationCommand(Notification notification, NotificationList notifications)
        {
            _notifications = notifications;
            _notification = notification;
        }

        public override string Text
        {
            get { return "Remove Notification"; }
        }

        public override string Description
        {
            get { return "remove notification from queue"; }
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            lock (_notifications.Lock)
            {
                _notifications.Remove(_notification);
            }
        }
    }
}
