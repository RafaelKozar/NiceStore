using Microsoft.AspNetCore.Mvc;
using NiceStore.Catalog.Application.DTOs;
using NiceStore.Catalog.Application.Services;

namespace NiceStore.WebApp.MVC.Controllers.Admin
{
    public class AdminProductController : Controller
    {
        private readonly IProductAppService _productAppService;
        public AdminProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("admin-product")]
        public async Task<IActionResult> Index()
        {
            var products = await _productAppService.GetAllProducts();
            return View(products);
        }

        [Route("new-product")]  
        public async Task<IActionResult> NewProduct()
        {
            return View(await PopulateCategories(new ProductDTO()));
        }

        [Route("new-product")]
        [HttpPost]
        public async Task<IActionResult> NewProduct(ProductDTO productDTO)
        {
            if (!ModelState.IsValid) return View(productDTO);

            await _productAppService.AddProduct(productDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("editct-produto")]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            return View(await PopulateCategories(await _productAppService.GetProductById(id)));
        }

        [HttpPost]
        [Route("editct-produto")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductDTO productDTO)
        {
           var product = await _productAppService.GetProductById(id);
            productDTO.StockQuantity = product.StockQuantity;

            ModelState.Remove("StockQuantity"); 
            if (!ModelState.IsValid) return View(await PopulateCategories(productDTO));

            await _productAppService.UpdateProduct(productDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("product-update-stock")]
        public async Task<IActionResult> UpdateStock(Guid id)
        {
            return View("Stock", await _productAppService.GetProductById(id));
        }

        [HttpPost]
        [Route("product-update-stock")]
        public async Task<IActionResult> UpdateStock(Guid id, int quantity)
        {
            if(quantity > 0)
            {
                await _productAppService.ReplenishStock(id, quantity);
            }
            else
            {
                await _productAppService.DebitStock(id, quantity);
            }

            return RedirectToAction("Index", await _productAppService.GetAllProducts());
        }

        private async Task<ProductDTO> PopulateCategories(ProductDTO productDTO)
        {
            var categories = await _productAppService.GetAllCategories();
            productDTO.Categories = categories ?? new List<CategoryDTO>(); // Ensure categories is not null
            return productDTO;
        }

    }
}
