using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Dtos.Request
{
    public class CreateTaskRequest
    {
        public string name { get; set; }
        public string description { get; set; }
        public string categoryId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
