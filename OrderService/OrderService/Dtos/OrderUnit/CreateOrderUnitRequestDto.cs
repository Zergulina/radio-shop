namespace OrderService.Dtos.OrderUnit
{
    public class CreateOrderUnitRequestDto
    {
        public int ProductId { get; set; }
        public ulong Amount { get; set; }
    }
}
