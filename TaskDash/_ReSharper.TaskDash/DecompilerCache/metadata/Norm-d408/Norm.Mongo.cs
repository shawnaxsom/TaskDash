// Type: Norm.Mongo
// Assembly: Norm, Version=1.0.0.0, Culture=neutral
// Assembly location: C:\mongo\lib\Norm.dll

using Norm.Collections;
using Norm.Responses;
using System;

namespace Norm
{
    public class Mongo : IMongo, IDisposable
    {
        public Mongo(IConnectionProvider provider, string options);
        public Mongo(string db, string server, string port, string options);

        #region IMongo Members

        public void Dispose();
        public IMongoCollection<T> GetCollection<T>();
        public IMongoCollection<T> GetCollection<T>(string collectionName);
        public LastErrorResponse LastError();
        public IMongoDatabase Database { get; }
        public IConnectionProvider ConnectionProvider { get; }

        #endregion

        public static IMongo Create(string connectionString);
        public static IMongo Create(string connectionString, string options);
        protected virtual void Dispose(bool disposing);
        ~Mongo();
    }
}
