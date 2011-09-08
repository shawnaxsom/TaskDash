using TaskDash.Core.Models.Tasks;

namespace TaskDash.Commands
{
    interface ITaskCommand : ITaskDashCommand
    {
        Task Task { get; set; }
    }
}
