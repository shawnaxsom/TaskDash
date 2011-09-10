using System;
using System.Windows.Threading;
using TaskDash.Core.Models.Tasks;
using TaskDash.Core.Utilities;
using TaskDash.ViewModels;

namespace TaskDash.Core.Services
{
    public class SaveService
    {
        private readonly DispatcherTimer _timer;
        private readonly Tasks _tasks;

        public SaveService(Tasks tasks)
        {
            _tasks = tasks;

            _timer = new DispatcherTimer(DispatcherPriority.Normal);
            _timer.Tick += CheckToSave;
            
            // Don't set this too fast or you will lock up the connection pool with the maximum 25 connections
            _timer.Interval = TimeSpan.FromSeconds(1);
        }

        public void Start()
        {
            _timer.Start();
        }

        private void CheckToSave(object sender, EventArgs eventArgs)
        {
            if (IdleMonitor.IsWaitingForInput)
            {
                Save();
            }
        }

        public void Save()
        {
            _tasks.Save();
        }
    }
}