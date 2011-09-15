using TaskDash.Core.Models.Tasks;

namespace TaskDash.Commands
{
    public interface ITaskCommand : ITaskDashCommand
    {
        Task Task { get; set; }
    }
}
