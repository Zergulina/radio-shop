namespace RabbitMQContracts
{
    public interface IAddRatingMessage
    {
        public int Id { get; set; }
        public byte Rating { get; set; }
    }
    public interface IRemoveRatingMessage
    {
        public int Id { get; set; }
        public byte Rating { get; set; }
    }
}
