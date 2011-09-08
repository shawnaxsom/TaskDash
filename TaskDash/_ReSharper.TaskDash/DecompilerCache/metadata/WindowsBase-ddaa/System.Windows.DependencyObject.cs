// Type: System.Windows.DependencyObject
// Assembly: WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\WindowsBase.dll

using MS.Internal.ComponentModel;
using System.ComponentModel;
using System.Runtime;
using System.Windows.Markup;
using System.Windows.Threading;

namespace System.Windows
{
    [TypeDescriptionProvider(typeof (DependencyObjectProvider))]
    [NameScopeProperty("NameScope", typeof (NameScope))]
    public class DependencyObject : DispatcherObject
    {
        public DependencyObject();
        public DependencyObjectType DependencyObjectType { get; }

        public bool IsSealed { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public override sealed bool Equals(object obj);
        public override sealed int GetHashCode();
        public object GetValue(DependencyProperty dp);
        public void SetValue(DependencyProperty dp, object value);
        public void SetCurrentValue(DependencyProperty dp, object value);
        public void SetValue(DependencyPropertyKey key, object value);
        public void ClearValue(DependencyProperty dp);
        public void ClearValue(DependencyPropertyKey key);
        public void CoerceValue(DependencyProperty dp);
        public void InvalidateProperty(DependencyProperty dp);
        protected virtual void OnPropertyChanged(DependencyPropertyChangedEventArgs e);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        protected internal virtual bool ShouldSerializeProperty(DependencyProperty dp);

        public object ReadLocalValue(DependencyProperty dp);
        public LocalValueEnumerator GetLocalValueEnumerator();
    }
}
