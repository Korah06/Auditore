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
        public async void Countdown()
        {
            
            int totalSeconds = _selectedChrono.Minutes * 60;
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

                    await Task.Delay(1000);
                    totalSeconds--;
                    Debug.WriteLine("____________"+totalSeconds+"______________" + ShowTime);
                }
                else
                {
                    Debug.WriteLine("Soy false: " + isRunning);
                    await Task.Delay(50);
                }
                if (isReset)
                {
                    totalSeconds = 0;
                    isReset = false;

                    return;
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



        public ICommand SelectCommand => new Command<object>((obj) =>
        {
            _selectedChrono = obj as Chrono;
            Countdown();
        });

        
        public ICommand StartPauseCommand => new Command(() =>
        {
            isRunning = !isRunning;
        });

        public ICommand ResetCommand => new Command(async () =>
        {
            isReset = true;
            ShowTime = "0:00";
            isRunning = false;
            Countdown();

        });

    }
}
