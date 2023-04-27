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
        public string name { get; set; }
        public string userId { get; set; }
        public string categoryId { get; set; }
        public int minutes { get; set; }
        public bool IsPomodoro { get; set; }
        public long V { get; set; }
    }
}
