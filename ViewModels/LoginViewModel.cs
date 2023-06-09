﻿using Auditore.Dtos.Request;
using Auditore.Services;
using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Auditore.ViewModels
{
    public class LoginViewModel
    {
        #region Variables
        //string.Empty
        private string _email = "";
        private string _password = "";
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }
        private readonly IUserService _userService;
        #endregion

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
        }

        public ICommand LoginCommand => new Command(async () =>
        {
            if(_email == string.Empty || _password == string.Empty)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email/Password Son necesarios", "Aceptar");
                return;
            }

            string token = await _userService.Login(new LoginRequest(_email, _password));

            if(token != null)
            {
                Preferences.Default.Set("token", token);
                Role.GetRole(_userService);
                await Task.Delay(5);
                await Shell.Current.GoToAsync("//Tasks");
            }


        });

    }
}
