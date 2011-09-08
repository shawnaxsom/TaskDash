// Type: System.Windows.UIElement
// Assembly: PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\PresentationCore.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime;
using System.Windows.Automation.Peers;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace System.Windows
{
    [UidProperty("Uid")]
    public class UIElement : Visual, IAnimatable, IInputElement
    {
        public static readonly RoutedEvent PreviewMouseDownEvent;
        public static readonly RoutedEvent MouseDownEvent;
        public static readonly RoutedEvent PreviewMouseUpEvent;
        public static readonly RoutedEvent MouseUpEvent;
        public static readonly RoutedEvent PreviewMouseLeftButtonDownEvent;
        public static readonly RoutedEvent MouseLeftButtonDownEvent;
        public static readonly RoutedEvent PreviewMouseLeftButtonUpEvent;
        public static readonly RoutedEvent MouseLeftButtonUpEvent;
        public static readonly RoutedEvent PreviewMouseRightButtonDownEvent;
        public static readonly RoutedEvent MouseRightButtonDownEvent;
        public static readonly RoutedEvent PreviewMouseRightButtonUpEvent;
        public static readonly RoutedEvent MouseRightButtonUpEvent;
        public static readonly RoutedEvent PreviewMouseMoveEvent;
        public static readonly RoutedEvent MouseMoveEvent;
        public static readonly RoutedEvent PreviewMouseWheelEvent;
        public static readonly RoutedEvent MouseWheelEvent;
        public static readonly RoutedEvent MouseEnterEvent;
        public static readonly RoutedEvent MouseLeaveEvent;
        public static readonly RoutedEvent GotMouseCaptureEvent;
        public static readonly RoutedEvent LostMouseCaptureEvent;
        public static readonly RoutedEvent QueryCursorEvent;
        public static readonly RoutedEvent PreviewStylusDownEvent;
        public static readonly RoutedEvent StylusDownEvent;
        public static readonly RoutedEvent PreviewStylusUpEvent;
        public static readonly RoutedEvent StylusUpEvent;
        public static readonly RoutedEvent PreviewStylusMoveEvent;
        public static readonly RoutedEvent StylusMoveEvent;
        public static readonly RoutedEvent PreviewStylusInAirMoveEvent;
        public static readonly RoutedEvent StylusInAirMoveEvent;
        public static readonly RoutedEvent StylusEnterEvent;
        public static readonly RoutedEvent StylusLeaveEvent;
        public static readonly RoutedEvent PreviewStylusInRangeEvent;
        public static readonly RoutedEvent StylusInRangeEvent;
        public static readonly RoutedEvent PreviewStylusOutOfRangeEvent;
        public static readonly RoutedEvent StylusOutOfRangeEvent;
        public static readonly RoutedEvent PreviewStylusSystemGestureEvent;
        public static readonly RoutedEvent StylusSystemGestureEvent;
        public static readonly RoutedEvent GotStylusCaptureEvent;
        public static readonly RoutedEvent LostStylusCaptureEvent;
        public static readonly RoutedEvent StylusButtonDownEvent;
        public static readonly RoutedEvent StylusButtonUpEvent;
        public static readonly RoutedEvent PreviewStylusButtonDownEvent;
        public static readonly RoutedEvent PreviewStylusButtonUpEvent;
        public static readonly RoutedEvent PreviewKeyDownEvent;
        public static readonly RoutedEvent KeyDownEvent;
        public static readonly RoutedEvent PreviewKeyUpEvent;
        public static readonly RoutedEvent KeyUpEvent;
        public static readonly RoutedEvent PreviewGotKeyboardFocusEvent;
        public static readonly RoutedEvent GotKeyboardFocusEvent;
        public static readonly RoutedEvent PreviewLostKeyboardFocusEvent;
        public static readonly RoutedEvent LostKeyboardFocusEvent;
        public static readonly RoutedEvent PreviewTextInputEvent;
        public static readonly RoutedEvent TextInputEvent;
        public static readonly RoutedEvent PreviewQueryContinueDragEvent;
        public static readonly RoutedEvent QueryContinueDragEvent;
        public static readonly RoutedEvent PreviewGiveFeedbackEvent;
        public static readonly RoutedEvent GiveFeedbackEvent;
        public static readonly RoutedEvent PreviewDragEnterEvent;
        public static readonly RoutedEvent DragEnterEvent;
        public static readonly RoutedEvent PreviewDragOverEvent;
        public static readonly RoutedEvent DragOverEvent;
        public static readonly RoutedEvent PreviewDragLeaveEvent;
        public static readonly RoutedEvent DragLeaveEvent;
        public static readonly RoutedEvent PreviewDropEvent;
        public static readonly RoutedEvent DropEvent;
        public static readonly RoutedEvent PreviewTouchDownEvent;
        public static readonly RoutedEvent TouchDownEvent;
        public static readonly RoutedEvent PreviewTouchMoveEvent;
        public static readonly RoutedEvent TouchMoveEvent;
        public static readonly RoutedEvent PreviewTouchUpEvent;
        public static readonly RoutedEvent TouchUpEvent;
        public static readonly RoutedEvent GotTouchCaptureEvent;
        public static readonly RoutedEvent LostTouchCaptureEvent;
        public static readonly RoutedEvent TouchEnterEvent;
        public static readonly RoutedEvent TouchLeaveEvent;
        public static readonly DependencyProperty IsMouseDirectlyOverProperty;
        public static readonly DependencyProperty IsMouseOverProperty;
        public static readonly DependencyProperty IsStylusOverProperty;
        public static readonly DependencyProperty IsKeyboardFocusWithinProperty;
        public static readonly DependencyProperty IsMouseCapturedProperty;
        public static readonly DependencyProperty IsMouseCaptureWithinProperty;
        public static readonly DependencyProperty IsStylusDirectlyOverProperty;
        public static readonly DependencyProperty IsStylusCapturedProperty;
        public static readonly DependencyProperty IsStylusCaptureWithinProperty;
        public static readonly DependencyProperty IsKeyboardFocusedProperty;
        public static readonly DependencyProperty AreAnyTouchesDirectlyOverProperty;
        public static readonly DependencyProperty AreAnyTouchesOverProperty;
        public static readonly DependencyProperty AreAnyTouchesCapturedProperty;
        public static readonly DependencyProperty AreAnyTouchesCapturedWithinProperty;
        public static readonly DependencyProperty AllowDropProperty;
        public static readonly DependencyProperty RenderTransformProperty;
        public static readonly DependencyProperty RenderTransformOriginProperty;
        public static readonly DependencyProperty OpacityProperty;
        public static readonly DependencyProperty OpacityMaskProperty;
        public static readonly DependencyProperty BitmapEffectProperty;
        public static readonly DependencyProperty EffectProperty;
        public static readonly DependencyProperty BitmapEffectInputProperty;
        public static readonly DependencyProperty CacheModeProperty;
        public static readonly DependencyProperty UidProperty;
        public static readonly DependencyProperty VisibilityProperty;
        public static readonly DependencyProperty ClipToBoundsProperty;
        public static readonly DependencyProperty ClipProperty;
        public static readonly DependencyProperty SnapsToDevicePixelsProperty;
        public static readonly RoutedEvent GotFocusEvent;
        public static readonly RoutedEvent LostFocusEvent;
        public static readonly DependencyProperty IsFocusedProperty;
        public static readonly DependencyProperty IsEnabledProperty;
        public static readonly DependencyProperty IsHitTestVisibleProperty;
        public static readonly DependencyProperty IsVisibleProperty;
        public static readonly DependencyProperty FocusableProperty;
        public static readonly DependencyProperty IsManipulationEnabledProperty;
        public static readonly RoutedEvent ManipulationStartingEvent;
        public static readonly RoutedEvent ManipulationStartedEvent;
        public static readonly RoutedEvent ManipulationDeltaEvent;
        public static readonly RoutedEvent ManipulationInertiaStartingEvent;
        public static readonly RoutedEvent ManipulationBoundaryFeedbackEvent;
        public static readonly RoutedEvent ManipulationCompletedEvent;
        public UIElement();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public InputBindingCollection InputBindings { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CommandBindingCollection CommandBindings { get; }

        public bool AllowDrop { get; set; }
        protected StylusPlugInCollection StylusPlugIns { get; }
        public Size DesiredSize { get; }
        public bool IsMeasureValid { get; }
        public bool IsArrangeValid { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size RenderSize { get; set; }

        public Transform RenderTransform { get; set; }
        public Point RenderTransformOrigin { get; set; }

        public bool IsMouseCaptureWithin { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public bool IsStylusCaptureWithin { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public bool IsInputMethodEnabled { get; }

        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public double Opacity { get; set; }

        public Brush OpacityMask { get; set; }

        [Obsolete(
            "BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
        public BitmapEffect BitmapEffect { get; set; }

        public Effect Effect { get; set; }

        [Obsolete(
            "BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
        public BitmapEffectInput BitmapEffectInput { get; set; }

        public CacheMode CacheMode { get; set; }
        public string Uid { get; set; }

        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public Visibility Visibility { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        public bool ClipToBounds { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        public Geometry Clip { get; set; }

        public bool SnapsToDevicePixels { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        public bool IsFocused { get; }
        protected virtual bool IsEnabledCore { get; }
        public bool IsHitTestVisible { get; set; }

        public bool IsVisible { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Obsolete(
            "PersistId is an obsolete property and may be removed in a future release.  The value of this property is not defined."
            )]
        public int PersistId { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public bool IsManipulationEnabled { get; set; }

        public bool AreAnyTouchesOver { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public bool AreAnyTouchesDirectlyOver { get; }

        public bool AreAnyTouchesCapturedWithin { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public bool AreAnyTouchesCaptured { get; }
        public IEnumerable<TouchDevice> TouchesCaptured { get; }
        public IEnumerable<TouchDevice> TouchesCapturedWithin { get; }
        public IEnumerable<TouchDevice> TouchesOver { get; }
        public IEnumerable<TouchDevice> TouchesDirectlyOver { get; }

        #region IAnimatable Members

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void ApplyAnimationClock(DependencyProperty dp, AnimationClock clock);

        public void ApplyAnimationClock(DependencyProperty dp, AnimationClock clock, HandoffBehavior handoffBehavior);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void BeginAnimation(DependencyProperty dp, AnimationTimeline animation);

        public void BeginAnimation(DependencyProperty dp, AnimationTimeline animation, HandoffBehavior handoffBehavior);
        public object GetAnimationBaseValue(DependencyProperty dp);
        public bool HasAnimatedProperties { get; }

        #endregion

        #region IInputElement Members

        public void RaiseEvent(RoutedEventArgs e);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void AddHandler(RoutedEvent routedEvent, Delegate handler);

        public void RemoveHandler(RoutedEvent routedEvent, Delegate handler);
        public bool CaptureMouse();
        public void ReleaseMouseCapture();
        public bool CaptureStylus();
        public void ReleaseStylusCapture();
        public bool Focus();

        public bool IsMouseDirectlyOver { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public bool IsMouseOver { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public bool IsStylusOver { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public bool IsKeyboardFocusWithin { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public bool IsMouseCaptured { get; }

        public bool IsStylusDirectlyOver { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public bool IsStylusCaptured { get; }

        public bool IsKeyboardFocused { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public bool IsEnabled { get; set; }
        public bool Focusable { get; set; }
        public event MouseButtonEventHandler PreviewMouseLeftButtonDown;
        public event MouseButtonEventHandler MouseLeftButtonDown;
        public event MouseButtonEventHandler PreviewMouseLeftButtonUp;
        public event MouseButtonEventHandler MouseLeftButtonUp;
        public event MouseButtonEventHandler PreviewMouseRightButtonDown;
        public event MouseButtonEventHandler MouseRightButtonDown;
        public event MouseButtonEventHandler PreviewMouseRightButtonUp;
        public event MouseButtonEventHandler MouseRightButtonUp;
        public event MouseEventHandler PreviewMouseMove;
        public event MouseEventHandler MouseMove;
        public event MouseWheelEventHandler PreviewMouseWheel;
        public event MouseWheelEventHandler MouseWheel;
        public event MouseEventHandler MouseEnter;
        public event MouseEventHandler MouseLeave;
        public event MouseEventHandler GotMouseCapture;
        public event MouseEventHandler LostMouseCapture;
        public event StylusDownEventHandler PreviewStylusDown;
        public event StylusDownEventHandler StylusDown;
        public event StylusEventHandler PreviewStylusUp;
        public event StylusEventHandler StylusUp;
        public event StylusEventHandler PreviewStylusMove;
        public event StylusEventHandler StylusMove;
        public event StylusEventHandler PreviewStylusInAirMove;
        public event StylusEventHandler StylusInAirMove;
        public event StylusEventHandler StylusEnter;
        public event StylusEventHandler StylusLeave;
        public event StylusEventHandler PreviewStylusInRange;
        public event StylusEventHandler StylusInRange;
        public event StylusEventHandler PreviewStylusOutOfRange;
        public event StylusEventHandler StylusOutOfRange;
        public event StylusSystemGestureEventHandler PreviewStylusSystemGesture;
        public event StylusSystemGestureEventHandler StylusSystemGesture;
        public event StylusEventHandler GotStylusCapture;
        public event StylusEventHandler LostStylusCapture;
        public event StylusButtonEventHandler StylusButtonDown;
        public event StylusButtonEventHandler StylusButtonUp;
        public event StylusButtonEventHandler PreviewStylusButtonDown;
        public event StylusButtonEventHandler PreviewStylusButtonUp;
        public event KeyEventHandler PreviewKeyDown;
        public event KeyEventHandler KeyDown;
        public event KeyEventHandler PreviewKeyUp;
        public event KeyEventHandler KeyUp;
        public event KeyboardFocusChangedEventHandler PreviewGotKeyboardFocus;
        public event KeyboardFocusChangedEventHandler GotKeyboardFocus;
        public event KeyboardFocusChangedEventHandler PreviewLostKeyboardFocus;
        public event KeyboardFocusChangedEventHandler LostKeyboardFocus;
        public event TextCompositionEventHandler PreviewTextInput;
        public event TextCompositionEventHandler TextInput;

        #endregion

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeInputBindings();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeCommandBindings();

        public void AddHandler(RoutedEvent routedEvent, Delegate handler, bool handledEventsToo);

        public void AddToEventRoute(EventRoute route, RoutedEventArgs e);
        protected virtual void OnPreviewMouseDown(MouseButtonEventArgs e);
        protected virtual void OnMouseDown(MouseButtonEventArgs e);
        protected virtual void OnPreviewMouseUp(MouseButtonEventArgs e);
        protected virtual void OnMouseUp(MouseButtonEventArgs e);
        protected virtual void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e);
        protected virtual void OnMouseLeftButtonDown(MouseButtonEventArgs e);
        protected virtual void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e);
        protected virtual void OnMouseLeftButtonUp(MouseButtonEventArgs e);
        protected virtual void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e);
        protected virtual void OnMouseRightButtonDown(MouseButtonEventArgs e);
        protected virtual void OnPreviewMouseRightButtonUp(MouseButtonEventArgs e);
        protected virtual void OnMouseRightButtonUp(MouseButtonEventArgs e);
        protected virtual void OnPreviewMouseMove(MouseEventArgs e);
        protected virtual void OnMouseMove(MouseEventArgs e);
        protected virtual void OnPreviewMouseWheel(MouseWheelEventArgs e);
        protected virtual void OnMouseWheel(MouseWheelEventArgs e);
        protected virtual void OnMouseEnter(MouseEventArgs e);
        protected virtual void OnMouseLeave(MouseEventArgs e);
        protected virtual void OnGotMouseCapture(MouseEventArgs e);
        protected virtual void OnLostMouseCapture(MouseEventArgs e);
        protected virtual void OnQueryCursor(QueryCursorEventArgs e);
        protected virtual void OnPreviewStylusDown(StylusDownEventArgs e);
        protected virtual void OnStylusDown(StylusDownEventArgs e);
        protected virtual void OnPreviewStylusUp(StylusEventArgs e);
        protected virtual void OnStylusUp(StylusEventArgs e);
        protected virtual void OnPreviewStylusMove(StylusEventArgs e);
        protected virtual void OnStylusMove(StylusEventArgs e);
        protected virtual void OnPreviewStylusInAirMove(StylusEventArgs e);
        protected virtual void OnStylusInAirMove(StylusEventArgs e);
        protected virtual void OnStylusEnter(StylusEventArgs e);
        protected virtual void OnStylusLeave(StylusEventArgs e);
        protected virtual void OnPreviewStylusInRange(StylusEventArgs e);
        protected virtual void OnStylusInRange(StylusEventArgs e);
        protected virtual void OnPreviewStylusOutOfRange(StylusEventArgs e);
        protected virtual void OnStylusOutOfRange(StylusEventArgs e);
        protected virtual void OnPreviewStylusSystemGesture(StylusSystemGestureEventArgs e);
        protected virtual void OnStylusSystemGesture(StylusSystemGestureEventArgs e);
        protected virtual void OnGotStylusCapture(StylusEventArgs e);
        protected virtual void OnLostStylusCapture(StylusEventArgs e);
        protected virtual void OnStylusButtonDown(StylusButtonEventArgs e);
        protected virtual void OnStylusButtonUp(StylusButtonEventArgs e);
        protected virtual void OnPreviewStylusButtonDown(StylusButtonEventArgs e);
        protected virtual void OnPreviewStylusButtonUp(StylusButtonEventArgs e);
        protected virtual void OnPreviewKeyDown(KeyEventArgs e);
        protected virtual void OnKeyDown(KeyEventArgs e);
        protected virtual void OnPreviewKeyUp(KeyEventArgs e);
        protected virtual void OnKeyUp(KeyEventArgs e);
        protected virtual void OnPreviewGotKeyboardFocus(KeyboardFocusChangedEventArgs e);
        protected virtual void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e);
        protected virtual void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e);
        protected virtual void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e);
        protected virtual void OnPreviewTextInput(TextCompositionEventArgs e);
        protected virtual void OnTextInput(TextCompositionEventArgs e);
        protected virtual void OnPreviewQueryContinueDrag(QueryContinueDragEventArgs e);
        protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs e);
        protected virtual void OnPreviewGiveFeedback(GiveFeedbackEventArgs e);
        protected virtual void OnGiveFeedback(GiveFeedbackEventArgs e);
        protected virtual void OnPreviewDragEnter(DragEventArgs e);
        protected virtual void OnDragEnter(DragEventArgs e);
        protected virtual void OnPreviewDragOver(DragEventArgs e);
        protected virtual void OnDragOver(DragEventArgs e);
        protected virtual void OnPreviewDragLeave(DragEventArgs e);
        protected virtual void OnDragLeave(DragEventArgs e);
        protected virtual void OnPreviewDrop(DragEventArgs e);
        protected virtual void OnDrop(DragEventArgs e);
        protected virtual void OnPreviewTouchDown(TouchEventArgs e);
        protected virtual void OnTouchDown(TouchEventArgs e);
        protected virtual void OnPreviewTouchMove(TouchEventArgs e);
        protected virtual void OnTouchMove(TouchEventArgs e);
        protected virtual void OnPreviewTouchUp(TouchEventArgs e);
        protected virtual void OnTouchUp(TouchEventArgs e);
        protected virtual void OnGotTouchCapture(TouchEventArgs e);
        protected virtual void OnLostTouchCapture(TouchEventArgs e);
        protected virtual void OnTouchEnter(TouchEventArgs e);
        protected virtual void OnTouchLeave(TouchEventArgs e);
        protected virtual void OnIsMouseDirectlyOverChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnIsMouseCapturedChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnIsMouseCaptureWithinChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnIsStylusDirectlyOverChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnIsStylusCapturedChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnIsStylusCaptureWithinChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnIsKeyboardFocusedChanged(DependencyPropertyChangedEventArgs e);
        public void InvalidateMeasure();
        public void InvalidateArrange();
        public void InvalidateVisual();
        protected virtual void OnChildDesiredSizeChanged(UIElement child);
        public void Measure(Size availableSize);
        public void Arrange(Rect finalRect);
        protected virtual void OnRender(DrawingContext drawingContext);
        protected internal virtual void OnRenderSizeChanged(SizeChangedInfo info);
        protected virtual Size MeasureCore(Size availableSize);
        protected virtual void ArrangeCore(Rect finalRect);
        protected internal override void OnVisualParentChanged(DependencyObject oldParent);
        protected internal virtual DependencyObject GetUIParentCore();
        public void UpdateLayout();
        public Point TranslatePoint(Point point, UIElement relativeTo);
        public IInputElement InputHitTest(Point point);
        public virtual bool MoveFocus(TraversalRequest request);
        public virtual DependencyObject PredictFocus(FocusNavigationDirection direction);
        protected virtual void OnAccessKey(AccessKeyEventArgs e);
        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters);
        protected override GeometryHitTestResult HitTestCore(GeometryHitTestParameters hitTestParameters);
        protected virtual Geometry GetLayoutClip(Size layoutSlotSize);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        protected virtual void OnGotFocus(RoutedEventArgs e);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        protected virtual void OnLostFocus(RoutedEventArgs e);

        protected virtual AutomationPeer OnCreateAutomationPeer();
        protected virtual void OnManipulationStarting(ManipulationStartingEventArgs e);
        protected virtual void OnManipulationStarted(ManipulationStartedEventArgs e);
        protected virtual void OnManipulationDelta(ManipulationDeltaEventArgs e);
        protected virtual void OnManipulationInertiaStarting(ManipulationInertiaStartingEventArgs e);
        protected virtual void OnManipulationBoundaryFeedback(ManipulationBoundaryFeedbackEventArgs e);
        protected virtual void OnManipulationCompleted(ManipulationCompletedEventArgs e);
        public bool CaptureTouch(TouchDevice touchDevice);
        public bool ReleaseTouchCapture(TouchDevice touchDevice);
        public void ReleaseAllTouchCaptures();
        public event MouseButtonEventHandler PreviewMouseDown;
        public event MouseButtonEventHandler MouseDown;
        public event MouseButtonEventHandler PreviewMouseUp;
        public event MouseButtonEventHandler MouseUp;
        public event QueryCursorEventHandler QueryCursor;
        public event QueryContinueDragEventHandler PreviewQueryContinueDrag;
        public event QueryContinueDragEventHandler QueryContinueDrag;
        public event GiveFeedbackEventHandler PreviewGiveFeedback;
        public event GiveFeedbackEventHandler GiveFeedback;
        public event DragEventHandler PreviewDragEnter;
        public event DragEventHandler DragEnter;
        public event DragEventHandler PreviewDragOver;
        public event DragEventHandler DragOver;
        public event DragEventHandler PreviewDragLeave;
        public event DragEventHandler DragLeave;
        public event DragEventHandler PreviewDrop;
        public event DragEventHandler Drop;
        public event EventHandler<TouchEventArgs> PreviewTouchDown;
        public event EventHandler<TouchEventArgs> TouchDown;
        public event EventHandler<TouchEventArgs> PreviewTouchMove;
        public event EventHandler<TouchEventArgs> TouchMove;
        public event EventHandler<TouchEventArgs> PreviewTouchUp;
        public event EventHandler<TouchEventArgs> TouchUp;
        public event EventHandler<TouchEventArgs> GotTouchCapture;
        public event EventHandler<TouchEventArgs> LostTouchCapture;
        public event EventHandler<TouchEventArgs> TouchEnter;
        public event EventHandler<TouchEventArgs> TouchLeave;
        public event DependencyPropertyChangedEventHandler IsMouseDirectlyOverChanged;
        public event DependencyPropertyChangedEventHandler IsKeyboardFocusWithinChanged;
        public event DependencyPropertyChangedEventHandler IsMouseCapturedChanged;
        public event DependencyPropertyChangedEventHandler IsMouseCaptureWithinChanged;
        public event DependencyPropertyChangedEventHandler IsStylusDirectlyOverChanged;
        public event DependencyPropertyChangedEventHandler IsStylusCapturedChanged;
        public event DependencyPropertyChangedEventHandler IsStylusCaptureWithinChanged;
        public event DependencyPropertyChangedEventHandler IsKeyboardFocusedChanged;
        public event EventHandler LayoutUpdated;
        public event RoutedEventHandler GotFocus;
        public event RoutedEventHandler LostFocus;
        public event DependencyPropertyChangedEventHandler IsEnabledChanged;
        public event DependencyPropertyChangedEventHandler IsHitTestVisibleChanged;
        public event DependencyPropertyChangedEventHandler IsVisibleChanged;
        public event DependencyPropertyChangedEventHandler FocusableChanged;
        public event EventHandler<ManipulationStartingEventArgs> ManipulationStarting;
        public event EventHandler<ManipulationStartedEventArgs> ManipulationStarted;
        public event EventHandler<ManipulationDeltaEventArgs> ManipulationDelta;
        public event EventHandler<ManipulationInertiaStartingEventArgs> ManipulationInertiaStarting;
        public event EventHandler<ManipulationBoundaryFeedbackEventArgs> ManipulationBoundaryFeedback;
        public event EventHandler<ManipulationCompletedEventArgs> ManipulationCompleted;
    }
}
