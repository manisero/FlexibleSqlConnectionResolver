using System;
using FlexibleSqlConnectionResolver.ConnectionResolution;

namespace FlexibleSqlConnectionResolver.UseCases
{
    public class NoTransaction : UseCase
    {
        public NoTransaction(string connectionString)
            : base(connectionString)
        {
        }

        public override void Right()
        {
            // Create connection per operation within task

            using (var connectionResolver = new PerResolveSqlConnectionResolver(_connectionString))
            {
                ExecuteLongRunningTask(connectionResolver, "Task1");
                ExecuteLongRunningTask(connectionResolver, "Task2");
            }
        }

        public override void Wrong1()
        {
            // Create single long-living connection per task

            using (var connectionResolver = new SingletonSqlConnectionResolver(_connectionString))
            {
                ExecuteLongRunningTask(connectionResolver, "Task1");
            }

            using (var connectionResolver = new SingletonSqlConnectionResolver(_connectionString))
            {
                ExecuteLongRunningTask(connectionResolver, "Task2");
            }
        }

        public override void Wrong2()
        {
            // Create single too-long-living connection

            using (var connectionResolver = new SingletonSqlConnectionResolver(_connectionString))
            {
                ExecuteLongRunningTask(connectionResolver, "Task1");
                ExecuteLongRunningTask(connectionResolver, "Task2");
            }
        }

        private void ExecuteLongRunningTask(ISqlConnectionResolver connectionResolver, string taskName)
        {
            throw new NotImplementedException();
        }
    }
}
