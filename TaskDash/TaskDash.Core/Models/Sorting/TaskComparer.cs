using System;
using System.Collections;
using System.Collections.Generic;
using TaskDash.Core.Models.Tasks;

namespace TaskDash.Core.Models.Sorting
{
    public class TaskComparer : IComparer
    {
        #region TaskComparingMethod enum

        public enum TaskComparingMethod
        {
            None,
            LastObserved,
            Relevance,
            TotalTime
        }

        #endregion

        private const TaskComparingMethod DEFAULT_METHOD = TaskComparingMethod.None;


        private static List<TaskComparer> instance;
        private static TaskComparer _default;

        private readonly TaskComparingMethod _mainMethod;

        private TaskComparer(TaskComparingMethod method)
        {
            _mainMethod = method;
        }

        public string MatchPhrase { get; set; }

        public static List<TaskComparer> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new List<TaskComparer>();

                    foreach (TaskComparingMethod method in Enum.GetValues(typeof (TaskComparingMethod)))
                    {
                        instance.Add(new TaskComparer(method));
                    }
                }

                return instance;
            }
        }

        public static TaskComparer Default
        {
            get { return _default ?? (_default = new TaskComparer(DEFAULT_METHOD)); }
        }

        public TaskComparingMethod MainMethod
        {
            get { return _mainMethod; }
        }

        #region IComparer Members

        public int Compare(object oX, object oY)
        {
            var x = (Task) oX;
            var y = (Task) oY;

            return Compare(x, y, MainMethod);
        }

        #endregion

        private int Compare(object oX, object oY, TaskComparingMethod method)
        {
            var x = (Task) oX;
            var y = (Task) oY;

            switch (method)
            {
                case TaskComparingMethod.LastObserved:
                    {
                        DateTime xtime = x.LastObserved;
                        DateTime ytime = y.LastObserved;

                        if (xtime > ytime)
                        {
                            return -1;
                        }
                        
                        if (xtime < ytime)
                        {
                            return 1;
                        }

                        return 0;
                    }
                case TaskComparingMethod.Relevance:
                    {
                        var xrank = (int) x.MatchRanking(MatchPhrase);
                        var yrank = (int) y.MatchRanking(MatchPhrase);

                        if (xrank > yrank)
                        {
                            return -1;
                        }
                        else if (xrank < yrank)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                case TaskComparingMethod.TotalTime:
                    {
                        TimeSpan xtime = TimeSpan.Parse(x.TotalTime);
                        TimeSpan ytime = TimeSpan.Parse(y.TotalTime);

                        if (xtime > ytime)
                        {
                            return -1;
                        }
                        else if (xtime < ytime)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
            }

            throw new NotImplementedException("Comparison method not implemented!");
        }

        public override string ToString()
        {
            return MainMethod.ToString();
        }
    }
}