using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TaskDash.Commands;
using System.Windows.Input;
using System.Collections.Generic;


namespace TaskDash.Controls.ExtensionMethods
{
    public static class ControlExtensionMethods
    {
        public static void Load(this Button button, ITaskDashCommand command)
        {
            button.Content = command.Text;
            button.Command = (ICommand)command;
        }

        public static void Load(this TextBlock textBlock, params ITaskDashCommand[] commands)
        {
            foreach (ITaskDashCommand command in commands)
            {
                if (String.IsNullOrEmpty(textBlock.Text))
                {
                    textBlock.Text = "You can " + command.Description;
                }
                else
                {
                    textBlock.Text += ", or you can " + command.Description;
                }
            }

            if (!String.IsNullOrEmpty(textBlock.Text))
            {
                textBlock.Text += ".";
            }
        }
    }
}
