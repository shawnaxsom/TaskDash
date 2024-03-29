﻿#pragma checksum "..\..\..\..\Notifications\NotificationWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "66A24CFD45CED5B7EB9600C1BBFAE95B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.237
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace TaskDash.Notifications {
    
    
    /// <summary>
    /// NotificationWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class NotificationWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\..\Notifications\NotificationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridWindow;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\Notifications\NotificationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlockTitle;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\Notifications\NotificationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlockDescription;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Notifications\NotificationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxDelay;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\Notifications\NotificationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stackPanelButtons;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Notifications\NotificationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonDismiss;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Notifications\NotificationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Animation.Storyboard storyBoardFadeWindow;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TaskDash;component/notifications/notificationwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Notifications\NotificationWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 6 "..\..\..\..\Notifications\NotificationWindow.xaml"
            ((TaskDash.Notifications.NotificationWindow)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.MakeOpaque);
            
            #line default
            #line hidden
            
            #line 7 "..\..\..\..\Notifications\NotificationWindow.xaml"
            ((TaskDash.Notifications.NotificationWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.OnWindowClosing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.gridWindow = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.textBlockTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.textBlockDescription = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.comboBoxDelay = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.stackPanelButtons = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.buttonDismiss = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\..\Notifications\NotificationWindow.xaml"
            this.buttonDismiss.Click += new System.Windows.RoutedEventHandler(this.DismissNotification);
            
            #line default
            #line hidden
            return;
            case 8:
            this.storyBoardFadeWindow = ((System.Windows.Media.Animation.Storyboard)(target));
            
            #line 41 "..\..\..\..\Notifications\NotificationWindow.xaml"
            this.storyBoardFadeWindow.Completed += new System.EventHandler(this.OnStoryBoardFadeWindowCompleted);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

