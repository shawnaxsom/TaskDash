// Type: Norm.IConnectionProvider
// Assembly: Norm, Version=1.0.0.0, Culture=neutral
// Assembly location: C:\mongo\lib\Norm.dll

namespace Norm
{
    public interface IConnectionProvider
    {
        ConnectionStringBuilder ConnectionString { get; }
        IConnection Open(string options);
        void Close(IConnection connection);
    }
}
