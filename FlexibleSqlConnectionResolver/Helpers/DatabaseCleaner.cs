using System.Data.SqlClient;
using Dapper;

namespace FlexibleSqlConnectionResolver.Helpers
{
    public interface IDatabaseCleaner
    {
        void Clean();
    }

    public class DatabaseCleaner : IDatabaseCleaner
    {
        private readonly string _connectionString;

        public DatabaseCleaner(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Clean()
        {
            const string sql =
@"DELETE [dbo].[OrderItem];
DELETE [dbo].[Order];
DELETE [dbo].[Task];";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql);
            }
        }
    }
}
