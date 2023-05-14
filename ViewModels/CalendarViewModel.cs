﻿using Auditore.Models;
using Auditore.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.ViewModels
{
    public partial class CalendarViewModel : INotifyPropertyChanged
    {

        private DateTime _currentDate = DateTime.Now;
        public DateTime CurrentDate
        {
            get { return _currentDate; }
            set
            {
                if (_currentDate == value) { return; }
                _currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));

            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ITaskService _taskService;
        private readonly ICategoryService _categoryService;

        private List<MyTask> _tasks;
        private List<Category> _categories;

        public ObservableCollection<MyTask> Tasks { get; set; } = new ObservableCollection<MyTask>();
        public ObservableCollection<MyTask> EndTasks { get; set; } = new ObservableCollection<MyTask>();
        public ObservableCollection<MyTask> StartTasks { get; set; } = new ObservableCollection<MyTask>();
        public ObservableCollection<MyTask> BetweenTasks { get; set; } = new ObservableCollection<MyTask>();
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public CalendarViewModel(ITaskService taskService, ICategoryService categoryService)
        {
            _taskService = taskService;
            _categoryService = categoryService;
        }

        public async Task GetData()
        {
            _tasks = await _taskService.GetTasks(Preferences.Default.Get("token", ""));
            _categories = await _categoryService.GetCategories(Preferences.Default.Get("token", ""));

            if (_categories != null && _tasks != null)
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
                FilterTasksOfTheDay();
            }
            
        }

        public void FilterTasksOfTheDay()
        {
            var filterEndTasks = _tasks.Where(task => task.EndDate.Date == CurrentDate.Date).ToList();
            var filterStartTasks = _tasks.Where(task => task.StartDate.Date == CurrentDate.Date).ToList();
            var filterBetweenTasks = 
                _tasks.Where(task => task.StartDate.Date < CurrentDate.Date && task.EndDate.Date > CurrentDate.Date)
                .ToList();
            filterStartTasks.RemoveAll(task => filterEndTasks.Any(endTask => endTask._id == task._id));

            foreach(var task in filterStartTasks)
            {
                StartTasks.Add(task);
            }
            foreach(var task in filterBetweenTasks)
            {
                BetweenTasks.Add(task);
            }
            foreach(var task in filterEndTasks)
            {
                EndTasks.Add(task);
            }


        }
    }
}
