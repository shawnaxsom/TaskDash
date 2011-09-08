using System;
using System.Windows.Input;
using TaskDash.Core.Models;
using TaskDash.Core.Models.Tasks;

namespace TaskDash.Commands
{
    public class DelayTaskCommand : BaseCommand, ITaskCommand
    {
        public DelayTaskCommand(Task task)
        {
            this.Task = task;
        }

        public override void Execute(object parameter)
        {
            this.Task.DueDate = this.Task.DueDate.AddDays(1);
            this.Cancelled = true;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public new event EventHandler CanExecuteChanged;

        public Task Task { get; set; }

        public override string Text
        {
            get { return "Delay One Day"; }
        }

        public override string Description
        {
            get { return "delay the task for one day"; }
        }
    }
}