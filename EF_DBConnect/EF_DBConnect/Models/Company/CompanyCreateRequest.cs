using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EF_DBConnect.Models
{
    public class CompanyCreateRequest
    {
        [Display(Name = "Tên doanh nghiệp")]
        [Required(ErrorMessage = "Tên doanh nghiệp không được để trống")]
        public string Name { get; set; }
    }
}
