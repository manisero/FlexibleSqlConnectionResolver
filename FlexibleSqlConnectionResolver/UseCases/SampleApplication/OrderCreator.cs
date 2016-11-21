using System.Linq;
using Dapper;
using FlexibleSqlConnectionResolver.ConnectionResolution;

namespace FlexibleSqlConnectionResolver.UseCases.SampleApplication
{
    public class OrderCreatorInput
    {
        public string Name { get; set; }
    }

    public class OrderCreator
    {
        private readonly ISqlConnectionResolver _sqlConnectionResolver;

        public OrderCreator(ISqlConnectionResolver sqlConnectionResolver)
        {
            _sqlConnectionResolver = sqlConnectionResolver;
        }

        public Order Create(OrderCreatorInput input)
        {
            const string sql =
@"INSERT INTO [dbo].[Order] ([Name])
OUTPUT inserted.*
VALUES (@Name);";

            using (var cw = _sqlConnectionResolver.Resolve())
            {
                return cw.Connection.Query<Order>(sql, input).Single();
            }
        }
    }
}
