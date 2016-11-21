using System.Data.SqlClient;

namespace FlexibleSqlConnectionResolver.ConnectionResolution
{
    public class PerResolveSqlConnectionResolver : ISqlConnectionResolver
    {
        private readonly string _connectionString;

        public PerResolveSqlConnectionResolver(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ISqlConnectionWrapper Resolve()
        {
            return new DisposingSqlConnectionWrapper(new SqlConnection(_connectionString));
        }

        public void Dispose()
        {
        }
    }
}
