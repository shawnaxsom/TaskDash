// Type: System.Windows.Media.Visual
// Assembly: PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\PresentationCore.dll

using System;
using System.Runtime;
using System.Windows;
using System.Windows.Media.Composition;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;

namespace System.Windows.Media
{
    public abstract class Visual : DependencyObject, DUCE.IResource
    {
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        protected Visual();

        protected virtual int VisualChildrenCount { get; }
        protected DependencyObject VisualParent { get; }
        protected internal Transform VisualTransform { get; protected set; }
        protected internal Effect VisualEffect { get; protected set; }

        [Obsolete(
            "BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
        protected internal BitmapEffect VisualBitmapEffect { get; protected set; }

        [Obsolete(
            "BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
        protected internal BitmapEffectInput VisualBitmapEffectInput { get; protected set; }

        protected internal CacheMode VisualCacheMode { get; protected set; }
        protected internal Rect? VisualScrollableAreaClip { get; protected set; }

        protected internal Geometry VisualClip { get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        protected set; }

        protected internal Vector VisualOffset { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; protected set; }

        protected internal double VisualOpacity { get; protected set; }
        protected internal EdgeMode VisualEdgeMode { get; protected set; }
        protected internal BitmapScalingMode VisualBitmapScalingMode { get; protected set; }
        protected internal ClearTypeHint VisualClearTypeHint { get; set; }
        protected internal TextRenderingMode VisualTextRenderingMode { get; set; }
        protected internal TextHintingMode VisualTextHintingMode { get; set; }
        protected internal Brush VisualOpacityMask { get; protected set; }
        protected internal DoubleCollection VisualXSnappingGuidelines { get; protected set; }
        protected internal DoubleCollection VisualYSnappingGuidelines { get; protected set; }
        protected virtual HitTestResult HitTestCore(PointHitTestParameters hitTestParameters);
        protected virtual GeometryHitTestResult HitTestCore(GeometryHitTestParameters hitTestParameters);
        protected virtual Visual GetVisualChild(int index);
        protected void AddVisualChild(Visual child);
        protected void RemoveVisualChild(Visual child);
        protected internal virtual void OnVisualParentChanged(DependencyObject oldParent);

        protected internal virtual void OnVisualChildrenChanged(DependencyObject visualAdded,
                                                                DependencyObject visualRemoved);

        public bool IsAncestorOf(DependencyObject descendant);
        public bool IsDescendantOf(DependencyObject ancestor);
        public DependencyObject FindCommonVisualAncestor(DependencyObject otherVisual);
        public GeneralTransform TransformToAncestor(Visual ancestor);
        public GeneralTransform2DTo3D TransformToAncestor(Visual3D ancestor);
        public GeneralTransform TransformToDescendant(Visual descendant);
        public GeneralTransform TransformToVisual(Visual visual);
        public Point PointToScreen(Point point);
        public Point PointFromScreen(Point point);
    }
}
