using NiceStore.Catalog.Domain.Events;
using NiceStore.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Catalog.Domain
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatrHandler _mediatrHandlerBus;
        public StockService(IProductRepository productRepository, IMediatrHandler mediatrHandlerBus)
        {
            _productRepository = productRepository;
            _mediatrHandlerBus = mediatrHandlerBus;
        }
        public async Task<bool> DebitStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);
            if (product == null) return false;
            if(!product.HasStock(quantity)) return false;   
            product.SubtractStock(quantity);

            if(product.StockQuantity < 10)
            {
                await _mediatrHandlerBus.PublishEvent(new ProductBellowStockEvent(product.Id, product.StockQuantity));
            }


            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

      
        public async Task<bool> ReplenishStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);
            if (product == null) return false;
            product.AddStock(quantity);
            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }

    }
}
