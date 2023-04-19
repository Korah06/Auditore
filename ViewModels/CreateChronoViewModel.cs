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
    public class CreateChronoViewModel
    {
        private string _chronoName = string.Empty;
        public string ChronoName
        {
            get { return _chronoName; }
            set { _chronoName = value; }
        }
        private int _chronoMinutes = 0;
        public int ChronoMinutes
        {
            get { return _chronoMinutes; }
            set { _chronoMinutes = value; }
        }
        private Category _selectedCat;
        public Category SelectedCat
        {
            get { return _selectedCat; }
            set { _selectedCat = value; }
        }

        private List<Category> _categories;
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        private IChronoService _chronoService;
        public CreateChronoViewModel(IChronoService chronoService) 
        {
            _chronoService = chronoService;
        }

        public ICommand CreateChronoCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("//Chrono/CreateChronoDesktop");
        });

    }
}
