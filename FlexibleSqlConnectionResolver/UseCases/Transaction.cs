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

        public override void Right()
        {
            // Create and dispose single connection per transaction (e.g. web request)
            
            using (var connectionResolver = new SingletonSqlConnectionResolver(_connectionString))
            {
                TryCreateOrder(connectionResolver, "Order1");
            }

            using (var connectionResolver = new SingletonSqlConnectionResolver(_connectionString))
            {
                TryCreateOrder(connectionResolver, "Order2");
            }
        }

        public override void Wrong1()
        {
            // Create multiple connections per transaction

            using (var connectionResolver = new PerResolveSqlConnectionResolver(_connectionString))
            {
                TryCreateOrder(connectionResolver, "Order1");
                TryCreateOrder(connectionResolver, "Order2");
            }
        }

        public override void Wrong2()
        {
            // Create too-long-living connection, possibly used by different threads

            using (var connectionResolver = new SingletonSqlConnectionResolver(_connectionString))
            {
                TryCreateOrder(connectionResolver, "Order1");
                TryCreateOrder(connectionResolver, "Order2");
            }
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
