using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TaskDash.Controls;

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
            Icon = new BitmapImage(new Uri(@"C:\Users\Shawn.Axsom\Desktop\TaskDash.ico"));
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
    }
}
