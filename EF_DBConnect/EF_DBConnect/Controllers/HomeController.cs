using EF_DBConnect.Models;
using EF_DBConnect.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
