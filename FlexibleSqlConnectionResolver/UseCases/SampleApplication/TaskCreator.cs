using System.Linq;
using Dapper;
using FlexibleSqlConnectionResolver.ConnectionResolution;

namespace FlexibleSqlConnectionResolver.UseCases.SampleApplication
{
    public class TaskCreatorInput
    {
        public string Name { get; set; }
    }

    public class TaskCreator
    {
        private readonly ISqlConnectionResolver _sqlConnectionResolver;

        public TaskCreator(ISqlConnectionResolver sqlConnectionResolver)
        {
            _sqlConnectionResolver = sqlConnectionResolver;
        }

        public Task Create(TaskCreatorInput input)
        {
            const string sql =
@"INSERT INTO [dbo].[Task] ([Name])
OUTPUT inserted.*
VALUES (@Name);";

            using (var cw = _sqlConnectionResolver.Resolve())
            {
                return cw.Connection.Query<Task>(sql, input).Single();
            }
        }
    }
}
