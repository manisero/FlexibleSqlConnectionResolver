using System;
using FlexibleSqlConnectionResolver.ConnectionResolution;
using FlexibleSqlConnectionResolver.UseCases.SampleApplication;

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
                ExecuteLongRunningTask(connectionResolver, "Task1", false);
                ExecuteLongRunningTask(connectionResolver, "Task2", true);
            }
        }

        public override void Wrong1()
        {
            // Create single long-living connection per task

            using (var connectionResolver = new SingletonSqlConnectionResolver(_connectionString))
            {
                ExecuteLongRunningTask(connectionResolver, "Task1", false);
            }

            using (var connectionResolver = new SingletonSqlConnectionResolver(_connectionString))
            {
                ExecuteLongRunningTask(connectionResolver, "Task2", true);
            }
        }

        public override void Wrong2()
        {
            // Create single too-long-living connection

            using (var connectionResolver = new SingletonSqlConnectionResolver(_connectionString))
            {
                ExecuteLongRunningTask(connectionResolver, "Task1", false);
                ExecuteLongRunningTask(connectionResolver, "Task2", true);
            }
        }

        private void ExecuteLongRunningTask(ISqlConnectionResolver connectionResolver, string taskName, bool shouldFail)
        {
            // We don't use transaction - if task fails, it remains in the database, marked as incomplete

            var taskCreator = new TaskCreator(connectionResolver);
            var taskCompleter = new TaskCompleter(connectionResolver);

            var task = taskCreator.Create(new TaskCreatorInput
                                              {
                                                  Name = taskName
                                              });

            // Perfom long-running operations...

            if (shouldFail)
            {
                throw new InvalidOperationException("Task failed.");
            }

            taskCompleter.Complete(new TaskCompleterInput
                                       {
                                           TaskId = task.TaskId
                                       });
        }
    }
}
