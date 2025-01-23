using NiceStore.Catalog.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Catalog.Application.Services
{
    public interface IProductAppService : IDisposable
    {
        Task AddProduct(ProductDTO productDTO);
        Task UpdateProduct(ProductDTO productDTO);
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<ProductDTO> GetProductById(Guid id);
        Task<IEnumerable<ProductDTO>> GetProductsByCategory(int code);
        Task<IEnumerable<CategoryDTO>> GetAllCategories();
        Task<ProductDTO> DebitStock(Guid id, int quantity); 
        Task<ProductDTO> ReplenishStock(Guid id, int quantity); 
        

    }
}
