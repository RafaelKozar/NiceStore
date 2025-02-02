using MediatR;
using NiceStore.Core.EmailService;
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
        private readonly IEmailService _emailService;

        public ProductEventHandler(IProductRepository productRepository, IEmailService emailService = null)
        {
            _productRepository = productRepository;
            _emailService = emailService;
        }

        public async Task Handle(ProductBellowStockEvent notification, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(notification.AggregateId);

            // send email to purchase department
            _emailService?.SendEmail("", "", "Stock bellow", "");

        }
    }
}
