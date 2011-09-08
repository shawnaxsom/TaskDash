// Type: System.Windows.Data.CollectionViewSource
// Assembly: PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\PresentationFramework.dll

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime;
using System.Windows;

namespace System.Windows.Data
{
    public class CollectionViewSource : DependencyObject, ISupportInitialize, IWeakEventListener
    {
        public static readonly DependencyProperty ViewProperty;
        public static readonly DependencyProperty SourceProperty;
        public static readonly DependencyProperty CollectionViewTypeProperty;
        public CollectionViewSource();

        [ReadOnly(true)]
        public ICollectionView View { get; }

        public object Source { get; set; }
        public Type CollectionViewType { get; set; }

        [TypeConverter(typeof (CultureInfoIetfLanguageTagConverter))]
        public CultureInfo Culture { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        public SortDescriptionCollection SortDescriptions { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public ObservableCollection<GroupDescription> GroupDescriptions { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        #region ISupportInitialize Members

        void ISupportInitialize.BeginInit();
        void ISupportInitialize.EndInit();

        #endregion

        #region IWeakEventListener Members

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e);

        #endregion

        protected virtual void OnSourceChanged(object oldSource, object newSource);
        protected virtual void OnCollectionViewTypeChanged(Type oldCollectionViewType, Type newCollectionViewType);
        public static ICollectionView GetDefaultView(object source);
        public static bool IsDefaultView(ICollectionView view);
        public IDisposable DeferRefresh();
        protected virtual bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e);

        public event FilterEventHandler Filter;
    }
}
