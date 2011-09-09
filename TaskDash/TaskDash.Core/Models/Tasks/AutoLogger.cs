using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Norm.Attributes;
using TaskDash.Core.Utilities;

namespace TaskDash.Core.Models.Tasks
{
    public class AutoLogger : ModelBase<AutoLogger>
    {
        private readonly TaskStopwatch _stopWatch;
        private TimeSpan _timeBetweenPrompts;

        public AutoLogger()
        {
            _stopWatch = new TaskStopwatch();
            _stopWatch.Tick += OnStopWatchTick;
        }

        private void OnStopWatchTick(object sender, EventArgs e)
        {
            LoggingRequested(this, new LoggingRequestedEventHandlerArgs());
        }

        public event LoggingRequestedEventHandler LoggingRequested;

        [MongoIgnore]
        public string TimeBetweenPrompts
        {
            get { return _timeBetweenPrompts.ToString(@"hh\:mm\:ss"); }
            set
            {
                TimeSpan newTime;

                if (TimeSpan.TryParse(value, out newTime))
                {
                    _timeBetweenPrompts = newTime;
                    OnPropertyChanged("TimeBetweenPrompts");
                }
            }
        }

        [MongoIgnore]
        public override string EditableValue
        {
            get { return TimeBetweenPrompts; }
            set { TimeBetweenPrompts = value; }
        }

        public void Start()
        {
            _stopWatch.Start();
        }
    }

    public delegate void LoggingRequestedEventHandler(object sender, LoggingRequestedEventHandlerArgs args);

    public class LoggingRequestedEventHandlerArgs : EventArgs
    {
        public LoggingRequestedEventHandlerArgs()
        {
        }
    }
}
