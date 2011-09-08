// Type: System.Configuration.SingleTagSectionHandler
// Assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.dll

using System.Runtime;
using System.Xml;

namespace System.Configuration
{
    public class SingleTagSectionHandler : IConfigurationSectionHandler
    {
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public SingleTagSectionHandler();

        #region IConfigurationSectionHandler Members

        public virtual object Create(object parent, object context, XmlNode section);

        #endregion
    }
}
