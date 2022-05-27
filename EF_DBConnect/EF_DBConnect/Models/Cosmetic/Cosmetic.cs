namespace EF_DBConnect.Models
{
    public class Cosmetic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }

    }
}
