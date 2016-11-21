using System.Transactions;
using FlexibleSqlConnectionResolver.ConnectionResolution;
using FlexibleSqlConnectionResolver.UseCases.SampleApplication;

namespace FlexibleSqlConnectionResolver.UseCases
{
    public class Transaction : UseCase
    {
        public Transaction(string connectionString)
            : base(connectionString)
        {
        }

        public override void RunRight()
        {
            TryCreateOrder(new SingletonSqlConnectionResolver(_connectionString), "Order1");
            TryCreateOrder(new SingletonSqlConnectionResolver(_connectionString), "Order2");
        }

        public override void RunWrong()
        {
            TryCreateOrder(new PerResolveSqlConnectionResolver(_connectionString), "Order1");
            TryCreateOrder(new PerResolveSqlConnectionResolver(_connectionString), "Order2");
        }

        private void TryCreateOrder(ISqlConnectionResolver sqlConnectionResolver, string orderName)
        {
            var orderCreator = new OrderCreator(sqlConnectionResolver);
            var orderItemCreator = new OrderItemCreator(sqlConnectionResolver);

            using (var transaction = new TransactionScope())
            {
                var order = orderCreator.Create(new OrderCreatorInput
                                                    {
                                                        Name = orderName
                                                    });

                orderItemCreator.Create(new OrderItemCreatorInput
                                            {
                                                OrderId = order.OrderId,
                                                Name = "Item"
                                            });

                transaction.Complete();
            }
        }
    }
}
