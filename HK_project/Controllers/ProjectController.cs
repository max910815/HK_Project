using Microsoft.AspNetCore.Mvc;

namespace HK_project.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
