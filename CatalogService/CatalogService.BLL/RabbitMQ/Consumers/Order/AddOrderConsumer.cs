using CatalogService.DAL.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQContracts;

namespace CatalogService.BLL.RabbitMQ.Consumers.Order
{
    internal class AddOrderConsumer : IConsumer<IAddOrderMessage>
    {
        private readonly IProductRepository _productRepository;
        public AddOrderConsumer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task Consume(ConsumeContext<IAddOrderMessage> context)
        {
            Console.WriteLine($"Id = {context.Message.Id} Amount = {context.Message.OrderAmount}");
            await _productRepository.AddOrderAmountAsync(context.Message.Id, context.Message.OrderAmount);
        }
    }
}
