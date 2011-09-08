// Type: System.Windows.Input.ICommand
// Assembly: PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\PresentationCore.dll

using System;
using System.ComponentModel;
using System.Windows.Markup;

namespace System.Windows.Input
{
    [TypeConverter(
        "System.Windows.Input.CommandConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null"
        )]
    [ValueSerializer(
        "System.Windows.Input.CommandValueSerializer, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null"
        )]
    public interface ICommand
    {
        void Execute(object parameter);
        bool CanExecute(object parameter);
        event EventHandler CanExecuteChanged;
    }
}
