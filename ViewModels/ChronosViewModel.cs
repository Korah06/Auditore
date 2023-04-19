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
    public class ChronosViewModel
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; }
        }


        #region Collections
        private List<MyTask> _tasks;
        private List<Category> _categories;
        private List<Chrono> _chronos;
        public ObservableCollection<MyTask> Tasks { get; set; } = new ObservableCollection<MyTask>();
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public ObservableCollection<Chrono> Chronos { get; set; } = new ObservableCollection<Chrono>(); 
        #endregion

        private readonly ITaskService _taskService;
        private readonly ICategoryService _categoryService;
        private readonly IChronoService _chronoService;
        public ChronosViewModel
            (IChronoService chronoService, ITaskService taskService, ICategoryService categoryService) 
        {
            _categoryService = categoryService;
            _taskService = taskService;
            _chronoService = chronoService;
            ObtainChronos();
        }

        public async void ObtainChronos()
        {
            IsLoading = true;
            await Task.Run(async () =>
            {
                _chronos = await _chronoService.GetChronos(Preferences.Default.Get("token",""));

                App.Current.Dispatcher.Dispatch(() =>
                {
                    foreach(Chrono chrono in _chronos)
                    {
                        Chronos.Add(chrono);
                    }
                });
            });

            IsLoading = false;
        }

        public ICommand CreateChronoCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("//Tasks/CreateChronoDesktop");
        });

    }
}
