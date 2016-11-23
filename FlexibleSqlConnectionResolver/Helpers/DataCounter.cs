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
        public int CompleteTasksCount { get; set; }
        public int IncompleteTasksCount { get; set; }
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
SELECT COUNT(*) FROM [dbo].[OrderItem];
SELECT COUNT(*) FROM [dbo].[Task] WHERE [IsComplete] = 1;
SELECT COUNT(*) FROM [dbo].[Task] WHERE [IsComplete] = 0;";

            using (var connection = new SqlConnection(_connectionString))
            using (var query = connection.QueryMultiple(sql))
            {
                return new DataCounterOutput
                    {
                        OrdersCount = query.ReadSingle<int>(),
                        OrderItemsCount = query.ReadSingle<int>(),
                        CompleteTasksCount = query.ReadSingle<int>(),
                        IncompleteTasksCount = query.ReadSingle<int>()
                    };
            }
        }
    }
}
