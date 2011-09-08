// Type: System.Windows.Window
// Assembly: PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\PresentationFramework.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Security;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shell;

namespace System.Windows
{
    [Localizability(LocalizationCategory.Ignore)]
    public class Window : ContentControl, IWindowService
    {
        public static readonly DependencyProperty TaskbarItemInfoProperty;
        public static readonly DependencyProperty AllowsTransparencyProperty;
        public static readonly DependencyProperty TitleProperty;
        public static readonly DependencyProperty IconProperty;
        public static readonly DependencyProperty SizeToContentProperty;
        public static readonly DependencyProperty TopProperty;
        public static readonly DependencyProperty LeftProperty;
        public static readonly DependencyProperty ShowInTaskbarProperty;
        public static readonly DependencyProperty IsActiveProperty;
        public static readonly DependencyProperty WindowStyleProperty;
        public static readonly DependencyProperty WindowStateProperty;
        public static readonly DependencyProperty ResizeModeProperty;
        public static readonly DependencyProperty TopmostProperty;
        public static readonly DependencyProperty ShowActivatedProperty;

        [SecurityCritical]
        public Window();

        protected internal override IEnumerator LogicalChildren { get; }
        public TaskbarItemInfo TaskbarItemInfo { get; set; }
        public bool AllowsTransparency { get; set; }

        public ImageSource Icon { get; [SecurityCritical]
        set; }

        public SizeToContent SizeToContent { get; set; }

        [TypeConverter(
            "System.Windows.LengthConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null"
            )]
        public double Top { get; set; }

        [TypeConverter(
            "System.Windows.LengthConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null"
            )]
        public double Left { get; set; }

        public Rect RestoreBounds { [SecurityCritical]
        get; }

        [DefaultValue(0)]
        public WindowStartupLocation WindowStartupLocation { get; set; }

        public bool ShowInTaskbar { get; set; }
        public bool IsActive { get; }

        [DefaultValue(null)]
        public Window Owner { [SecurityCritical]
        get; [SecurityCritical]
        set; }

        public WindowCollection OwnedWindows { get; }

        [TypeConverter(typeof (DialogResultConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool? DialogResult { get; set; }

        public WindowStyle WindowStyle { get; set; }
        public WindowState WindowState { get; set; }
        public ResizeMode ResizeMode { get; set; }
        public bool Topmost { get; set; }
        public bool ShowActivated { get; set; }

        #region IWindowService Members

        [Localizability(LocalizationCategory.Title)]
        public string Title { get; set; }

        #endregion

        public void Show();
        public void Hide();

        [SecurityCritical]
        public void Close();

        [SecurityCritical]
        public void DragMove();

        [SecurityCritical]
        public bool? ShowDialog();

        [SecurityCritical]
        public bool Activate();

        public static Window GetWindow(DependencyObject dependencyObject);
        protected override AutomationPeer OnCreateAutomationPeer();
        protected internal override sealed void OnVisualParentChanged(DependencyObject oldParent);
        protected override Size MeasureOverride(Size availableSize);
        protected override Size ArrangeOverride(Size arrangeBounds);
        protected override void OnContentChanged(object oldContent, object newContent);
        protected virtual void OnSourceInitialized(EventArgs e);
        protected virtual void OnActivated(EventArgs e);
        protected virtual void OnDeactivated(EventArgs e);
        protected virtual void OnStateChanged(EventArgs e);
        protected virtual void OnLocationChanged(EventArgs e);
        protected virtual void OnClosing(CancelEventArgs e);
        protected virtual void OnClosed(EventArgs e);
        protected virtual void OnContentRendered(EventArgs e);
        protected override void OnManipulationBoundaryFeedback(ManipulationBoundaryFeedbackEventArgs e);
        public event EventHandler SourceInitialized;
        public event EventHandler Activated;
        public event EventHandler Deactivated;
        public event EventHandler StateChanged;
        public event EventHandler LocationChanged;
        public event CancelEventHandler Closing;
        public event EventHandler Closed;
        public event EventHandler ContentRendered;
    }
}
