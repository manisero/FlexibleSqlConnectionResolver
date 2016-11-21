using System.Linq;
using Dapper;
using FlexibleSqlConnectionResolver.ConnectionResolution;

namespace FlexibleSqlConnectionResolver.UseCases.SampleApplication
{
    public class OrderItemCreatorInput
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
    }

    public class OrderItemCreator
    {
        private readonly ISqlConnectionResolver _sqlConnectionResolver;

        public OrderItemCreator(ISqlConnectionResolver sqlConnectionResolver)
        {
            _sqlConnectionResolver = sqlConnectionResolver;
        }

        public OrderItem Create(OrderItemCreatorInput input)
        {
            const string sql =
@"INSERT INTO [dbo].[OrderItem] ([OrderId], [Name])
OUTPUT inserted.*
VALUES (@OrderId, @Name);";

            using (var cw = _sqlConnectionResolver.Resolve())
            {
                return cw.Connection.Query<OrderItem>(sql, input).Single();
            }
        }
    }
}
