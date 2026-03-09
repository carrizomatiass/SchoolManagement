using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.API.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
