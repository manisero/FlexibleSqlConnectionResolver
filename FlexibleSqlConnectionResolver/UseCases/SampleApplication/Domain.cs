namespace FlexibleSqlConnectionResolver.UseCases.SampleApplication
{
    public class Order
    {
        public int OrderId { get; set; }

        public string Name { get; set; }
    }

    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public string Name { get; set; }
    }

    public class Task
    {
        public int TaskId { get; set; }

        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}
