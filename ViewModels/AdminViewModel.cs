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

        private User _itemSelected;
        public User ItemSelected
        {
            get { return _itemSelected; }
            set { _itemSelected = value; }
        }

        public ObservableCollection<string> Roles { get; set; } = new ObservableCollection<string>();

        private string _selectedRol;
        public string SelectedRol
        {
            get { return _selectedRol; }
            set { _selectedRol = value; }
        }

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
            Roles.Clear();
            Roles.Add("admin");
            Roles.Add("basic");
            Users.Clear();
            _users = await _userService.GetUsers(Preferences.Default.Get("token",""));
            foreach(var user in _users)
            {
                Users.Add(user);
            }
        }

        public ICommand SelectCommand => new Command<object>((obj) =>
        {
            if(obj != null)
            {
                _itemSelected = obj as User;
                _selectedRol = _itemSelected.rol;
            }
            
        });
        public bool Deleting = true;
        public ICommand DeleteUser => new Command(async () =>
        {
            bool answer = await Application.Current.MainPage
                        .DisplayAlert("Eliminar", "Te gustaria eliminar el usuario?", "Si", "No");
            if (answer)
            {
                bool deleted = await _userService.DeleteUser(_itemSelected._id, Preferences.Default.Get("token", ""));

#if ANDROID || IOS
#else
                if (deleted) 
                { 
                    _itemSelected = null; 
                    GetData(); 
                }
#endif

            }
            Deleting = false;
            await Task.Delay(100);
            Deleting = true;
        });

        public ICommand UpdateUser => new Command(async () =>
        {
            _itemSelected.rol = _selectedRol;
            await _userService.ModifyUser(_itemSelected, Preferences.Default.Get("token", ""));
        });
    }
}
