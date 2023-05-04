using Auditore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Dtos.Response
{
    public class GetUsersDto
    {
        public string message { get; set; }
        public List<User> users { get; set; }
    }
}
