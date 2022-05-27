using EF_DBConnect.Models;
using EF_DBConnect.Models.Context;
using EF_DBConnect.Services.Interface;

namespace EF_DBConnect.Services
{
    public class SQLCosmeticRepository : ICosmeticRepository
    {
        private readonly AppDbContext context;
        public SQLCosmeticRepository(AppDbContext context)
        {
            this.context = context;
        }

        public System.Collections.Generic.IEnumerable<Cosmetic> GetList()
        {

            return context.Cosmetic;
        }
    }
}
