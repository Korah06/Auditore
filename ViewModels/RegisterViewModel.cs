﻿using Auditore.Dtos.Request;
using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Auditore.ViewModels
{
    public class RegisterViewModel
    {
        #region Variables
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _Cpassword = string.Empty;
        private string _username = string.Empty;
        private string _name = string.Empty;
        private string _surname = string.Empty;
        private string rol = "basic";
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string surname
        {
            get { return _surname; }
            set { _surname = value; }
        }
        public string username
        {
            get { return _username; }
            set { _username = value; }
        }
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }
        public string Cpassword
        {
            get { return _Cpassword; }
            set { _Cpassword = value; }
        }
        private readonly IUserService _userService;
        #endregion

        public RegisterViewModel(IUserService userService)
        {
            _userService = userService;
        }
        public ICommand RegisterCommand => new Command(async () => 
        {
            if (
            _email == string.Empty || 
            _name == string.Empty || 
            _surname == string.Empty || 
            _username == string.Empty ||
            _password == string.Empty ||
            _Cpassword == string.Empty
            )
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe rellenar todos los campos", "Aceptar");
                return;
            }

            if(_password != _Cpassword) 
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Las contraseñas escritas no son iguales", "Aceptar");
                return;
            }
            string pPattern = @"^(?=.*[A-Z])(?=.*\d).{4,}$";
            bool isValidPassword = Regex.IsMatch(_password, pPattern);
            if (!isValidPassword)
            {
                await Application.Current.MainPage.DisplayAlert
                ("Formato contraseña", "El formato de la contraseña debe de tener al menos una mayuscula, un número y minimo 4 caracteres", "Aceptar");
                return;
            }
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";


            bool isValidEmail = Regex.IsMatch(_email, pattern);

            if (isValidEmail)
            {
                RegisterRequest registerRequest = new RegisterRequest(_username, _name, _surname, _email, _password, rol);
                bool registered = await _userService.Register(registerRequest);
                if (registered)
                {
                    await Task.Run(() =>
                    {
                        Cpassword = string.Empty;
                        password = string.Empty;
                        email = string.Empty;
                        username = string.Empty;
                        name = string.Empty;
                        surname = string.Empty;
                    });

                    await Application.Current.MainPage.DisplayAlert("Registro completado", "Se ha registrado correctamente", "Aceptar");
                    await Shell.Current.GoToAsync("//Login");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert
                ("Formato correo", "El formato del correo no es el adecuado", "Aceptar");
            }
            

        });    
    }
}
