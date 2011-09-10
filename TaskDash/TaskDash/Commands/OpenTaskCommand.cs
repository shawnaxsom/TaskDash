using TaskDash.Core.Models.Tasks;

namespace TaskDash.Commands
{
    public class OpenTaskCommand : BaseCommand, ITaskCommand
    {
        public OpenTaskCommand(Task task)
        {
            Task = task;
        }

        #region ITaskCommand Members

        public override void Execute(object parameter)
        {
            var main = MainWindow.Instance;
            main.SelectTask(Task);
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public Task Task { get; set; }

        public override string Text
        {
            get { return "Open Task"; }
        }

        public override string Description
        {
            get { return "open the task in the main view"; }
        }

        #endregion
    }
}