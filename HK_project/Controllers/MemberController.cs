using HK_Product.Data;
using HK_Product.Services;
using HK_project.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HK_project.Controllers
{
    public class MemberController : Controller
    {

        private readonly HKContext _ctx;
        private readonly IHashService _hashService;
        private readonly AccountServices _accountServices;

        public MemberController(HKContext ctx, AccountServices accountServices, IHashService hashService)
        {
            _ctx = ctx;
            _accountServices = accountServices;
            _hashService = hashService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Createapp()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var member = await _ctx.Member.FirstOrDefaultAsync(m => m.MemberId == userId);
            return View(member);
        }

    }
}
