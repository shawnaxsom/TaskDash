using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TaskDash.Core.ExtensionMethods
{
    public static class Bools
    {
        public static Visibility ToVisible(this bool setVisible)
        {
            if (setVisible)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public static bool FromVisible(Visibility visible)
        {
            if (visible == Visibility.Visible)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Visibility Toggle(this Visibility current)
        {
            if (current == Visibility.Visible)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }
    }
}
