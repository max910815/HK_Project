using HK_Product.Data;
using HK_Product.Services;
using HK_project.Interface;
using HK_project.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HK_project.ViewModels;

namespace HK_project.Controllers
{
    public class LoginRegisterController : Controller
    {
        private readonly HKContext _ctx;
        private readonly IHashService _hashService;
        private readonly AccountServices _accountServices;

        public LoginRegisterController(HKContext ctx, AccountServices accountServices, IHashService hashService)
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
        public IActionResult Singin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Singin(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {

                Member member = await _accountServices.AuthenticateUser(lvm);

                if (member == null)
                {
                    ModelState.AddModelError(string.Empty, "帳號密碼有錯!!!");
                    return View(lvm);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, member.MemberId)  ,
                    new Claim(ClaimTypes.Email, member.MemberEmail)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)
                    );

                return RedirectToAction("Index", "Member");

            }

            return View(lvm);
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind("MemberEmail,MemberPassword")] Member member)
        {
            if (ModelState.IsValid)
            {

                var Samememberemail = await _ctx.Member.SingleOrDefaultAsync(u => u.MemberEmail == member.MemberEmail);
                var MemberWithMaxId = await _ctx.Member.OrderByDescending(u => u.MemberId).FirstOrDefaultAsync();
                //var appWithMaxId = await _ctx.Applications.OrderByDescending(u => u.ApplicationId).FirstOrDefaultAsync();
                string newMemberId;
                if (Samememberemail != null)
                {
                    ViewBag.ErrorMessage = "Signin failed: email already exists.";
                    return View(member);
                }
                else
                {
                    if (MemberWithMaxId.MemberId != null)
                    {
                        int maxId = int.Parse(MemberWithMaxId.MemberId.Substring(1));

                        int newId = maxId + 1;

                        newMemberId = "M" + newId.ToString().PadLeft(4, '0');

                    }
                    else
                    {
                        newMemberId = "M0001";
                    }

                    member.MemberPassword = _hashService.MD5Hash(member.MemberPassword);

                    Member m = new Member()
                    {
                        MemberId = newMemberId, 
                        MemberEmail = member.MemberEmail,
                        MemberName = newMemberId,
                        MemberPassword = member.MemberPassword,
                    };

                    _ctx.Add(m);
                    await _ctx.SaveChangesAsync();

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, MemberWithMaxId.MemberId)  ,
                        new Claim(ClaimTypes.Email, member.MemberEmail)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity)
                        );

                    return RedirectToAction("Index", "Member");
                }
            }
            return View();
        }

        //登出
        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
