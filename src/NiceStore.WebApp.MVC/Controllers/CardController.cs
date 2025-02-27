using Microsoft.AspNetCore.Mvc;
using NiceStore.Catalog.Application.Services;
using NiceStore.Core.Bus;
using NiceStore.Payments.Application.Commands;

namespace NiceStore.WebApp.MVC.Controllers
{
    public class CardController : ControllerBase
    {
        private readonly IProductAppService _productAppService;
        private readonly IMediatrHandler _mediatrHandler;

        public CardController(IProductAppService productAppService, IMediatrHandler mediatrHandler)
        {
            _productAppService = productAppService;
            _mediatrHandler = mediatrHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("my-cart")]
        public async Task<IActionResult> AddItem(Guid productId, int quantity)
        {
            var product = await _productAppService.GetProductById(productId);
            if (product == null)
            {
                return BadRequest();
            }

            if (product.StockQuantity < quantity)
            {
                TempData["Error"] = "Product out of stock";
                return RedirectToAction("Product", "Catalog", new { id = productId });
            }

            var command = new AddItemOrderCommand(ClientId, product.Id, product.Name, quantity, product.Price);
            await _mediatrHandler.SendCommand(command);

            TempData["Error"] = "Product Unavailable";    
            return RedirectToAction("ProductDetails", "Display", new { id = productId });
        }
    }
}
