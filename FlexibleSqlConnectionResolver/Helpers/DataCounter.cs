using System.Data.SqlClient;
using Dapper;

namespace FlexibleSqlConnectionResolver.Helpers
{
    public interface IDataCounter
    {
        DataCounterOutput Count();
    }

    public class DataCounterOutput
    {
        public int OrdersCount { get; set; }
        public int OrderItemsCount { get; set; }
    }

    public class DataCounter : IDataCounter
    {
        private readonly string _connectionString;

        public DataCounter(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataCounterOutput Count()
        {
            const string sql =
@"SELECT COUNT(*) FROM [dbo].[Order];
SELECT COUNT(*) FROM [dbo].[OrderItem];";

            using (var connection = new SqlConnection(_connectionString))
            using (var query = connection.QueryMultiple(sql))
            {
                return new DataCounterOutput
                    {
                        OrdersCount = query.ReadSingle<int>(),
                        OrderItemsCount = query.ReadSingle<int>()
                    };
            }
        }
    }
}
