//using System.ComponentModel;
//using System.Windows;
//using System.Windows.Data;
//using TaskDash.Core.Models.Tasks;

//namespace TaskDash.ViewModels
//{
//    /// <summary>
//    /// http://en.wikipedia.org/wiki/Model_View_ViewModel
//    /// </summary>
//    public class MainWindowViewModel : ViewModelBase<MainWindowViewModel>
//    {
//        public MainWindowViewModel()
//        {
//            Tasks = new Task().GetTasks();
//            FilteredTasks = new CollectionViewSource { Source = this.Tasks };
//        }

//        private Task _currentTask;
//        public Task CurrentTask
//        {
//            get { return _currentTask; }
//            set
//            {
//                _currentTask = value;
//                OnPropertyChanged("CurrentTask");
//                OnPropertyChanged("CurrentTask.Key");
//                OnPropertyChanged("Key");
//            }
//        }

//        public CollectionViewSource FilteredTasks { get; private set; }
//        private Tasks _tasks;
//        public Tasks Tasks
//        {
//            get { return _tasks; }
//            private set
//            {
//                _tasks = value;
//                i = 0;
//            }
//        }

//        private void AddNewTask()
//        {
//            Tasks.Add(new Task() { _Id = i.ToString() });
//            i++;
//        }

//        public void Save()
//        {
//            foreach (Task task in Tasks)
//            {
//                task.Save();
//            }
//        }
//    }
//}
