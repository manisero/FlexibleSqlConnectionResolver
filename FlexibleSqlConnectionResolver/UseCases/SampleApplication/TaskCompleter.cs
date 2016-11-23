using System.Data;
using Dapper;
using FlexibleSqlConnectionResolver.ConnectionResolution;

namespace FlexibleSqlConnectionResolver.UseCases.SampleApplication
{
    public class TaskCompleterInput
    {
        public int TaskId { get; set; }
    }

    public class TaskCompleter
    {
        private readonly ISqlConnectionResolver _sqlConnectionResolver;

        public TaskCompleter(ISqlConnectionResolver sqlConnectionResolver)
        {
            _sqlConnectionResolver = sqlConnectionResolver;
        }

        public void Complete(TaskCompleterInput input)
        {
            const string sql =
@"UPDATE [dbo].[Task]
SET [IsComplete] = 1
WHERE [TaskId] = @TaskId;";

            using (var cw = _sqlConnectionResolver.Resolve())
            {
                cw.Connection.Execute(sql, input, commandType: CommandType.Text);
            }
        }
    }
}
