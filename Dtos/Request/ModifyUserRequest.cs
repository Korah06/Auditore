using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Dtos.Request
{
    public class ModifyUserRequest
    {
        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string rol { get; set; }
        public string avatar { get; set; }
        public string banner { get; set; }
    }
}
