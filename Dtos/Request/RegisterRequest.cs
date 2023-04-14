using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Dtos.Request
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }

        public RegisterRequest(string username,string name, string surname, string email, string password, string rol) 
        {
            Username = username;
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            Rol = rol;
        }
    }
}
