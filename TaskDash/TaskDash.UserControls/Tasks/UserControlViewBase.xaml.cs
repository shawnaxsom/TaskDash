using System;
using System.Windows.Controls;
using System.Windows.Input;
using TaskDash.CustomControls;

namespace TaskDash.UserControls
{
    public class UserControlViewBase : UserControl
    {
        /// <summary>
        /// Checks if focused control is a text box.
        /// This includes text boxes in user controls.
        /// </summary>
        public bool IsEditing
        {
            get
            {
                Type type = Keyboard.FocusedElement.GetType();

                return type == typeof(TextBox);
            }
        }

        protected void AddItemOrFocus(ListBoxWithAddRemove listBox)
        {
            //if (!IsEditing)
            //{
            if (Keyboard.IsKeyDown(Key.LeftShift)
                || Keyboard.IsKeyDown(Key.RightShift))
            {
                listBox.Focus();
            }
            else
            {
                listBox.SimulateClick(listBox.AddButton);
            }
            //}
        }
    }
}