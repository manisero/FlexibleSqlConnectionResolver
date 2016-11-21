using System;
using System.Configuration;
using FlexibleSqlConnectionResolver.Helpers;
using FlexibleSqlConnectionResolver.UseCases;

namespace FlexibleSqlConnectionResolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var databaseCleaner = new DatabaseCleaner(connectionString);
            var dataCounter = new DataCounter(connectionString);

            var useCases = new UseCase[]
                {
                    new Transaction(connectionString),
                    new NoTransaction(connectionString)
                };

            foreach (var useCase in useCases)
            {
                // Clean
                databaseCleaner.Clean();

                Console.WriteLine(useCase.GetType().Name);
                Console.WriteLine();

                // Right
                Console.WriteLine("Right:");

                try
                {
                    useCase.RunRight();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                PrintDataCount(dataCounter);
                Console.WriteLine();

                // Clean
                databaseCleaner.Clean();

                // Wrong
                Console.WriteLine("Wrong:");

                try
                {
                    useCase.RunWrong();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                PrintDataCount(dataCounter);
                Console.WriteLine();

                // Clean
                databaseCleaner.Clean();
                
                Console.WriteLine("----------");
                Console.WriteLine();
            }
        }

        private static void PrintDataCount(IDataCounter dataCounter)
        {
            var count = dataCounter.Count();

            Console.WriteLine($"Orders: {count.OrdersCount}");
            Console.WriteLine($"OrderItems: {count.OrderItemsCount}");
        }
    }
}
