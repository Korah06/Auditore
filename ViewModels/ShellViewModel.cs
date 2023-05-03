using Auditore.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.ViewModels
{
    public class ShellViewModel : INotifyPropertyChanged
    {
        private bool _admin;
        public bool Admin
        {
            get { return _admin; }
            set
            {
                if (_admin == value) { return; }
                _admin = value;
                OnPropertyChanged(nameof(Admin));

            }
        }
        public ShellViewModel() 
        {
            GetRole();
        }

        private async void GetRole()
        {
            while (true)
            {
                Admin = Role.IsAdmin;
                await Task.Delay(100);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
