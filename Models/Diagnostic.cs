using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Models
{
    public class Diagnostic
    {
        public int Id { get; set; }
        public int workMinutes { get; set; }
        public int restMinutes { get; set; }
        public int repeats { get; set; }
        public string idCategory { get; set; }
        public string idUser { get; set; }
        public List<string> tasksIds { get; set; }
    }
}
