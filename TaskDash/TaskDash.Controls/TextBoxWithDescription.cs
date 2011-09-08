using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TaskDash.Controls
{
    [TemplatePart(Name = "Text", Type = typeof (TextBox))]
    [TemplatePart(Name = "LabelText", Type = typeof (TextBlock))]
    public sealed class TextBoxWithDescription : Control
    {
        private static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("LabelText", typeof (string), typeof (TextBoxWithDescription),
                                        new PropertyMetadata(string.Empty, OnLabelTextPropertyChanged));

        private static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof (string), typeof (TextBoxWithDescription),
                                        new UIPropertyMetadata(null,
                                                               OnTextChanged
                                            ));

        private static readonly RoutedEvent TextChangedEvent =
            EventManager.RegisterRoutedEvent("TextChanged",
                                             RoutingStrategy.Bubble,
                                             typeof (RoutedEventHandler),
                                             typeof (TextBoxWithDescription));

        static TextBoxWithDescription()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (TextBoxWithDescription),
                                                     new FrameworkPropertyMetadata(typeof (TextBoxWithDescription)));
        }

        public TextBoxWithDescription()
        {
            InitializeControl();
        }

        public TextBoxWithDescription(TextBoxWithDescription control)
        {
            InitializeControl();

            this.Name = control.Name;
            this.LabelText = control.LabelText;
            this.DataContext = control.DataContext;

            var binding = control.GetBindingExpression(TextBoxWithDescription.TextProperty);
            SetBinding(TextProperty, binding.ParentBindingBase);
            
            this.Height = control.Height;
        }


        public TextBox MyTextBox
        {
            get
            {
                if (Template == null)
                    return null;
                return Template.FindName("MyTextBox", this) as TextBox;
            }
        }

        public string LabelText
        {
            get
            {
                return GetValue(LabelTextProperty).ToString();
            }
            set { SetValue(LabelTextProperty, value); }
        }

        public string Text
        {
            get
            {
                var value = GetValue(TextProperty);
                if (value == null)
                {
                    return string.Empty;
                }
                return value.ToString();
            }
            set { SetValue(TextProperty, value); }
        }

        private void InitializeControl()
        {
            Focusable = true;

            LabelText = String.Empty;
            Text = String.Empty;

            IsTabStop = false;

            this.Loaded += new RoutedEventHandler(TextBoxWithDescription_Loaded);
        }

        void TextBoxWithDescription_Loaded(object sender, RoutedEventArgs e)
        {
            MyTextBox.MouseDown += MyTextBox_MouseDown;
            MyTextBox.GotKeyboardFocus += MyTextBox_GotKeyboardFocus;
        }

        #region Focus Event
        void MyTextBox_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            RaiseControlFocused();
        }

        void MyTextBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RaiseControlFocused();
        }

        private void RaiseControlFocused()
        {
            RaiseEvent(new RoutedEventArgs(ControlFocusedEvent, this));
        }
        public event RoutedEventHandler ControlFocused
        {
            add { AddHandler(ControlFocusedEvent, value); }
            remove { RemoveHandler(ControlFocusedEvent, value); }
        }
        private static readonly RoutedEvent ControlFocusedEvent =
            EventManager.RegisterRoutedEvent("ControlFocused",
                                             RoutingStrategy.Bubble,
                                             typeof(RoutedEventHandler),
                                             typeof(TextBoxWithDescription));
        #endregion Focus Event


        protected override void OnGotFocus(RoutedEventArgs e)
        {
            MyTextBox.Focus();
        }

        private static void OnLabelTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        // http://xamlcoder.com/cs/blogs/joe/archive/2007/12/13/building-custom-template-able-wpf-controls.aspx

        private static void OnTextChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var textBox = o as TextBoxWithDescription;
            if (textBox != null)
                textBox.OnTextChanged((String) e.OldValue, (String) e.NewValue);
        }

        private void OnTextChanged(String oldValue, String newValue)
        {
            // fire text changed event
            Text = newValue;
            RaiseEvent(new RoutedEventArgs(TextChangedEvent, this));
        }


        public event RoutedEventHandler TextChanged
        {
            add { AddHandler(TextChangedEvent, value); }
            remove { RemoveHandler(TextChangedEvent, value); }
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //var textBlock = (TextBlock)this.Template.FindName("LabelText", this);
            //if (textBlock != null) textBlock.Text = this.LabelText;


            //var textBox = (TextBox)this.Template.FindName("Text", this);
            //if (textBox != null) textBox.Text = this.Text;
        }
    }
}