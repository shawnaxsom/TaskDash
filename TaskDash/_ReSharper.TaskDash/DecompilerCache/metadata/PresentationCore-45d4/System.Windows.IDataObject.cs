// Type: System.Windows.IDataObject
// Assembly: PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\PresentationCore.dll

using System;
using System.Security;

namespace System.Windows
{
    public interface IDataObject
    {
        [SecurityCritical]
        object GetData(string format);

        [SecurityCritical]
        object GetData(Type format);

        [SecurityCritical]
        object GetData(string format, bool autoConvert);

        [SecurityCritical]
        bool GetDataPresent(string format);

        [SecurityCritical]
        bool GetDataPresent(Type format);

        [SecurityCritical]
        bool GetDataPresent(string format, bool autoConvert);

        [SecurityCritical]
        string[] GetFormats();

        [SecurityCritical]
        string[] GetFormats(bool autoConvert);

        [SecurityCritical]
        void SetData(object data);

        [SecurityCritical]
        void SetData(string format, object data);

        [SecurityCritical]
        void SetData(Type format, object data);

        [SecurityCritical]
        void SetData(string format, object data, bool autoConvert);
    }
}
