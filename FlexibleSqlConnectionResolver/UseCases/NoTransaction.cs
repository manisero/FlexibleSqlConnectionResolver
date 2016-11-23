using System;

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
            throw new NotImplementedException();
        }

        public override void Wrong1()
        {
            throw new NotImplementedException();
        }

        public override void Wrong2()
        {
            throw new NotImplementedException();
        }
    }
}
