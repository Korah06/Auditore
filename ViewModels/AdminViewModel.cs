using Auditore.Models;
using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
            Users.Clear();
            _users = await _userService.GetUsers(Preferences.Default.Get("token",""));
            foreach(var user in _users)
            {
                Users.Add(user);
            }
        }

        public ICommand DeleteUser => new Command(async (obj) =>
        {
            bool answer = await Application.Current.MainPage
                        .DisplayAlert("Eliminar", "Te gustaria eliminar el usuario?", "Si", "No");
            if (answer)
            {
                User user = obj as User;
                bool deleted = await _userService.DeleteUser(user._id, Preferences.Default.Get("token", ""));
                if (deleted) { GetData(); }
            }
        });

        public ICommand UpdateUser => new Command(async (obj) =>
        {
            User user = obj as User;
            await _userService.DeleteUser(user._id, Preferences.Default.Get("token", ""));
        });
    }
}
