using System.Collections;
using System.Collections.Generic;

namespace TaskDash.Notifications
{
    public class NotificationList : IEnumerable
    {
        private readonly List<Notification> _container = new List<Notification>();

        public NotificationList()
        {
            Lock = new object();
        }

        public int CountBeforeReset { get; private set; }

        public object Lock { get; private set; }

        public Notification this[int index]
        {
            get { return _container[index]; }
        }

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return _container.GetEnumerator();
        }

        #endregion

        public void Add(Notification notification)
        {
            _container.Add(notification);

            CountBeforeReset++;
        }

        public void Remove(Notification notification)
        {
            _container.Remove(notification);

            if (_container.Count == 0)
            {
                CountBeforeReset = 0;
            }
        }
    }
}