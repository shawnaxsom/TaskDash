using System;
using System.Linq;
using Norm.Attributes;

namespace TaskDash.Core.Models.Tasks
{
    public class TaskTimeOnDays<T> : ModelCollectionBase<TaskTimeOnDay>
    {
        private TaskTimeOnDay _cachedDayTime;

        [MongoIgnore]
        public TaskTimeOnDay Today
        {
            get
            {
                if (_cachedDayTime == null
                    || _cachedDayTime.Date != DateTime.Today)
                {
                    foreach (var dateEntry in this)
                    {
                        if (dateEntry.Date == DateTime.Today)
                        {
                            _cachedDayTime = dateEntry;
                            break;
                        }
                    }

                    if (_cachedDayTime == null
                        || _cachedDayTime.Date != DateTime.Today)
                    {
                        _cachedDayTime = new TaskTimeOnDay(DateTime.Today);
                        this.Add(_cachedDayTime);
                    }
                }


                return _cachedDayTime;
            }
        }

        [MongoIgnore]
        public string TotalTime
        {
            get
            {
                var total = new TimeSpan(0, 0, 0, 0);

                total = this.Aggregate(total, (current, time) => current + time);

                return total.ToString(total > new TimeSpan(1, 0, 0, 0) ? @"dd hh\:mm\:ss" : @"hh\:mm\:ss");
            }
        }

        public TimeSpan TimeWithinDays(int days)
        {
            var total = new TimeSpan(0, 0, 0, 0);

            var daysSpan = new TimeSpan(days, 0, 0, 0);

            total = this.Where(timeOnDay => days == -1 ||
                DateTime.Today.Subtract(timeOnDay.Date) <= daysSpan)
                .Aggregate(total, (current, timeOnDay) 
                    => current + timeOnDay);

            return total;
        }
        public string TimeWithinDaysFormatted(int days)
        {
            TimeSpan total = this.TimeWithinDays(days);
            return total.ToString(total > new TimeSpan(1, 0, 0, 0) ? @"dd hh\:mm\:ss" : @"hh\:mm\:ss");
        }

        [MongoIgnore]
        public TimeSpan TimeToday
        {
            get { return this.TimeWithinDays(0); }
        }
    }
}
