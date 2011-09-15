using System;

namespace TaskDash.Commands
{
    public abstract class BaseCommand : ITaskDashCommand
    {
        public bool Cancelled { get; set; }

        public abstract bool CanExecute(object parameter);
        
        public event EventHandler CanExecuteChanged;

        public abstract void Execute(object parameter);

        public abstract string Text { get; }

        public abstract string Description { get; }
    }
}
