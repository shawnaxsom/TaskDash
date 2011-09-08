// Type: System.Windows.Controls.Primitives.Selector
// Assembly: PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\Profile\Client\PresentationFramework.dll

using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime;
using System.Windows;
using System.Windows.Controls;

namespace System.Windows.Controls.Primitives
{
    [DefaultEvent("SelectionChanged")]
    [DefaultProperty("SelectedIndex")]
    [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
    public abstract class Selector : ItemsControl
    {
        public static readonly RoutedEvent SelectionChangedEvent;
        public static readonly RoutedEvent SelectedEvent;
        public static readonly RoutedEvent UnselectedEvent;
        public static readonly DependencyProperty IsSelectionActiveProperty;
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty IsSynchronizedWithCurrentItemProperty;
        public static readonly DependencyProperty SelectedIndexProperty;
        public static readonly DependencyProperty SelectedItemProperty;
        public static readonly DependencyProperty SelectedValueProperty;
        public static readonly DependencyProperty SelectedValuePathProperty;
        protected Selector();

        [TypeConverter(
            "System.Windows.NullableBoolConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null"
            )]
        [Localizability(LocalizationCategory.NeverLocalize)]
        [Bindable(true)]
        [Category("Behavior")]
        public bool? IsSynchronizedWithCurrentItem { get; set; }

        [Localizability(LocalizationCategory.NeverLocalize)]
        [Bindable(true)]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        [Category("Appearance")]
        public object SelectedItem { get; set; }

        [Localizability(LocalizationCategory.NeverLocalize)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        [Category("Appearance")]
        public object SelectedValue { get; set; }

        [Bindable(true)]
        [Category("Appearance")]
        [Localizability(LocalizationCategory.NeverLocalize)]
        public string SelectedValuePath { get; set; }

        public static void AddSelectedHandler(DependencyObject element, RoutedEventHandler handler);
        public static void RemoveSelectedHandler(DependencyObject element, RoutedEventHandler handler);
        public static void AddUnselectedHandler(DependencyObject element, RoutedEventHandler handler);
        public static void RemoveUnselectedHandler(DependencyObject element, RoutedEventHandler handler);
        public static bool GetIsSelectionActive(DependencyObject element);

        [AttachedPropertyBrowsableForChildren]
        public static bool GetIsSelected(DependencyObject element);

        public static void SetIsSelected(DependencyObject element, bool isSelected);
        protected override void ClearContainerForItemOverride(DependencyObject element, object item);
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e);
        protected virtual void OnSelectionChanged(SelectionChangedEventArgs e);
        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue);

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item);
        protected override void OnInitialized(EventArgs e);

        [Category("Behavior")]
        public event SelectionChangedEventHandler SelectionChanged;
    }
}
