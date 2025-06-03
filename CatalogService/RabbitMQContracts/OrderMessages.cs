namespace RabbitMQContracts
{
    public interface IAddOrderMessage
    {
        public int Id { get; set; }
        public ulong OrderAmount { get; set; }
    }

    public interface IRemoveOrderMessage
    {
        public int Id { get; set; }
        public ulong OrderAmount { get; set; }
    }
}
