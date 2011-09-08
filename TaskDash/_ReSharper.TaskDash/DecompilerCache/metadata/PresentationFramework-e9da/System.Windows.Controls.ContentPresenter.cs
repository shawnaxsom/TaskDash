// Type: System.Windows.Controls.ContentPresenter
// Assembly: PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\Profile\Client\PresentationFramework.dll

using System.ComponentModel;
using System.Windows;

namespace System.Windows.Controls
{
    [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
    public class ContentPresenter : FrameworkElement
    {
        public static readonly DependencyProperty RecognizesAccessKeyProperty;
        public static readonly DependencyProperty ContentProperty;
        public static readonly DependencyProperty ContentTemplateProperty;
        public static readonly DependencyProperty ContentTemplateSelectorProperty;
        public static readonly DependencyProperty ContentStringFormatProperty;
        public static readonly DependencyProperty ContentSourceProperty;
        public ContentPresenter();
        public bool RecognizesAccessKey { get; set; }
        public object Content { get; set; }
        public DataTemplate ContentTemplate { get; set; }
        public DataTemplateSelector ContentTemplateSelector { get; set; }

        [Bindable(true)]
        public string ContentStringFormat { get; set; }

        public string ContentSource { get; set; }
        protected virtual void OnContentTemplateChanged(DataTemplate oldContentTemplate, DataTemplate newContentTemplate);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeContentTemplateSelector();

        protected virtual void OnContentTemplateSelectorChanged(DataTemplateSelector oldContentTemplateSelector,
                                                                DataTemplateSelector newContentTemplateSelector);

        protected virtual void OnContentStringFormatChanged(string oldContentStringFormat, string newContentStringFormat);
        protected override Size MeasureOverride(Size constraint);
        protected override Size ArrangeOverride(Size arrangeSize);
        protected virtual DataTemplate ChooseTemplate();
        protected virtual void OnTemplateChanged(DataTemplate oldTemplate, DataTemplate newTemplate);
    }
}
