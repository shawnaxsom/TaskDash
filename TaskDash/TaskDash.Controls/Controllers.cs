using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TaskDash.Controls.ExtensionMethods;
using TaskDash.Core;
using TaskDash.Core.Models.Tasks;

namespace TaskDash.Controls
{
// ReSharper disable UnusedMember.Global
    public partial class Controllers
// ReSharper restore UnusedMember.Global
    {
        private void AddControlClick(object sender, RoutedEventArgs e)
        {
            var control = (Control) e.OriginalSource;
            var parentControl = (Control) control.TemplatedParent;
            var listBoxControl = (ListBox) parentControl.Template.FindName("MyListBox", parentControl);
            BindingExpression binding = listBoxControl.GetBindingExpression(ItemsControl.ItemsSourceProperty);


            if (binding.DataItem is ListCollectionView)
            {
                var view = (ListCollectionView) binding.DataItem;

                view.AddNew();
            }
            else if (binding.DataItem is IModelCollection)
            {
                var items = (IModelCollection) binding.DataItem;
                items.AddNew();
            }

            ListBoxItem listBoxItem = listBoxControl.GetLastListBoxItemFromListBox();
            if (listBoxItem != null)
            {
                listBoxItem.IsSelected = true;
                //listBoxItem.Focus(); // Don't focus here, in case view wants to focus something else, like a related textbox.

                string editableValue = (listBoxItem.Content as ModelBase).EditableValue;
                if (string.IsNullOrEmpty(editableValue))
                {
                    var parent = (ListBoxWithAddRemove) listBoxControl.TemplatedParent;
                    parent.ToggleEditing();
                }
            }
        }

        private void DeleteControlClick(object sender, RoutedEventArgs e)
        {
            var control = (Control)e.OriginalSource;
            var parentControl = (Control)control.TemplatedParent;
            var listBoxControl = (ListBox)parentControl.Template.FindName("MyListBox", parentControl);
            int newIndex = Math.Max(0, listBoxControl.SelectedIndex - 1);
            var item = listBoxControl.SelectedItem;
            
            BindingExpression binding = listBoxControl.GetBindingExpression(ItemsControl.ItemsSourceProperty);

            if (binding.DataItem is ListCollectionView)
            {
                //var view = (ListCollectionView)listBoxControl.DataContext;
                var view = (ListCollectionView)binding.DataItem;
                var items = (IModelCollection)view.SourceCollection;

                items.RemoveItem(item);

                //var view = (ListCollectionView)binding.DataItem;
                
                //if (view.IsAddingNew)
                //{
                //    view.CommitNew();
                //}

                //view.Remove(item);
            }
            else if (listBoxControl.DataContext is IModelCollection)
            {
                var boundItems = (IModelCollection)listBoxControl.DataContext;
                boundItems.RemoveItem(item);
            }
            else if (listBoxControl.DataContext is CollectionViewSource)
            {
                var boundItems = (CollectionViewSource)listBoxControl.DataContext;
                
                if (boundItems.Source is IModelCollection)
                {
                    var items = (IModelCollection) boundItems.Source;
                    items.RemoveItem(item);
                }

                if (item is IMongoDocument)
                {
                    // Save the document deletion
                    (item as IMongoDocument).Delete(); 
                }
            }
            
            listBoxControl.SelectedIndex = newIndex;
        }
    }
}
