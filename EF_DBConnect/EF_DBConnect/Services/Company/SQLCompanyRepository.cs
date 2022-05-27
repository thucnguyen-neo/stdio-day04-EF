using EF_DBConnect.Models;
using EF_DBConnect.Models.Context;
using EF_DBConnect.Services.Interface;

namespace EF_DBConnect.Services
{
    public class SQLCompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext context;
        public SQLCompanyRepository(AppDbContext context)
        {
            this.context = context;
        }

        public System.Collections.Generic.IEnumerable<Company> GetList()
        {

            return context.Company;
        }
    }
}
