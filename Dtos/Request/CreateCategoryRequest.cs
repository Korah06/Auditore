using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Dtos.Request
{
    public class CreateCategoryRequest
    {
        public string name { get; set; }
        public string color { get; set; }
    }
}
