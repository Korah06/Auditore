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
    public class CreateTaskViewModel
    {

        private bool _categoryFactory { get; set; }
        public bool CategoryFactory
        {
            get { return _categoryFactory; }
            set { _categoryFactory = value; }
        }

        #region TaskVariablesRegion
        private string _taskName = string.Empty;
        private string _description = string.Empty;
        private DateTime _startDate = DateTime.Now;
        private DateTime _endDate = DateTime.Now;
        private Category _selectedCat;
        private string _categoryName = string.Empty;
        public string TaskName
        {
            get { return _taskName; }
            set { _taskName = value; }
        }
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public Category SelectedCat
        {
            get { return _selectedCat; }
            set { _selectedCat = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        } 
        #endregion

        private List<Category> _categories;
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        private readonly ICategoryService _categoryService;
        private readonly ITaskService _taskService;
        public CreateTaskViewModel(ICategoryService categoryService, ITaskService taskService)
        { 
            _categoryService = categoryService;
            _taskService = taskService;
            ObtainCategories();
        }

        public ICommand CreateTaskCommand => new Command(async () =>
        {
            string catId = string.Empty;
            if(_selectedCat != null || _categoryName != string.Empty) 
            {
                if (_selectedCat ==null)
                {
                    catId = await _categoryService
                .CreateCategory(_categoryName, Preferences.Default.Get("token", ""));
                }
                else
                {
                    catId = _selectedCat._id;
                }
                 
            }
            else
            {
                await Application.Current.MainPage
                .DisplayAlert("Error", "Seleccione o cree alguna categoria", "Aceptar");
                return;
            }
            CreateTaskRequest dto = new CreateTaskRequest();
            dto.categoryId = catId;
            dto.startDate = _startDate;
            dto.endDate = _endDate;
            dto.description = _description;
            dto.name = _taskName;
            bool created = await _taskService.CreateTask(dto, Preferences.Default.Get("token", ""));

            if(created)
            {
                await Shell.Current.GoToAsync("//Tasks");
            }
        });

        private void ObtainCategories()
        {
            Task.Run(async () =>
            {

                _categories = await _categoryService.GetCategories(Preferences.Default.Get("token", ""));

                App.Current.Dispatcher.Dispatch(() =>
                {
                    foreach (var category in _categories)
                    {
                        Categories.Add(category);

                    }
                });
            });
        }
    }
}
