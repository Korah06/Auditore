using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Dtos.Request
{
    public class CreateChronoRequest
    {
        public string name { get; set; }
        public int minutes { get; set; }
        public string categoryId { get; set; }
        public bool IsPomodoro { get; set; }
    }
}
