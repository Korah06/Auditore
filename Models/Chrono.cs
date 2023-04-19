using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Models
{
    public class Chrono
    {
        public string _id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string CategoryId { get; set; }
        public int Minutes { get; set; }
        public long V { get; set; }
    }
}
