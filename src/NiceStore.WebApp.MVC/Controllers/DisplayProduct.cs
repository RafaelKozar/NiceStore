using Microsoft.AspNetCore.Mvc;
using NiceStore.Catalog.Application.Services;

namespace NiceStore.WebApp.MVC.Controllers
{
    public class DisplayProduct : Controller
    {
        private readonly IProductAppService _productAppService;

        public DisplayProduct(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("")]
        [Route("display")]
        public async Task<IActionResult> Index()
        {
            var products = await _productAppService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        [Route("product-details/{id}")] 
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            var product = await _productAppService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
