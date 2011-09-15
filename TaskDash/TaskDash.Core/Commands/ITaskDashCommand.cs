using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace TaskDash.Commands
{
    public interface ITaskDashCommand : ICommand
    {
        // Allow any command to be on a button
        string Text { get; }

        // Allow any command to provide a description of what it does
        // E.g. "open a task in the main view".
        // Will show up like: "Or you can open a task in the main view".
        string Description { get; }

        /// <summary>
        /// Determines if notification should be dismissed after action is taken.
        /// </summary>
        bool Cancelled { get; set; }
    }
}
