using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace TaskDash.Core.Utilities
{
    public class TaskStopwatch
    {
        #region ResetType enum

        public enum ResetType
        {
            ToZero,
            ToStartTime,
            ToLastStoppedTime
        }

        #endregion

        public bool _idlePaused;
        public DispatcherTimer _refreshtimer;
        public Stopwatch _stopwatch = new Stopwatch();
        private TimeSpan _elapsedWhenLastStopped;

        public TaskStopwatch()
        {
            _refreshtimer = new DispatcherTimer(DispatcherPriority.Normal);
            _refreshtimer.Tick += DoTick;
            _refreshtimer.Interval = new TimeSpan(0, 0, 0, 1);
        }

        public TaskStopwatch(TimeSpan startTime)
            : this()
        {
            ElapsedAtStart = startTime;
        }


        private TimeSpan ElapsedAtStart { get; set; }

        public bool IsRunning
        {
            get { return _stopwatch.IsRunning; }
        }

        public TimeSpan Elapsed
        {
            get { return ElapsedAtStart.Add(ElapsedWhenLastStopped).Add(_stopwatch.Elapsed); }
            set
            {
                ElapsedAtStart = value;
                Reset(ResetType.ToStartTime);
            }
        }

        public TimeSpan ElapsedWhenLastStopped
        {
            get { return _elapsedWhenLastStopped; }
            set { _elapsedWhenLastStopped = value; }
        }

        public TimeSpan ElapsedSinceLastStopped
        {
            get { return _stopwatch.Elapsed; }
        }

        public TimeSpan Interval
        {
            get { return _refreshtimer.Interval; }
            set { _refreshtimer.Interval = value; }
        }

        public TimeSpan Start()
        {
            _stopwatch.Start();
            _refreshtimer.Start();

            return Elapsed;
        }

        public TimeSpan Stop()
        {
            _stopwatch.Stop();
            _refreshtimer.Stop();

            ElapsedWhenLastStopped += _stopwatch.Elapsed;
            Reset(ResetType.ToLastStoppedTime);

            return Elapsed;
        }

        public void Reset(ResetType type)
        {
            if (_stopwatch.IsRunning)
            {
                _stopwatch.Restart();
            }
            else
            {
                _stopwatch.Reset();
            }

            if (type == ResetType.ToZero)
            {
                ElapsedAtStart = TimeSpan.Zero;
                ElapsedWhenLastStopped = TimeSpan.Zero;
            }

            if (type == ResetType.ToStartTime)
            {
                ElapsedWhenLastStopped = TimeSpan.Zero;
            }
        }

        public void DoTick(object sender, EventArgs e)
        {
            CheckIdle();

            _refreshtimer.Dispatcher.Invoke(
                DispatcherPriority.Normal,
                new Action(
                    delegate { Tick(this, new EventArgs()); }));
        }

        private void CheckIdle()
        {
            if (IdleMonitor.IsIdle
                && _stopwatch.IsRunning)
            {
                _idlePaused = true;
                _stopwatch.Stop();
            }
            else if (_idlePaused
                     && !IdleMonitor.IsIdle)
            {
                _idlePaused = false;
                _stopwatch.Start();
            }
        }

        public event EventHandler Tick;

        public void Toggle()
        {
            if (_stopwatch.IsRunning)
            {
                Stop();
            }
            else
            {
                Start();
            }
        }
    }
}