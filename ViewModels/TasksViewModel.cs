using Auditore.Models;
using Auditore.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        
        public ObservableCollection<MyTask> Tasks { get; set; } = new ObservableCollection<MyTask>();
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        


        private readonly ITaskService _taskService;
        private readonly ICategoryService _categoryService;

        public Command RefreshCommand { get; set; }
        public TasksViewModel(ITaskService taskService, ICategoryService categoryService) 
        { 
            _taskService = taskService;
            _categoryService = categoryService;
            RefreshCommand = new Command(() => OnRefresh());
            ObtainTasks();
        }
        

        private void OnRefresh()
        {
            ObtainTasks();
        }

        private void ObtainTasks()
        {
            IsLoading= true;

            Task.Run(async () => 
            {
                _tasks = await _taskService.GetTasks(Preferences.Default.Get("token", ""));

                App.Current.Dispatcher.Dispatch(() =>
                {
                    foreach(var task in _tasks)
                    {
                        Tasks.Add(task);
                    }
                    IsLoading = false;

                });
            });


        }


    }
}
