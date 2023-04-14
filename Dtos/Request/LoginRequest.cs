using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Dtos.Request
{
    public class LoginRequest
    {
        public string email {  get; set; }
        public string password { get; set; }

        public LoginRequest(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
