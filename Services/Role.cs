using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Services
{
    public static class Role
    {
        public static bool IsAdmin = false;
        public static async void GetRole(IUserService userService)
        {
            IsAdmin = await userService.GetRole(Preferences.Default.Get("token", ""));
        }
    }
}
