using CatalogService.DAL.Interfaces;
using MassTransit;
using RabbitMQContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.RabbitMQ.Consumers.RatingConsumers
{
    internal class AddRatingConsumer : IConsumer<IAddRatingMessage>
    {
        private readonly IProductRepository _productRepository;
        public AddRatingConsumer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task Consume(ConsumeContext<IAddRatingMessage> context)
        {
            await _productRepository.AddRatingAsync(context.Message.Id, context.Message.Rating);
        }
    }
}
