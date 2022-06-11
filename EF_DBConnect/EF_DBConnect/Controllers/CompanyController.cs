using EF_DBConnect.Models;
using EF_DBConnect.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EF_DBConnect.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ILogger<CompanyController> _logger;
        private IWebHostEnvironment webHostEnvironment;
        private readonly ICosmeticRepository cosmeticRepository;
        private readonly ICompanyRepository companyRepository;
        public CompanyController(ICosmeticRepository cosmeticRepository, IWebHostEnvironment webHostEnvironment, ICompanyRepository companyRepository)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.cosmeticRepository = cosmeticRepository;
            this.companyRepository = companyRepository;

        }
        public IActionResult Index()
        {
            ViewBag.Companies = companyRepository.GetList();
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CompanyCreateRequest request)
        {
            ViewBag.Companies = companyRepository.GetList();
            if (ModelState.IsValid)
            {
                var newCompany = new Company()
                {
                    Name = request.Name,
                };
                
                var result = companyRepository.Create(newCompany);
                return RedirectToAction("Index");

            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = companyRepository.GetById(id);
            if (item != null)
            {
                var editRequest = new CompanyEditRequest()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                return View(editRequest);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(CompanyEditRequest request)
        {
            if (ModelState.IsValid)
            {
                var editCompany = new Company()
                {
                    Id = request.Id,
                    Name = request.Name,
                };

                if (companyRepository.Edit(editCompany) != null)
                {
                    return RedirectToAction("Index"); ;
                }
            }

            return View();
        }
        public IActionResult Delete(int id)
        {
            ViewBag.Companies = companyRepository.GetList();
            companyRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
