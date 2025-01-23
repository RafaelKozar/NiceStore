using AutoMapper;
using NiceStore.Catalog.Application.DTOs;
using NiceStore.Catalog.Domain;
using NiceStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Catalog.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IStockService _stockService;

        public ProductAppService(IProductRepository productRepository, IMapper mapper, IStockService stockService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _stockService = stockService;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            return _mapper.Map<IEnumerable<CategoryDTO>>(await _productRepository.GetCategories()); 
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(await _productRepository.GetAll());
        }

        public async Task<ProductDTO> GetProductById(Guid id)
        {
            return _mapper.Map<ProductDTO>(await _productRepository.GetById(id));
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategory(int code)
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(await _productRepository.GetByCategory(code));
        }
        public async Task AddProduct(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            _productRepository.Add(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public Task UpdateProduct(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            _productRepository.Update(product);

            return _productRepository.UnitOfWork.Commit();

        }

        public async Task<ProductDTO> DebitStock(Guid id, int quantity)
        {
            if(!_stockService.DebitStock(id, quantity).Result)
            {
                throw new DomainException("Fail to debit stock");
            }

            return _mapper.Map<ProductDTO>(await _productRepository.GetById(id));
        }

        public async Task<ProductDTO> ReplenishStock(Guid id, int quantity)
        {
            if(!_stockService.ReplenishStock(id, quantity).Result)
            {
                throw new DomainException("Fail to replenish stock");
            }

            return _mapper.Map<ProductDTO>(await _productRepository.GetById(id));
        }

       

        public void Dispose()
        {
            _productRepository?.Dispose();
            _stockService?.Dispose();
        }
    }
}
