using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.ViewModels
{
    public class AdminViewModel
    {
        private readonly IUserService _userService;
        public AdminViewModel(IUserService userService) 
        {
            _userService = userService;
        }

    }
}
