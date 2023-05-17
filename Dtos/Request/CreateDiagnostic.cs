using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Dtos.Request
{
    public class CreateDiagnostic
    {
        public string _id { get; set; }
        public string name { get; set; }
        public int workMinutes { get; set; }
        public int restMinutes { get; set; }
        public int repeats { get; set; }
        public string idCategory { get; set; }
        public string idUser { get; set; }
        public string[] tasksId { get; set; }
    }
}
