// Type: System.Windows.RoutedEvent
// Assembly: PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\Profile\Client\PresentationCore.dll

using System;
using System.ComponentModel;
using System.Runtime;
using System.Windows.Markup;

namespace System.Windows
{
    [TypeConverter(
        "System.Windows.Markup.RoutedEventConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null"
        )]
    [ValueSerializer(
        "System.Windows.Markup.RoutedEventValueSerializer, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null"
        )]
    public sealed class RoutedEvent
    {
        internal RoutedEvent(string name, RoutingStrategy routingStrategy, Type handlerType, Type ownerType);

        public string Name { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public RoutingStrategy RoutingStrategy { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public Type HandlerType { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public Type OwnerType { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        internal int GlobalIndex { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public RoutedEvent AddOwner(Type ownerType);
        internal bool IsLegalHandler(Delegate handler);
        public override string ToString();
    }
}
