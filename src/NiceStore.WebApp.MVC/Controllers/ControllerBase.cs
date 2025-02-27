using Microsoft.AspNetCore.Mvc;

namespace NiceStore.WebApp.MVC.Controllers
{
    public abstract class ControllerBase : Controller
    {
        public Guid ClientId = Guid.Parse("d3b07384-d9a7-4f3b-8f1b-5e2b5b5e5e5e");
    }
}
