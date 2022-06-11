using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EF_DBConnect.Models
{
    public class CosmeticCreateRequest
    {
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string Name { get; set; }
        [Display(Name = "Giá sản phẩm")]
        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        public int? Price { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        public string Image { get; set; }
        public IFormFile IFFImage { get; set; }
        public int CompanyId { get; set; }

    }
}
