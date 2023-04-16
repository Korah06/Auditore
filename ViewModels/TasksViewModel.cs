﻿using Auditore.Models;
using Auditore.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Auditore.ViewModels
{
    public class TasksViewModel
    {

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; }
        }
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { _isRefreshing = value; }
        }

        private List<MyTask> _tasks;
        private List<Category> _categories;
        
        public ObservableCollection<MyTask> Tasks { get; set; } = new ObservableCollection<MyTask>();
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        


        private readonly ITaskService _taskService;
        private readonly ICategoryService _categoryService;

        public Command RefreshCommand { get; set; }
        public TasksViewModel(ITaskService taskService, ICategoryService categoryService) 
        { 
            _taskService = taskService;
            _categoryService = categoryService;
            Tasks.CollectionChanged += Tasks_CollectionChanged;
            RefreshCommand = new Command(() => OnRefresh());
            ObtainTasks();
        }
        

        public void OnRefresh()
        {
            
            Tasks.Clear();
            Categories.Clear();
            ObtainTasks();
        }

        private void Tasks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MyTask newItem in e.NewItems)
                {

                    //Add listener for each item on PropertyChanged event
                    newItem.PropertyChanged += this.OnItemPropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (MyTask oldItem in e.OldItems)
                {
                    oldItem.PropertyChanged -= this.OnItemPropertyChanged;
                }
            }
            //UpdateTasks();
        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateTasks();
        }

        public async void UpdateTasks() 
        {
            bool updated = await _taskService.UpdateTasks(_tasks, Preferences.Default.Get("token", ""));
            if (updated)
            {

                foreach (var c in Categories)
                {
                    var tasks = from t in Tasks
                                where t.CategoryId == c._id
                                select t;

                    var completed = from t in tasks
                                    where t.Completed == true
                                    select t;

                    var notCompleted = from t in tasks
                                       where t.Completed == false
                                       select t;



                    c.PendingTasks = notCompleted.Count();
                    c.Percentage = (float)completed.Count() / (float)tasks.Count();
                }
                foreach (var t in Tasks)
                {
                    var catColor =
                         (from c in Categories
                          where c._id == t.CategoryId
                          select c.Color).FirstOrDefault();
                    t.TaskColor = catColor;
                }
            }
        }

        public ICommand CreateTaskCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("//Tasks/CreateTaskDesktop");
        });

        private void ObtainTasks()
        {
        IsLoading= true;
        Task.Run(async () => 
        {
                
            _tasks = await _taskService.GetTasks(Preferences.Default.Get("token", ""));
            _categories = await _categoryService.GetCategories(Preferences.Default.Get("token", ""));

            App.Current.Dispatcher.Dispatch(() =>
            {
                foreach(var category in _categories)
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

                foreach(var task in _tasks)
                {
                    var catColor =
                    (from c in Categories
                    where c._id == task.CategoryId
                    select c.Color).FirstOrDefault();

                    task.TaskColor = catColor;
                    Tasks.Add(task);
                }
                IsLoading = false;

            });
        });


        }


    }
}
