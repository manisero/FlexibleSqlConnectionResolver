using System;
using System.Configuration;
using FlexibleSqlConnectionResolver.Helpers;
using FlexibleSqlConnectionResolver.UseCases;

namespace FlexibleSqlConnectionResolver
{
    class Program
    {
        private static IDatabaseCleaner _databaseCleaner;
        private static IDataCounter _dataCounter;

        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            _databaseCleaner = new DatabaseCleaner(connectionString);
            _dataCounter = new DataCounter(connectionString);

            var useCases = new UseCase[]
                {
                    new Transaction(connectionString),
                    new NoTransaction(connectionString)
                };

            foreach (var useCase in useCases)
            {
                Console.WriteLine(useCase.GetType().Name);
                Console.WriteLine();

                RunCase(nameof(UseCase.Right), () => useCase.Right());
                RunCase(nameof(UseCase.Wrong1), () => useCase.Wrong1());
                RunCase(nameof(UseCase.Wrong2), () => useCase.Wrong2());
                
                Console.WriteLine("----------");
                Console.WriteLine();
            }
        }

        private static void RunCase(string name, Action @case)
        {
            _databaseCleaner.Clean();

            Console.WriteLine($"{name}:");

            try
            {
                @case();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            PrintDataCount();
            Console.WriteLine();
            
            _databaseCleaner.Clean();
        }

        private static void PrintDataCount()
        {
            var count = _dataCounter.Count();

            Console.WriteLine($"Orders: {count.OrdersCount}");
            Console.WriteLine($"OrderItems: {count.OrderItemsCount}");
        }
    }
}
