using Auditore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> Login(LoginRequest user);
        Task<bool> Register(RegisterRequest user);
    }
}
