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
    internal class RemoveOrderConsumer : IConsumer<IRemoveOrderMessage>
    {
        private readonly IProductRepository _productRepository;
        public RemoveOrderConsumer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<IRemoveOrderMessage> context)
        {
            Console.WriteLine("remove");
            await _productRepository.RemoveOrderAmountAsync(context.Message.Id, context.Message.OrderAmount);
        }
    }
}
