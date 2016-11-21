namespace FlexibleSqlConnectionResolver.UseCases
{
    public abstract class UseCase
    {
        protected readonly string _connectionString;

        protected UseCase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public abstract void RunRight();

        public abstract void RunWrong();
    }
}
