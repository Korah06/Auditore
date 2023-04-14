using Auditore.Dtos.Request;
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
        private string _email = string.Empty;
        private string _password = string.Empty;
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
        #endregion

        public ICommand LoginComand => new Command(async () =>
        {
            if(_email == string.Empty || _password == string.Empty)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email/Password Son necesarios", "Aceptar");
                return;
            }
            //new LoginRequest(_email, _password);





        });

    }
}
