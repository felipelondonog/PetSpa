using Microsoft.AspNetCore.Mvc;

namespace PetSpaAPI.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
