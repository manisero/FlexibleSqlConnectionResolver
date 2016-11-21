using System;
using System.Data.SqlClient;

namespace FlexibleSqlConnectionResolver.ConnectionResolution
{
    public class SingletonSqlConnectionResolver : ISqlConnectionResolver
    {
        private readonly string _connectionString;
        private readonly Lazy<SqlConnection> _connection;

        public SingletonSqlConnectionResolver(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new Lazy<SqlConnection>(() => new SqlConnection(_connectionString));
        }

        public ISqlConnectionWrapper Resolve()
        {
            return new EmptySqlConnectionWrapper(_connection.Value);
        }

        public void Dispose()
        {
            if (_connection.IsValueCreated)
            {
                _connection.Value.Dispose();
            }
        }
    }
}
