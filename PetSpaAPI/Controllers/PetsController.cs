using Microsoft.AspNetCore.Mvc;

namespace PetSpaAPI.Controllers
{
    public class PetsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
