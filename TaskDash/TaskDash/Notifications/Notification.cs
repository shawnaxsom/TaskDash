using System;
using System.ComponentModel;
using TaskDash.Commands;

namespace TaskDash.Notifications
{
    public sealed class Notification : INotifyPropertyChanged
    {
        /// <summary>
        /// Bind to this
        /// </summary>
        private string _delayLength;

        private bool _dismissButtonPressed;

        public Notification(string description, params ITaskDashCommand[] commands)
        {
            Description = description;
            Commands = commands;
        }

        public string Description { get; private set; }

        private DateTime DelayTime { get; set; }

        public string DelayLength
        {
            get { return _delayLength; }
            set
            {
                if (_delayLength != value)
                {
                    if (String.IsNullOrEmpty(value))
                    {
                        DelayTime = DateTime.MinValue;
                    }
                    else
                    {
                        DelayTime = DateTime.Now;
                    }

                    _delayLength = value;
                }

                OnPropertyChanged("DelayLength");
                OnPropertyChanged("DelayTime");
                OnPropertyChanged("DelayTimeSpan");
                OnPropertyChanged("NextNotificationTime");
            }
        }

        private DateTime NextNotificationTime
        {
            get { return DelayTime.Add(DelayTimeSpan); }
        }

        private TimeSpan DelayTimeSpan
        {
            get
            {
                TimeSpan timeSpan;
                TimeSpan.TryParse(DelayLength, out timeSpan);

                return timeSpan;
            }
        }


        public ITaskDashCommand[] Commands { get; private set; }

        internal bool IsReady
        {
            get { return (DateTime.Now > NextNotificationTime); }
        }

        internal bool IsDelayed
        {
            get { return !IsReady; }
        }


        public bool IsDismissed
        {
            get
            {
                if (_dismissButtonPressed)
                {
                    return true;
                }

                foreach (ITaskDashCommand command in Commands)
                {
                    if (command.Cancelled)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Notification)) return false;
            return Equals((Notification)obj);
        }

        internal void Dismiss()
        {
            _dismissButtonPressed = true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Equals(Notification other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            bool equal = CheckEquals(other);

            return equal;
        }

        private bool CheckEquals(Notification other)
        {
            bool equalLength = Equals(other._delayLength, _delayLength);
            bool equalButtonPresses = other._dismissButtonPressed.Equals(_dismissButtonPressed);
            bool equalDescriptions = Equals(other.Description, Description);
            bool equalDelay = other.DelayTime.Equals(DelayTime);

            return equalLength
                   && equalButtonPresses
                   && equalDescriptions
                   && equalDelay;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (_delayLength != null ? _delayLength.GetHashCode() : 0);
                result = (result * 397) ^ _dismissButtonPressed.GetHashCode();
                result = (result * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                result = (result * 397) ^ DelayTime.GetHashCode();
                result = (result * 397) ^ (Commands != null ? Commands.GetHashCode() : 0);
                return result;
            }
        }
    }
}