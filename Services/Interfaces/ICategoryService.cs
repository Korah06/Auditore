using Auditore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories(string token);
    }
}
