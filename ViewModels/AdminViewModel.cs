using Auditore.Models;
using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.ViewModels
{
    public class AdminViewModel
    {
        private List<User> _users;
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        private readonly IUserService _userService;
        public AdminViewModel(IUserService userService) 
        {
            _userService = userService;
            GetData();
        }

        public async void GetData()
        {
            _users = await _userService.GetUsers(Preferences.Default.Get("token",""));
            foreach(var user in _users)
            {
                Users.Add(user);
            }
        }

    }
}
