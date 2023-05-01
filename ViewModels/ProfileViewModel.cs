using Auditore.Models;
using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.ViewModels
{
    public class ProfileViewModel
    {


        #region Collections
        private List<MyTask> _tasks;
        private List<MyTask> _completedTasks;
        private List<MyTask> _nearlyTasks;
        private List<Category> _categories;
        private List<Chrono> _chronos;
        public ObservableCollection<MyTask> CompletedTasks { get; set; } = new ObservableCollection<MyTask>();
        public ObservableCollection<MyTask> Tasks { get; set; } = new ObservableCollection<MyTask>();
        public ObservableCollection<MyTask> NearlyTasks { get; set; } = new ObservableCollection<MyTask>();
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public ObservableCollection<Chrono> Chronos { get; set; } = new ObservableCollection<Chrono>();
        public ObservableCollection<Chrono> Pomodoros { get; set; } = new ObservableCollection<Chrono>();
        #endregion

        private double _taskPercentage;

        public double TaskPercentage
        {
            get { return _taskPercentage; }
            set 
            {
                if(_taskPercentage != value)
                {
                    _taskPercentage = value;
                    OnPropertyChanged(nameof(TaskPercentage));
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ITaskService _taskService;
        private readonly ICategoryService _categoryService;
        private readonly IChronoService _chronoService;
        public ProfileViewModel
            (ITaskService taskService, ICategoryService categoryService, IChronoService chronoService)
        {
            _taskService = taskService;
            _categoryService = categoryService;
            _chronoService = chronoService;
            GetClassifyInfo();
        }

        public static List<MyTask> ObtainNearlyTasks(List<MyTask> tasks)
        {
            tasks.Sort((task1, task2) =>
            {
                DateTime fecha1 = task1.EndDate;
                DateTime fecha2 = task2.EndDate;
                TimeSpan diferencia1 = DateTime.Now - fecha1;
                TimeSpan diferencia2 = DateTime.Now - fecha2;
                return diferencia1.Duration().CompareTo(diferencia2.Duration());
            });

            return tasks.Where(task => task.EndDate>= DateTime.Now).Take(3).ToList();
        }

        public async void GetClassifyInfo()
        {
            Tasks.Clear();
            Categories.Clear();
            Chronos.Clear();
            Pomodoros.Clear();
            NearlyTasks.Clear();
            CompletedTasks.Clear();

            _tasks = await _taskService.GetTasks(Preferences.Default.Get("token", ""));
            _categories = await _categoryService.GetCategories(Preferences.Default.Get("token", ""));
            _chronos = await _chronoService.GetChronos(Preferences.Default.Get("token", ""));

            if(_tasks != null)
            {
                foreach (var task in _tasks)
                {
                    if (task.Completed)
                    {
                        CompletedTasks.Add(task);
                    }
                    Tasks.Add(task);
                }
                _nearlyTasks = ObtainNearlyTasks(_tasks);
                foreach (var task in _nearlyTasks)
                {
                    NearlyTasks.Add(task);
                }
                //TaskPercentage = (double)CompletedTasks.Count/(double)Tasks.Count;

            }
            if(_categories != null)
            {
                foreach (var category in _categories)
                {
                    Categories.Add(category);
                }
            }
            
            if (_chronos != null)
            {
                foreach (var chrono in _chronos)
                {
                    if (chrono.IsPomodoro)
                    {
                        Pomodoros.Add(chrono);
                    }
                    else
                    {
                        Chronos.Add(chrono);
                    }
                }
            }
        }
    }
}
