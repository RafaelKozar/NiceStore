using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<ProductBellowStockEvent>
    {
        private readonly IProductRepository _productRepository;

        public ProductEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(ProductBellowStockEvent notification, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(notification.AggregateId);

        }
    }
}
