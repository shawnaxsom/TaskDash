using System;
using System.Collections;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using TaskDash.Controls.ExtensionMethods;
using TaskDash.Core;
using TaskDash.Core.Models.Tasks;

namespace TaskDash.Controls
{
    [TemplatePart(Name = "MyListBox", Type = typeof(ListBox))]
    public class ListBoxWithAddRemove : Control
    {
        private static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("LabelText", typeof(string), typeof(ListBoxWithAddRemove),
                                        new PropertyMetadata(string.Empty, OnLabelTextPropertyChanged));

        private static void OnLabelTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(DataTemplate), typeof(ListBoxWithAddRemove),
                                        new UIPropertyMetadata(null));

        private new static readonly RoutedEvent MouseDoubleClickEvent = EventManager.RegisterRoutedEvent(
            "MouseDoubleClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ListBoxWithAddRemove));

        private ListBox _listBox = new ListBox();
        private ToolTip _tooltip;

        static ListBoxWithAddRemove()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBoxWithAddRemove),
                                                     new FrameworkPropertyMetadata(typeof(ListBoxWithAddRemove)));


            // Allow tabbing to child items of control?? Doesn't seem to work
            FocusableProperty.OverrideMetadata(typeof(ListBoxWithAddRemove), new FrameworkPropertyMetadata(false));
            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(typeof(ListBoxWithAddRemove),
                                                                      new FrameworkPropertyMetadata(
                                                                          KeyboardNavigationMode.Local));
        }

        public ListBoxWithAddRemove()
        {
            this.Focusable = true;

            this.Loaded += new RoutedEventHandler(ListBoxWithAddRemove_Loaded);
        }

        public bool ChildHasFocus
        {
            get
            {
                return MyListBox.IsFocused
                    || GetListBoxItemFromListBox(MyListBox).IsFocused;
            }
        }

        public ListBoxWithAddRemove(ListBoxWithAddRemove control)
        {
            this.Name = control.Name;
            this.LabelText = control.LabelText;
            this.DataContext = control.DataContext;
            this.ItemsSource = control.ItemsSource;

            this.Focusable = true;

            this.Loaded += ListBoxWithAddRemove_Loaded;
        }

        void ListBoxWithAddRemove_Loaded(object sender, RoutedEventArgs e)
        {
            if (MyListBox != null)
            {
                MyListBox.GotKeyboardFocus += MyListBox_GotKeyboardFocus;
                MyListBox.MouseDoubleClick += new MouseButtonEventHandler(MyListBox_MouseDoubleClick);
            }
            this.MouseDown += ListBoxWithAddRemove_MouseDown;

            Window parentWindow = GetWindow();
            parentWindow.LostFocus += new RoutedEventHandler(parentWindow_LostFocus);
        }

        void MyListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SimulateEnterButton();
        }

        void parentWindow_LostFocus(object sender, RoutedEventArgs e)
        {
            HideToolTip();
        }

        public string LabelText
        {
            get
            {
                return GetValue(LabelTextProperty).ToString();
            }
            set { SetValue(LabelTextProperty, value); }
        }

        public Visibility LabelTextVisible
        {
            get
            {
                bool hasLabel = (LabelText != string.Empty);
                if (hasLabel)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        #region Focus Event
        private void ListBoxWithAddRemove_MouseDown(object sender, RoutedEventArgs e)
        {
            RaiseControlFocused();
        }
        void MyListBox_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            RaiseControlFocused();
            ShowToolTip();
        }

        void MyListBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
                                             typeof(ListBoxWithAddRemove));
        #endregion Focus Event

        //void OnListBoxWithAddRemoveMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //if (this.SelectedIndex == -1)
        //{
        //    this.SelectedIndex = this.Count - 1;
        //}

        //if (!this.IsFocused)
        //{
        //    MyListBox.Focus();
        //}

        //var newEventArgs = new RoutedEventArgs(MouseDoubleClickEvent);
        //RaiseEvent(newEventArgs);
        //}

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public ItemCollection Items
        {
            get { return _listBox.Items; }
        }

        public Object SelectedItem
        {
            get { return _listBox.SelectedItem; }
            set { _listBox.SelectedItem = value; }
        }

        public int SelectedIndex
        {
            get { return _listBox.SelectedIndex; }
            set { _listBox.SelectedIndex = value; }
        }

        public int Count
        {
            get { return _listBox.Items.Count; }
        }

        public Button AddButton
        {
            get { return (Button)Template.FindName("_AddButton", this); }
        }

        public Button DeleteButton
        {
            get { return (Button)Template.FindName("_DeleteButton", this); }
        }

        public ListBox MyListBox
        {
            get { return (ListBox)Template.FindName("MyListBox", this); }
        }

        public TextBlock LabelDescription
        {
            get { return (TextBlock)Template.FindName("LabelDescription", this); }
        }
        public TextBox EditableDescription
        {
            get { return (TextBox)Template.FindName("EditableDescription", this); }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            ListBoxItem myListBoxItem = GetListBoxItemFromListBox(MyListBox);
            if (myListBoxItem == null)
            {
                MyListBox.Focus();
            }
            else
            {
                myListBoxItem.Focus();
            }
        }

        public bool IsEditingSelectedItem
        {
            get
            {
                var listBox = (ListBox)Template.FindName("MyListBox", this);
                var item = (IModelBase)SelectedItem;

                if (item == null) return false;


                return (item.IsEditing == Visibility.Visible);
            }
        }

        public string DisplayMemberPath
        {
            get { return _listBox.DisplayMemberPath; }
            set { _listBox.DisplayMemberPath = value; }
        }


        private static childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            if (obj == null)
            {
                return null;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;

                var childOfChild = FindVisualChild<childItem>(child);
                if (childOfChild != null)
                    return childOfChild;
            }
            return null;
        }

        public override void OnApplyTemplate()
        {
            if (Template != null)
            {
                object lb = Template.FindName("MyListBox", this);
                _listBox = (ListBox)lb;

                _listBox.SelectionChanged += OnSelectionChanged;
                _listBox.KeyDown += _listBox_KeyDown;
                _listBox.PreviewKeyDown += new KeyEventHandler(_listBox_PreviewKeyDown);
                _listBox.MouseDoubleClick += _listBox_MouseDoubleClick;
                _listBox.LostFocus += OnLostFocus;
            }
        }

        void OnLostFocus(object sender, RoutedEventArgs e)
        {
            HideToolTip();
        }

        void _listBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((!IsEditingSelectedItem)
                && e.Key == Key.Space)
            {
                ToggleCompleted();
            }
        }

        private void _listBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2
                || (this.IsEditingSelectedItem
                       && (e.Key == Key.Enter
                        || e.Key == Key.Escape)))
            {
                ToggleEditing();
                e.Handled = true;
            }
            else if ((!IsEditingSelectedItem) &&
                     (e.Key == Key.A
                      || e.Key == Key.Insert))
            {
                AddItem();
            }
            else if ((!IsEditingSelectedItem) &&
                     (e.Key == Key.D
                      || e.Key == Key.Delete))
            {
                DeleteItem();
            }
            else if ((!IsEditingSelectedItem) &&
                     (e.Key == Key.J))
            {
                MoveNext();
            }
            else if ((!IsEditingSelectedItem) &&
                     (e.Key == Key.K))
            {
                MovePrevious();
            }
            else if (!this.IsEditingSelectedItem
                       && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                       && e.Key == Key.C)
            {
                CopyToClipboard();
            }
            else if (!this.IsEditingSelectedItem
                       && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                       && e.Key == Key.V)
            {
                AddFromClipboard();
            }
        }

        private void AddFromClipboard()
        {
            var control = (Control)this;
            var listBoxControl = (ListBox)control.Template.FindName("MyListBox", control);
            BindingExpression binding = listBoxControl.GetBindingExpression(ItemsControl.ItemsSourceProperty);


            if (binding.DataItem is ListCollectionView)
            {
                var view = (ListCollectionView)binding.DataItem;

                ModelBase item = (ModelBase)view.AddNew();
                item.EditableValue = Clipboard.GetText();
            }
        }

        protected void CopyToClipboard()
        {
            ModelBase item = MyListBox.SelectedItem as ModelBase;

            if (item == null) return;


            item.CopyToClipboard();
        }

        private void ToggleCompleted()
        {
            ICompletable item = MyListBox.SelectedItem as ICompletable;

            if (item != null)
            {
                item.Completed = !item.Completed;
            }
        }

        private void Move(int indexChange)
        {
            int newIndex = MyListBox.SelectedIndex + indexChange;

            if (newIndex >= 0
                && newIndex <= MyListBox.Items.Count - 1)
            {
                MyListBox.SelectedIndex = newIndex;
            }
        }

        private Window GetWindow()
        {
            var item = VisualTreeHelper.GetParent(this);

            while (item is Window == false
                && item != null)
            {
                item = VisualTreeHelper.GetParent(item);
            }

            return (Window) item;
        }

        private void MovePrevious()
        {
            Move(-1);
        }

        private void MoveNext()
        {
            Move(1);
        }

        private void DeleteItem()
        {
            SimulateClick(DeleteButton);
        }

        private void AddItem()
        {
            SimulateClick(AddButton);
        }

        public void SimulateClick(Button buttonToClick)
        {
            var peer = new ButtonAutomationPeer(buttonToClick);

            var invokeProv =
                peer.GetPattern(PatternInterface.Invoke)
                as IInvokeProvider;

            invokeProv.Invoke();
        }

        public void SimulateEnterButton()
        {
            var source = PresentationSource.FromVisual(CurrentListBoxItem);

            OnKeyDown(new KeyEventArgs(null, source, DateTime.Now.Millisecond, Key.Enter));
        }

        public void ToggleEditing()
        {
            var listBox = (ListBox)Template.FindName("MyListBox", this);
            var item = (IModelBase)SelectedItem;

            if (item == null) return;


            item.ToggleEditing();

            if (item.IsEditing == Visibility.Visible)
            {
                HideToolTip();

                // http://msdn.microsoft.com/en-us/library/system.windows.frameworktemplate.findname.aspx
                ListBoxItem myListBoxItem = GetListBoxItemFromListBox(listBox);
                if (myListBoxItem != null)
                {
                    var myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
                    DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                    var editControl =
                        (TextBox)myDataTemplate.FindName("EditableDescription", myContentPresenter);
                    editControl.Focus();

                    editControl.CaretIndex = editControl.Text.Length;
                }
            }
            else if (item.IsDisplaying == Visibility.Visible)
            {
                listBox.ScrollToLeftEnd();
            }
        }

        private ListBoxItem CurrentListBoxItem
        {
            get { return GetListBoxItemFromListBox(MyListBox); }
        }

        private static ListBoxItem GetListBoxItemFromListBox(ListBox listBox)
        {
            //http://social.msdn.microsoft.com/Forums/en/wpf/thread/12788e8b-570d-4760-888d-b195ebbb6304
            // Update the layout so that ContainerFromItem can find the ListBoxItem. Removes virtualization.
            listBox.UpdateLayout();

            if (listBox.SelectedItems.Count == 0)
            {
                return null;
            }

            var listBoxItem = (ListBoxItem)(listBox.ItemContainerGenerator.ContainerFromItem(listBox.SelectedItems[0]));
            if (listBoxItem == null)
            {
                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    object yourObject = listBox.Items[i];
                    var lbi = (ListBoxItem)listBox.ItemContainerGenerator.ContainerFromItem(yourObject);
                    if (lbi != null && lbi.IsFocused)
                    {
                        listBoxItem = lbi;
                        break;
                    }
                }
            }
            return listBoxItem;
        }

        private void _listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var newEventArgs = new RoutedEventArgs(MouseDoubleClickEvent);
            RaiseEvent(newEventArgs);
        }

        // Create a custom routed event by first registering a RoutedEventID
        // This event uses the bubbling routing strategy

        //public event RoutedEventHandler SelectionChanged;
        // Provide CLR accessors for the event
        public new event RoutedEventHandler MouseDoubleClick
        {
            add { AddHandler(MouseDoubleClickEvent, value); }
            remove { RemoveHandler(MouseDoubleClickEvent, value); }
        }

        // Create a custom routed event by first registering a RoutedEventID
        // This event uses the bubbling routing strategy

        //public event RoutedEventHandler SelectionChanged;
        // Provide CLR accessors for the event
        public new event RoutedEventHandler MouseDown
        {
            add { AddHandler(MouseDownEvent, value); }
            remove { RemoveHandler(MouseDownEvent, value); }
        }

        
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(sender, e);
            }

            ShowToolTip();
        }

        private void ShowToolTip()
        {
            HideToolTip();


            var listBoxItem = GetListBoxItemFromListBox(MyListBox);
            if (listBoxItem != null && SelectedItem != null
                && listBoxItem.IsFocused)
            {
                ModelBase modelItem = ((ModelBase)SelectedItem);
                if (modelItem.EditableValue != null
                    && modelItem.EditableValue.Length >= 30
                    && modelItem.IsEditing == Visibility.Collapsed)
                {
                    _tooltip = new ToolTip()
                                   {
                                       Content = modelItem.EditableValue,
                                       IsOpen = true,
                                       PlacementTarget = GetListBoxItemFromListBox(MyListBox),
                                       Placement = PlacementMode.Relative,
                                       HorizontalOffset = 10
                                   };

                    if (modelItem.EditableValue.Length > 100)
                    {
                        _tooltip.Placement = PlacementMode.Right;
                    }
                }
            }
        }

        protected void HideToolTip()
        {
            if (_tooltip != null)
            {
                _tooltip.IsOpen = false;
                _tooltip = null;
            }
        }

        public event SelectionChangedEventHandler SelectionChanged;

        public ListBoxItem GetFirstListBoxItemFromListBox()
        {
            return _listBox.GetFirstListBoxItemFromListBox();
        }
    }
}