using System;
using Norm.Attributes;

namespace TaskDash.Core.Models.Tasks
{
    public class TaskTimeOnDay : ModelBase<TaskTimeOnDay>
    {
        private DateTime _date = DateTime.Today;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private TimeSpan _time;

        public TaskTimeOnDay()
        {
        }
        public TaskTimeOnDay(DateTime date)
        {
            _date = date;
        }

        public string Time
        {
            get
            {
                return _time.ToString(_time > new TimeSpan(1, 0, 0, 0) ? @"dd hh\:mm\:ss" : @"hh\:mm\:ss");
            }
            set { _time = TimeSpan.Parse(value); }
        }

        [MongoIgnore]
        public override string EditableValue
        {
            get { return this.Date.ToString(); }
            set { this.Date = DateTime.Parse(value); }
        }

        public static TimeSpan operator +(TaskTimeOnDay time1, TaskTimeOnDay time2)
        {
            return time1._time.Add(time2._time);
        }

        public static TimeSpan operator +(TimeSpan time1, TaskTimeOnDay time2)
        {
            return time1.Add(time2._time);
        }
    }
}
