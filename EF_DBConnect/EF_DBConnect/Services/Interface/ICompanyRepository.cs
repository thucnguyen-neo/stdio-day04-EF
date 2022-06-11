using EF_DBConnect.Models;
using System.Collections.Generic;

namespace EF_DBConnect.Services.Interface
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetList();
        Company Create(Company company);
        Company Edit(Company company);
        Company GetById(int id);
        bool Delete(int id);
    }
}
