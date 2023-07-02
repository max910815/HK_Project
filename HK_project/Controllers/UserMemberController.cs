using Microsoft.AspNetCore.Mvc;

namespace HK_project.Controllers
{
    public class UserMemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
