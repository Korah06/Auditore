using Auditore.Models;
using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.ViewModels
{
    public class CreateTaskViewModel
    {

        private string _name = string.Empty;
        private string _description = string.Empty;
        private DateTimeOffset _startDate = DateTimeOffset.Now;
        private DateTimeOffset _endDate = DateTimeOffset.Now;
        private string _categoryId = string.Empty;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }

        public DateTimeOffset StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTimeOffset EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

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
