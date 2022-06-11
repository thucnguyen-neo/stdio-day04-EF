using EF_DBConnect.Models;
using EF_DBConnect.Models.Context;
using EF_DBConnect.Services.Interface;
using Microsoft.EntityFrameworkCore;

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
        public Company Create(Company c)
        {
            context.Company.Add(c);
            context.SaveChanges();
            return c;
        }
        public Company Edit(Company c)
        {
            var editCompany = context.Company.Attach(c);
            editCompany.State = EntityState.Modified;
            context.SaveChanges();
            return c;
        }
        public Company GetById(int id)
        {
            return context.Company.Find(id);
        }
        public bool Delete(int id)
        {
            var item = context.Company.Find(id);
            if (item != null)
            {
                context.Company.Remove(item);
                return context.SaveChanges() > 0;
            }
            return false;
        }
    }
}
