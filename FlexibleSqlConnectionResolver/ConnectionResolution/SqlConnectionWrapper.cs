using System;
using System.Data.SqlClient;

namespace FlexibleSqlConnectionResolver.ConnectionResolution
{
    public interface ISqlConnectionWrapper : IDisposable
    {
        SqlConnection Connection { get; }
    }

    public class EmptySqlConnectionWrapper : ISqlConnectionWrapper
    {
        public EmptySqlConnectionWrapper(SqlConnection connection)
        {
            Connection = connection;
        }

        public SqlConnection Connection { get; }

        public void Dispose()
        {
        }
    }

    public class DisposingSqlConnectionWrapper : ISqlConnectionWrapper
    {
        public DisposingSqlConnectionWrapper(SqlConnection connection)
        {
            Connection = connection;
        }

        public SqlConnection Connection { get; }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
