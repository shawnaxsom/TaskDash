using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using TaskDash.Core.Models.Sorting;
using TaskDash.Core.Models.Tasks;

namespace TaskDash.Windows.Main
{
    class TaskListViewModel
    {
        internal void Search()
        {
            if (_tasks == null) return;

            var view = (ListCollectionView)_tasks.FilteredTasks.View;

            if (view.IsAddingNew)
            {
                view.CommitNew();
            }

            TaskComparer sorter = (TaskComparer)_view.comboBoxSortBy.SelectedValue ?? TaskComparer.Default;

            if (sorter.MainMethod != TaskComparer.TaskComparingMethod.None)
            {
                sorter.MatchPhrase = _view.textBoxSearch.Text;
                view.CustomSort = sorter;
            }

            // Search
            view.Refresh();
        }

        
    }
}
