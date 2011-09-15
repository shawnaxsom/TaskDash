using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TaskDash.CustomControls;

namespace TaskDash
{
    /// <summary>
    /// Interaction logic for DockWindow.xaml
    /// </summary>
    public partial class DockWindow : Window
    {
        private readonly IMainWindow _window;

        public DockWindow(IMainWindow window)
        {
            InitializeComponent();

            _window = window;
        }

        public void AddControl(Control control)
        {
            if (control is ListBoxWithAddRemove)
            {
                this.AddControl((ListBoxWithAddRemove)control);
            }
            else if (control is TextBoxWithDescription)
            {
                this.AddControl((TextBoxWithDescription)control);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void AddControl(ListBoxWithAddRemove control)
        {
            foreach (Control existing in MyStackPanel.Children)
            {
                if (existing.Name == control.Name)
                {
                    return;
                }
            }

            ListBoxWithAddRemove newControl = new ListBoxWithAddRemove(control);
            MyStackPanel.Children.Add(newControl);
        }
        public void AddControl(TextBoxWithDescription control)
        {
            foreach (Control existing in MyStackPanel.Children)
            {
                if (existing.Name == control.Name)
                {
                    return;
                }
            }

            TextBoxWithDescription newControl = new TextBoxWithDescription(control);
            MyStackPanel.Children.Add(newControl);
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            _window.Show();
        }

        private void buttonIssueTracker_Click(object sender, RoutedEventArgs e)
        {
            _window.OpenIssueTracker();
        }

        public void AddControls(List<Control> defaultDockingControls)
        {
            foreach (Control control in defaultDockingControls)
            {
                this.AddControl(control);
            }
        }
    }
}
