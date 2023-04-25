using Auditore.Dtos.Request;
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
        private bool _chronoPomodoro;
        public bool ChronoPomodoro
        {
            get { return _chronoPomodoro; }
            set { _chronoPomodoro = value; }
        }

        private List<Category> _categories;
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        private readonly IChronoService _chronoService;
        private readonly ICategoryService _categoryService;
        public CreateChronoViewModel(IChronoService chronoService,ICategoryService categoryService) 
        {
            _chronoService = chronoService;
            _categoryService = categoryService;
            ObtainCategories();
        }

        private void ObtainCategories()
        {
            Task.Run(async () =>
            {

                _categories = await _categoryService.GetCategories(Preferences.Default.Get("token", ""));

                App.Current.Dispatcher.Dispatch(() =>
                {
                    foreach (var category in _categories) {Categories.Add(category);}
                });
            });
        }

        #region Commands
        public ICommand CreateChronoCommand => new Command(async () =>
        {
            CreateChronoRequest dto = new CreateChronoRequest
            {
                categoryId = _selectedCat._id,
                minutes = _chronoMinutes,
                name = _chronoName,
                IsPomodoro = _chronoPomodoro
            };
            bool created = await _chronoService.CreateChrono(dto, Preferences.Default.Get("token", ""));
            if (created)
            {
                await Shell.Current.GoToAsync("//Chrono");
            }
        }); 
        #endregion

    }
}
