using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace TaskDash.Controls.ExtensionMethods
{
    public static class ControlExtensionMethods
    {
        public static void ScrollToLeftEnd(this ListBox listBox)
        {
            ScrollViewer myScrollviewer = FindVisualChild<ScrollViewer>(listBox);
            myScrollviewer.ScrollToLeftEnd();
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
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        public static ListBoxItem GetLastListBoxItemFromListBox(this ListBox listBox)
        {
            //http://social.msdn.microsoft.com/Forums/en/wpf/thread/12788e8b-570d-4760-888d-b195ebbb6304
            // Update the layout so that ContainerFromItem can find the ListBoxItem. Removes virtualization.
            listBox.UpdateLayout();

            if (listBox.Items.Count == 0)
            {
                return null;
            }

            ListBoxItem listBoxItem = (ListBoxItem)(listBox.ItemContainerGenerator.ContainerFromItem(listBox.Items[listBox.Items.Count - 1]));
            if (listBoxItem == null)
            {
                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    object yourObject = listBox.Items[i];
                    ListBoxItem lbi = (ListBoxItem)listBox.ItemContainerGenerator.ContainerFromItem(yourObject);
                    if (lbi != null
                        && lbi.IsFocused)
                    {
                        listBoxItem = lbi;
                        break;
                    }
                }
            }
            return listBoxItem;
        }

        public static ListBoxItem GetFirstListBoxItemFromListBox(this ListBox listBox)
        {
            //http://social.msdn.microsoft.com/Forums/en/wpf/thread/12788e8b-570d-4760-888d-b195ebbb6304
            // Update the layout so that ContainerFromItem can find the ListBoxItem. Removes virtualization.
            listBox.UpdateLayout();

            if (listBox.Items.Count == 0)
            {
                return null;
            }

            ListBoxItem listBoxItem = (ListBoxItem)(listBox.ItemContainerGenerator.ContainerFromItem(listBox.Items[0]));
            if (listBoxItem == null)
            {
                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    object yourObject = listBox.Items[i];
                    ListBoxItem lbi = (ListBoxItem)listBox.ItemContainerGenerator.ContainerFromItem(yourObject);
                    if (lbi.IsFocused)
                    {
                        listBoxItem = lbi;
                        break;
                    }
                }
            }
            return listBoxItem;
        }
    }
}
