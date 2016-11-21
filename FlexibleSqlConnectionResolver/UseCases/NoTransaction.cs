using System;

namespace FlexibleSqlConnectionResolver.UseCases
{
    public class NoTransaction : UseCase
    {
        public NoTransaction(string connectionString)
            : base(connectionString)
        {
        }

        public override void RunRight()
        {
            throw new NotImplementedException();
        }

        public override void RunWrong()
        {
            throw new NotImplementedException();
        }
    }
}
