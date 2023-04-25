using Auditore.Models;
using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace Auditore.ViewModels
{
    public class ChronosViewModel : INotifyPropertyChanged
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; }
        }

        private Chrono _selectedChrono;
        public Chrono SelectedChrono
        {
            get { return _selectedChrono; }
            set { _selectedChrono = value; }
        }


        private string _timer;
        public string Timer
        {
            get { return _timer; }
            set { _timer = value; }
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

        public void OnRefresh()
        {
            Chronos.Clear();
            ObtainChronos();
        }
        #region Functions
        public async void ObtainChronos()
        {
            IsLoading = true;
            await Task.Run(async () =>
            {
                _chronos = await _chronoService.GetChronos(Preferences.Default.Get("token", ""));

                App.Current.Dispatcher.Dispatch(() =>
                {
                    foreach (Chrono chrono in _chronos)
                    {
                        Chronos.Add(chrono);
                    }
                });
            });

            IsLoading = false;
        }

        private bool isRunning = false;
        private string _showTime = "00:00";
        public string ShowTime
        {
            get { return _showTime; }
            set
            {
                if (_showTime != value)
                {
                    _showTime = value;
                    OnPropertyChanged(nameof(ShowTime));
                }
            }
        }
        private bool isReset = false;
        private bool isChanging = false;
        private bool Finished = true;
        int totalSeconds = 0;
        public async void Countdown()
        {
            totalSeconds = 0;

            if(_selectedChrono != null)
            {
                totalSeconds = _selectedChrono.Minutes * 60;
                
            }
            int minutesLeft;
            int secondsLeft;

            while (totalSeconds > 0)
            {
                
                Debug.WriteLine("Inicio: " + isRunning);
                if (isRunning)
                {
                    Debug.WriteLine("Soy true: " + isRunning);
                    minutesLeft = totalSeconds / 60;
                    secondsLeft = totalSeconds % 60;

                    ShowTime = string.Format("{0}:{1:00}", minutesLeft, secondsLeft);

                    totalSeconds--;
                    await Task.Delay(1000);

                    
                    Debug.WriteLine("____________"+totalSeconds+"______________" + ShowTime + "reset: " + isReset);

                    if (isReset)
                    {

                        isReset = false;
                        return;
                    }
                    if (isChanging)
                    {
                        totalSeconds = 0;
                        return;
                    }
                    Debug.WriteLine
                        ("Soy false: " + isRunning + " reset: " + isReset +
                        " secs: " + totalSeconds + "reset: " + isReset);
                }
                else
                {
                    
                    await Task.Delay(50);
                    if (isReset)
                    {

                        isReset = false;
                        return;
                    }
                    if (isChanging)
                    {
                        totalSeconds = 0;
                        return;
                    }
                    Debug.WriteLine
                        ("Soy false: " + isRunning + " reset: " + isReset + 
                        " secs: " + totalSeconds + "reset: " + isReset);
                }
                
            }


        }

        
        #region Showtime
        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion


       

        #endregion

        public ICommand CreateChronoCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("//Chrono/CreateChronoDesktop");
        });

        public ICommand DeleteChrono => new Command<object>(async (obj) =>
        {
            bool deleted = false;
            var chrono = obj as Chrono;
            bool answer = await Application.Current.MainPage.DisplayAlert
            ("Eliminar", "Quieres eliminar el Cronometro?", "Yes", "No");
            if (answer)
            {
                deleted = await _chronoService.DeleteChrono(chrono._id, Preferences.Default.Get("token", ""));
            }
            
            if (deleted)
            {
                OnRefresh();
            }
        });



        public ICommand SelectCommand => new Command<object>((obj) =>
        {
            if (Finished)
            {
                if (obj != null)
                {
                    _selectedChrono = obj as Chrono;
                }
                int minutesLeft = _selectedChrono.Minutes;
                int secondsLeft = (_selectedChrono.Minutes * 60) % 60;

                ShowTime = string.Format("{0}:{1:00}", minutesLeft, secondsLeft);
            }
            

        });

        private bool init = false;
        public ICommand StartPauseCommand => new Command(() =>
        {
            if (init)
            {
                isRunning = !isRunning;
                Debug.WriteLine("Is Running: " + isRunning);
            }
            else
            {
                Finished = false;
                isRunning = true;
                init = true;
                Countdown();
                Debug.WriteLine("Started");
            }
        });
        public ICommand EndCommand => new Command(() =>
        {
            if (init)
            {
                totalSeconds = 0;
                init = false;
                Finished = true;
                Debug.WriteLine("Finished");
            }
        });

        public ICommand ResetCommand => new Command(async () =>
        {
            if (_selectedChrono != null)
            {
                Debug.WriteLine("Reset: "+ isReset);
                isReset = true;
                await Task.Delay(50);
                int minutesLeft = _selectedChrono.Minutes;
                int secondsLeft = (_selectedChrono.Minutes * 60) % 60;

                ShowTime = string.Format("{0}:{1:00}", minutesLeft, secondsLeft);
                isRunning = false;
                Countdown();
            }
                
        });

        public async void getTasksForChrono()
        {
            Tasks.Clear();
            Categories.Clear();
            await Task.Run(async () =>
            {
                _tasks = await _taskService.GetTasksCategory(_selectedChrono.CategoryId,Preferences.Default.Get("token", ""));
                _categories = await _categoryService.GetCategories(Preferences.Default.Get("token", ""));

                if(_tasks != null )
                {
                    App.Current.Dispatcher.Dispatch(() =>
                    {
                        foreach (var category in _categories)
                        {
                            var tasks = from t in _tasks
                                        where t.CategoryId == category._id
                                        select t;

                            var completed = from t in tasks
                                            where t.Completed == true
                                            select t;

                            var notCompleted = from t in tasks
                                               where t.Completed == false
                                               select t;



                            category.PendingTasks = notCompleted.Count();
                            category.Percentage = (float)completed.Count() / (float)tasks.Count();
                            Categories.Add(category);
                        }

                        foreach (var task in _tasks)
                        {
                            var catColor =
                            (from c in Categories
                             where c._id == task.CategoryId
                             select c.Color).FirstOrDefault();

                            task.TaskColor = catColor;
                            Tasks.Add(task);
                        }

                    });
                }

                
            });
        }

    }
}
