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

        private Diagnostic diagnostic;

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
        private readonly INotificationService _notificationService;
        private readonly IDiagnosticService _diagnosticService;
        public ChronosViewModel
            (IChronoService chronoService, ITaskService taskService, 
            ICategoryService categoryService, INotificationService notificationService,
            IDiagnosticService diagnosticService) 
        {
            _categoryService = categoryService;
            _taskService = taskService;
            _chronoService = chronoService;
            _notificationService = notificationService;
            _diagnosticService = diagnosticService;
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



        #region Countdown
        public async void Countdown()
        {
            totalSeconds = 0;

            if (_selectedChrono != null)
            {
                totalSeconds = _selectedChrono.minutes * 60;

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
                    if (totalSeconds % 60 == 0)
                    {
                        diagnostic.workMinutes++;
                    }
                    await Task.Delay(1000);


                    Debug.WriteLine("____________" + totalSeconds + "______________" + ShowTime + "reset: " + isReset);

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

            if (totalSeconds == 0)
            {
                await _notificationService.EndChrono();
            }

            init = false;
            Finished = true;

            foreach (MyTask task in Tasks)
            {
                if (task.Completed)
                {
                    diagnostic.tasksId.Add(task.Name);
                }
            }
            await _diagnosticService.CreateDiagnostic(diagnostic, Preferences.Default.Get("token", ""));
            List<MyTask> nTasks = new List<MyTask>();
            foreach (MyTask task in Tasks)
            {
                nTasks.Add(task);
            }
            await _taskService.UpdateTasks(nTasks, Preferences.Default.Get("token", ""));

        } 
        #endregion

        private int _repeats = 3;
        public int Repeats
        {
            get { return _repeats; }
            set { _repeats = value; }
        }
        private int sleepSeconds = 0;
        #region Pomodoro
        public async void Pomodoro()
        {
            totalSeconds = 0;
            if (_selectedChrono != null)
            {
                totalSeconds = _selectedChrono.minutes * 60;
                sleepSeconds = _selectedChrono.restMinutes * 60;

            }

            int minutesLeft;
            int secondsLeft;

            for (int i = 0; i < Repeats; i++)
            {
                if (Finished)
                {
                    return;
                }
                if (i > 0)
                {
                    while (sleepSeconds > 0)
                    {
                        if (isRunning)
                        {
                            minutesLeft = sleepSeconds / 60;
                            secondsLeft = sleepSeconds % 60;

                            ShowTime = string.Format("{0}:{1:00}", minutesLeft, secondsLeft);
                            sleepSeconds--;
                            if (sleepSeconds % 60 == 0)
                            {
                                diagnostic.restMinutes++;
                            }
                            await Task.Delay(1000);
                            if (isReset)
                            {

                                isReset = false;
                                return;
                            }
                            if (Finished)
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (isReset)
                            {

                                isReset = false;
                                return;
                            }
                            if (Finished)
                            {
                                return;
                            }
                            await Task.Delay(50);
                            Debug.WriteLine
                            ("Soy false: " + isRunning + " reset: " + isReset +
                            " secs: " + totalSeconds + "reset: " + isReset);
                        }
                        if (Finished)
                        {
                            return;
                        }
                    }
                    //reasignar valor de sleepSeconds
                    sleepSeconds = _selectedChrono.restMinutes * 60;
                    await _notificationService.EndRest();
                }

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
                        if (totalSeconds % 60 == 0)
                        {
                            diagnostic.workMinutes++;
                        }
                        if (Finished)
                        {
                            return;
                        }
                        await Task.Delay(1000);


                        Debug.WriteLine("____________" + totalSeconds + "______________" + ShowTime + "reset: " + isReset);

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
                        if (Finished)
                        {
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
                        if (Finished)
                        {
                            return;
                        }
                        Debug.WriteLine
                            ("Soy false: " + isRunning + " reset: " + isReset +
                            " secs: " + totalSeconds + "reset: " + isReset);
                    }

                }
                totalSeconds = _selectedChrono.minutes * 60;
                await _notificationService.EndWorkTime();
            }

            init = false;
            Finished = true;
            await _notificationService.EndPomodoro();
            foreach (MyTask task in Tasks)
            {
                if (task.Completed)
                {
                    diagnostic.tasksId.Add(task.Name);
                }
            }
            await _diagnosticService.CreateDiagnostic(diagnostic, Preferences.Default.Get("token", ""));
            List<MyTask> nTasks = new List<MyTask>();
            foreach (MyTask task in Tasks)
            {
                nTasks.Add(task);
            }
            await _taskService.UpdateTasks(nTasks, Preferences.Default.Get("token", ""));
        } 
        #endregion

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
#if ANDROID || IOS
            await Shell.Current.GoToAsync("//Chrono/CreateChronoPhone");
#else
            await Shell.Current.GoToAsync("//Chrono/CreateChronoDesktop");

#endif
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

        public bool Deleting = true;
        public ICommand DeleteChronoPhone => new Command(async () =>
        {
            bool deleted = false;
            bool answer = await Application.Current.MainPage.DisplayAlert
            ("Eliminar", "Quieres eliminar el Cronometro?", "Yes", "No");
            if (answer)
            {
                deleted = await _chronoService.DeleteChrono(SelectedChrono._id, Preferences.Default.Get("token", ""));
            }
            Deleting = false;
            await Task.Delay(100);
            Deleting = true;
        });


        private List<MyTask> _tasks_not_completed;
        public ICommand SelectCommand => new Command<object>(async (obj) =>
        {
            if (Finished)
            {
                if (obj != null)
                {
                    _selectedChrono = obj as Chrono;

                    await Task.Run( async() =>
                    {
                        Tasks.Clear();
                        _tasks = await _taskService.GetTasksCategory
                        (_selectedChrono.categoryId, Preferences.Default.Get("token", ""));
                        if (_tasks != null)
                        {
                            App.Current.Dispatcher.Dispatch(() =>
                            {
                                _tasks_not_completed = _tasks.FindAll(t => !t.Completed);

                                foreach (var task in _tasks_not_completed)
                                {
                                    Tasks.Add(task);
                                }
                            });
                        }
                        OnRefresh();
                    });
                }

                int minutesLeft = _selectedChrono.minutes;
                int secondsLeft = (_selectedChrono.minutes * 60) % 60;

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
                diagnostic = new()
                {
                    idCategory = _selectedChrono.categoryId,
                    idUser = _selectedChrono.userId,
                    repeats = Repeats,
                    name = _selectedChrono.name + "-" + DateTime.Now.ToString("d"),
                    tasksId = new List<string>()
                };
                if (_selectedChrono.IsPomodoro)
                {
                    Pomodoro();
                }
                else
                {
                    Countdown();
                }
                Debug.WriteLine("Started");
            }
        });
        public ICommand EndCommand => new Command( async () =>
        {
            if (init)
            {
                totalSeconds = 0;
                init = false;
                Finished = true;
                //enviar diagnostic
                foreach (MyTask task in Tasks) 
                {
                    if (task.Completed)
                    {
                        diagnostic.tasksId.Add(task.Name);
                    }
                }
                await _diagnosticService.CreateDiagnostic(diagnostic,Preferences.Default.Get("token",""));
                Debug.WriteLine("Finished");
                List<MyTask> nTasks = new List<MyTask>();
                foreach (MyTask task in Tasks)
                {
                    nTasks.Add(task);
                }
                await _taskService.UpdateTasks(nTasks, Preferences.Default.Get("token", ""));
            }
        });

        public ICommand ResetCommand => new Command(async () =>
        {
            if (_selectedChrono != null)
            {
                Debug.WriteLine("Reset: "+ isReset);
                isReset = true;
                await Task.Delay(50);
                int minutesLeft = _selectedChrono.minutes;
                int secondsLeft = (_selectedChrono.minutes * 60) % 60;

                ShowTime = string.Format("{0}:{1:00}", minutesLeft, secondsLeft);
                isRunning = false;
                diagnostic = new()
                {
                    idCategory = _selectedChrono.categoryId,
                    idUser = _selectedChrono.userId,
                    repeats = Repeats,
                    name = _selectedChrono.name + "-" + DateTime.Now
                };
                if (_selectedChrono.IsPomodoro)
                {
                    Pomodoro();
                }
                else
                {
                    Countdown();
                }
            }
                
        });

        //public async void getTasksForChrono()
        //{
        //    Tasks.Clear();
        //    Categories.Clear();
        //    await Task.Run(async () =>
        //    {
        //        _tasks = await _taskService.GetTasksCategory(_selectedChrono.categoryId, Preferences.Default.Get("token", ""));
        //        _categories = await _categoryService.GetCategories(Preferences.Default.Get("token", ""));

        //        if(_tasks == null ){return;}

        //        foreach (var task in _tasks)
        //        {

        //            Tasks.Add(task);
        //        }

        //    });
        //}

    }
}
