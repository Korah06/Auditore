﻿using Auditore.Models;
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
            _itemSelected = obj as User;
            _selectedRol = _itemSelected.rol;
        });
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

        public ICommand UpdateUser => new Command(async () =>
        {
            _itemSelected.rol = _selectedRol;
            await _userService.ModifyUser(_itemSelected, Preferences.Default.Get("token", ""));
        });
    }
}
