using EF_DBConnect.Models;
using EF_DBConnect.Models.Context;
using EF_DBConnect.Services.Interface;
using Microsoft.EntityFrameworkCore;

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
        public Cosmetic Create(Cosmetic c)
        {
            context.Cosmetic.Add(c);
            context.SaveChanges();
            return c;
        }
        public Cosmetic Edit(Cosmetic c)
        {
            var editCosmetic = context.Cosmetic.Attach(c);
            editCosmetic.State = EntityState.Modified;
            context.SaveChanges();
            return c;
        }
        public Cosmetic GetById(int id)
        {
            return context.Cosmetic.Find(id);
        }
        public bool Delete(int id)
        {
            var item = context.Cosmetic.Find(id);
            if (item != null)
            {
                context.Cosmetic.Remove(item);
                return context.SaveChanges() > 0;
            }
            return false;
        }
    }
}
