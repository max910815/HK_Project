using HK_Product.Data;
using HK_Product.Services;
using HK_project.Interface;
using HK_project.Models;
using HK_project.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly ILogger<MemberController> _logger;

        public MemberController(HKContext ctx, AccountServices accountServices, IHashService hashService, ILogger<MemberController> logger)
        {
            _ctx = ctx;
            _accountServices = accountServices;
            _hashService = hashService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Createapp()
        {
            var memberId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var member = await _ctx.Member.FirstOrDefaultAsync(m => m.MemberId == memberId);

            ViewBag.Member = member;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Createapp(Application a)
        {
            if (ModelState.IsValid)
            {
                var AppWithMaxId = await _ctx.Applications.OrderByDescending(u => u.ApplicationId).FirstOrDefaultAsync();
                string newAppId;
                if (AppWithMaxId != null)
                {
                    int maxId = int.Parse(AppWithMaxId.ApplicationId.Substring(1));

                    int newId = maxId + 1;

                    newAppId = "A" + newId.ToString().PadLeft(4, '0');

                }
                else
                {
                    newAppId = "A0001";
                }
                Application app = new Application
                {
                    ApplicationId = newAppId,
                    Model = "gpt-35-turbo",
                    Parameter = a.Parameter,
                    MemberId = a.MemberId
                };

                _ctx.Add(app); // Use Add() here instead of Update()
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Uploadfileapp", "Member");
            }
            return View();
        }



        public IActionResult Chooseapp( )
        {
            var memberid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var app = _ctx.Applications.Where(a => a.MemberId == memberid).ToList();
            ViewBag.App = app;
            return View();
        }



        [HttpGet]
        public IActionResult Uploadfileapp( )
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Uploadfileapp(List<IFormFile> files)
        {
            string path = "Upload";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            try
            {
                
                // 獲取當前已驗證使用者的名稱
                var memberId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; 

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string filePath = Path.Combine(path, fileName);
                        string fileType = file.ContentType;

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        // 取得完整檔案路徑
                        string fullPath = Path.GetFullPath(filePath);
                        _logger.LogInformation("Upload file success, path: {FilePath}", fullPath);

                        var fileWithMaxId = await _ctx.AIFiles.OrderByDescending(u => u.AifileId).FirstOrDefaultAsync();
                        string id;

                        if (fileWithMaxId.AifileId != null)
                        {
                            // 從 UserID 中提取出數字部分，並轉換成整數
                            int maxId = int.Parse(fileWithMaxId.AifileId.Substring(1));
                            // 新的 UserID 是最大 UserID 加一
                            int newId = maxId + 1;
                            // 將新的 UserID 轉換回字符串形式，並確保它始終有四位數字
                            string newUserId = "D" + newId.ToString().PadLeft(4, '0');
                            id = newUserId;
                        }
                        else
                        {
                            // 如果沒有任何 User，則新的 UserID 可以是 "U0001"
                            string newUserId = "D0001";
                            id = newUserId;
                        }

                        Aifile embs = new Aifile()
                        {
                            AifileId = id,
                            AifileType = fileType,
                            AifilePath = filePath,
                            ApplicationId = memberId
                        };

                        _ctx.Add(embs);
                        await _ctx.SaveChangesAsync();
                    }
                }

                TempData["UploadSuccess"] = true;
                return RedirectToAction("Index", "Member");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Upload Error");
                return BadRequest();
            }
        }





        public IActionResult qa()
        {
           return  View();
        }

        public IActionResult a()
        {
            return View();
        }


    }
}
