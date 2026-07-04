using Microsoft.AspNetCore.Mvc;

namespace BabyCare.Api.Controllers
{
    public class ToDosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
