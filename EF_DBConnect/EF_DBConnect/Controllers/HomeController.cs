using EF_DBConnect.Models;
using EF_DBConnect.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DBConnect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment webHostEnvironment;
        private readonly ICosmeticRepository cosmeticRepository;
        private readonly ICompanyRepository companyRepository;

        public HomeController(ICosmeticRepository cosmeticRepository, IWebHostEnvironment webHostEnvironment, ICompanyRepository companyRepository)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.cosmeticRepository = cosmeticRepository;
            this.companyRepository = companyRepository;

        }
        /*public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        public IActionResult Index()
        {
            ViewBag.Cosmetics = cosmeticRepository.GetList();
            ViewBag.Companies = companyRepository.GetList();
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Companies = companyRepository.GetList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CosmeticCreateRequest request)

        {
            ViewBag.Companies = companyRepository.GetList();
            if (ModelState.IsValid)
            {
                var newCosmetic = new Cosmetic()
                {
                    Name = request.Name,
                    Price = (int)request.Price,
                    Description = request.Description,
                    CompanyId = request.CompanyId,

                };
                //Đoạn add ảnh vào thư mục wwwroot/images
                var fileName = string.Empty;
                if (request.IFFImage != null)
                {
                    string uploadImg = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    fileName = $"{Guid.NewGuid()}_{request.IFFImage.FileName}";
                    var filePath = Path.Combine(uploadImg, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        request.IFFImage.CopyTo(fs);
                    }
                }
                newCosmetic.Image = fileName;
                var result = cosmeticRepository.Create(newCosmetic);
                return RedirectToAction("Index");

            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = cosmeticRepository.GetById(id);
            if (item != null)
            {
                ViewBag.Companies = companyRepository.GetList();
                var editRequest = new CosmeticEditRequest()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description,
                    Image = item.Image,
                    CompanyId = item.CompanyId,
                };
                return View(editRequest);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(CosmeticEditRequest request)
        {
            if (ModelState.IsValid)
            {
                var editCosmetic = new Cosmetic()
                {
                    Id = request.Id,
                    Name = request.Name,
                    Price = (int)request.Price,
                    Description = request.Description,
                    CompanyId = request.CompanyId,

                };
                var fileName = string.Empty;
                if (request.IFFImage != null)
                {
                    string uploadImg = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    fileName = $"{Guid.NewGuid()}_{request.IFFImage.FileName}";
                    var filePath = Path.Combine(uploadImg, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        request.IFFImage.CopyTo(fs);
                    }
                    editCosmetic.Image = fileName;
                    if (!string.IsNullOrEmpty(editCosmetic.Image))
                    {
                        string delFile = Path.Combine(webHostEnvironment.WebRootPath, "images", editCosmetic.Image);
                        System.IO.File.Delete(delFile);
                    }
                }
                else
                {
                    editCosmetic.Image = request.Image;
                }

                if (cosmeticRepository.Edit(editCosmetic) != null)
                {
                    return RedirectToAction("Index"); ;
                }
            }

            return View();
        }
        public IActionResult Delete(int id)
        {
            ViewBag.Cosmetics = cosmeticRepository.GetList();
            ViewBag.Companies = companyRepository.GetList();
            cosmeticRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
