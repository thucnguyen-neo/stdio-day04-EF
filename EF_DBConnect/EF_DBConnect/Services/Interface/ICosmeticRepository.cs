using EF_DBConnect.Models;
using System.Collections.Generic;

namespace EF_DBConnect.Services.Interface
{
    public interface ICosmeticRepository
    {
        IEnumerable<Cosmetic> GetList();
    }
}
