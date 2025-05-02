using System;
using CatalogService.BLL.Protos;
using CatalogService.DAL.Interfaces;
using MassTransit;

namespace CatalogService.BLL.Consumers;

internal class OrderValidationRequestConsumer : IConsumer<OrderValidationRequest>
{
    private readonly IProductRepository _productRepository;

    public OrderValidationRequestConsumer(IProductRepository productRepository) {
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<OrderValidationRequest> context) {
        var invalidIds = new List<int>();

        foreach (var productId in context.Message.ProductIds)
        {
            var exists = await _productRepository.ExistsAsync(productId);
            if (!exists) invalidIds.Add(productId);
        }

        await context.RespondAsync(new OrderValidationResponse
        {
            OrderId = context.Message.OrderId,
            IsValid = invalidIds.Count == 0,
            InvalidProductIds = invalidIds
        });
    }
}
