// Type: System.Windows.Controls.ItemContainerGenerator
// Assembly: PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\Profile\Client\PresentationFramework.dll

using System;
using System.Runtime;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace System.Windows.Controls
{
    public sealed class ItemContainerGenerator : IRecyclingItemContainerGenerator, IItemContainerGenerator,
                                                 IWeakEventListener
    {
        public GeneratorStatus Status { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        #region IRecyclingItemContainerGenerator Members

        ItemContainerGenerator IItemContainerGenerator.GetItemContainerGeneratorForPanel(Panel panel);
        IDisposable IItemContainerGenerator.StartAt(GeneratorPosition position, GeneratorDirection direction);

        IDisposable IItemContainerGenerator.StartAt(GeneratorPosition position, GeneratorDirection direction,
                                                    bool allowStartAtRealizedItem);

        DependencyObject IItemContainerGenerator.GenerateNext();
        DependencyObject IItemContainerGenerator.GenerateNext(out bool isNewlyRealized);
        void IItemContainerGenerator.PrepareItemContainer(DependencyObject container);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        void IItemContainerGenerator.Remove(GeneratorPosition position, int count);

        void IItemContainerGenerator.RemoveAll();

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        void IRecyclingItemContainerGenerator.Recycle(GeneratorPosition position, int count);

        GeneratorPosition IItemContainerGenerator.GeneratorPositionFromIndex(int itemIndex);
        int IItemContainerGenerator.IndexFromGeneratorPosition(GeneratorPosition position);

        #endregion

        #region IWeakEventListener Members

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e);

        #endregion

        public object ItemFromContainer(DependencyObject container);
        public DependencyObject ContainerFromItem(object item);
        public int IndexFromContainer(DependencyObject container);
        public DependencyObject ContainerFromIndex(int index);

        public event ItemsChangedEventHandler ItemsChanged;
        public event EventHandler StatusChanged;
    }
}
