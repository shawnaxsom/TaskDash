using System;
using System.Diagnostics;
using Norm;
using Norm.Collections;
using TaskDash.Core.Models.Tasks;

namespace TaskDash.Core.Data
{
    public class DataManagerFactory
    {
        public static DataManagerFactory Instance
        {
            get { return InstanceHolder._instance; }
        }

        public IDataManager CreateDataManager()
        {
            return DataManager.Instance ?? (DataManager.Instance = new DataManager());
        }

        #region Nested type: InstanceHolder

        private static class InstanceHolder
        {
            public static readonly DataManagerFactory _instance = new DataManagerFactory();
        }

        #endregion
    }

    public interface IDataManager
    {
        IMongoCollection<Task> GetData();
        void SaveData(Task task, IMongoCollection<Task> tasks);
        void RemoveData(Task task, IMongoCollection<Task> tasks);
    }

    public class DataManager : IDataManager, IDisposable
    {
        internal DataManager()
        {
            
        }

        private IMongoCollection<Task> _taskData;

        public static IDataManager Instance
        {
            get { return InstanceHolder._instance; }
            internal set { InstanceHolder._instance = value; }
        }

        public string ConnectionString
        {
            get
            {
#if DEBUG
                return "mongodb://localhost/TaskDashDebug";
#else
                return "mongodb://localhost/TaskDash";
#endif
            }
        }

        #region IDataManager Members

        public IMongoCollection<Task> GetData()
        {
            using (var database = Mongo.Create(ConnectionString))
            {
                IMongoCollection<Task> tasks = database.GetCollection<Task>("tasks");

                _taskData = tasks;

                return tasks;
            }
        }

        public void SaveData(Task task, IMongoCollection<Task> tasks)
        {

            using (var database = Mongo.Create("mongodb://localhost/TaskDash"))
            {
                try
                {
                    _taskData.Save(task);
                }
                catch (Exception e)
                {
                    
                }
            }

        }

        public void RemoveData(Task task, IMongoCollection<Task> tasks)
        {
            using (var database = Mongo.Create("mongodb://localhost/TaskDash"))
            {
                _taskData.Delete(task);
            }
        }

        #endregion

        #region Nested type: InstanceHolder

        internal static class InstanceHolder
        {
            public static IDataManager _instance;
        }

        #endregion

        public void Dispose()
        {
            
        }
    }
}