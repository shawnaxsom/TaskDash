using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Norm;
using Norm.Attributes;
using TaskDash.Core.Data;
using TaskDash.Core.Models.Tasks;

namespace TaskDash.Core
{
    public interface IModelBase
    {
        Visibility IsEditing { get; }
        Visibility IsDisplaying { get; }
        string DisplayValue { get; }
        void ToggleEditing();
    }

    public interface IRankable
    {
        double RankingImportance { get; }
        bool CloselyMatches(string matchPhrase);
        double MatchRanking(string matchPhrase);
    }

    public abstract class ModelBase<T> : ModelBase
    {
        public ModelBase<T> CreateItem()
        {
            throw new NotImplementedException();
        }
    }

    // NOTE: Expando: Allows your model to expand to inherit any database fields not in model.
    // TODO: Expando ends up DELETING all of the data values on save. WHY??? Maybe implement IExpando instead???
    public abstract class ModelBase : IModelBase, IRankable, INotifyPropertyChanged
    {
        private Visibility _isEditing;

        public ModelBase()
        {
            DataManager = DataManagerFactory.Instance.CreateDataManager();

            IsEditing = Visibility.Collapsed;
        }

        [MongoIgnore]
        protected IDataManager DataManager { get; set; }

        public void CopyToClipboard()
        {
            Clipboard.SetText(EditableValue);
        }

        public ObjectId _Id { get; set; }

        [MongoIgnore]
        public virtual string DisplayValue
        {
            get { return EditableValue; }
        }

        [MongoIgnore]
        public abstract string EditableValue { get; set; }

        [MongoIgnore]
        public ToolTip ToolTip
        {
            get
            {
                return new ToolTip()
                           {
                               Content = EditableValue
                           };
            }
        }

        #region IModelBase Members

        [MongoIgnore]
        public Visibility IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value;
                OnPropertyChanged("IsEditing");
                OnPropertyChanged("IsDisplaying");
            }
        }

        [MongoIgnore]
        public Visibility IsDisplaying
        {
            get
            {
                if (IsEditing == Visibility.Visible)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            set
            {
                if (value == Visibility.Visible)
                {
                    IsEditing = Visibility.Collapsed;
                }
                else
                {
                    IsEditing = Visibility.Visible;
                }
            }
        }

        public void ToggleEditing()
        {
            if (IsEditing == Visibility.Visible)
            {
                IsEditing = Visibility.Collapsed;
            }
            else
            {
                IsEditing = Visibility.Visible;
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IRankable Members

        public virtual bool CloselyMatches(string matchPhrase)
        {
            return false;
        }

        public double MatchRanking(string matchPhrase)
        {
            return GetRanking(matchPhrase)*RankingImportance;
        }

        [MongoIgnore]
        public virtual double RankingImportance
        {
            get { return 100; }
        }

        #endregion

        protected virtual double GetRanking(string matchPhrase)
        {
            return 0;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        [MongoIgnore]
        public Visibility IsCompletable
        {
            get
            {
                bool isCompletable = this is ICompletable;
                if (isCompletable)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
        }
    }
}