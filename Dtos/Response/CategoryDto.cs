using Auditore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Dtos.Response
{
    public class CategoryDto
    {
        public string message { get; set; }
        public List<Category> Categories { get; set; }
    }
}
