using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskDash.Core.Models.Tasks;

namespace TaskDash.CustomControls
{
    /// <summary>
    /// Interaction logic for ListBoxLinks.xaml
    /// </summary>
    public partial class ListBoxLinks : ListBoxWithAddRemove
    {
        public ListBoxLinks()
        {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!this.IsEditingSelectedItem
                       && e.Key == Key.Enter)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift)
                    || Keyboard.IsKeyDown(Key.RightShift))
                {
                    OpenPath();
                }
                else
                {
                    OpenLink();
                }
            }
        }


        private void OpenPath()
        {
            Link link = MyListBox.SelectedItem as Link;

            if (link == null) return;


            link.ExecutePath();
        }

        private void OpenLink()
        {
            Link link = MyListBox.SelectedItem as Link;

            if (link == null) return;


            link.Execute();
        }

        private void CopyToClipboard()
        {
            Link link = MyListBox.SelectedItem as Link;

            if (link == null) return;


            link.CopyToClipboard();
        }
    }
}
