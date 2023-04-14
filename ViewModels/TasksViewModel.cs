using Auditore.Models;
using Auditore.Services.Interfaces;
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
        public bool _isRefreshing;
        private List<MyTask> _tasks;
        private int pageSize = 20;
        public bool isRefreshing
        {
            get { return _isRefreshing; }
            set { _isRefreshing = value; }
        }
        public ObservableCollection<MyTask> Tasks { get; set; } = new ObservableCollection<MyTask>();
        //public ObservableCollection<MyTask> Tasks
        //{
        //    get { return _tasks; }
        //    set { _tasks = value; }
        //}


        private readonly ITaskService _taskService;

        public Command RefreshCommand { get; set; }
        public TasksViewModel(ITaskService taskService) 
        { 
            _taskService = taskService;
            RefreshCommand = new Command(() => OnRefresh());
            ObtainTasks();
        }

        public void OnRefresh()
        {
            ObtainTasks();
        }

        private void ObtainTasks()
        {
            isRefreshing = true;
            Tasks.Clear();
            Task.Run(async () => 
            {
                _tasks = await _taskService.GetTasks(Preferences.Default.Get("token", ""));

                App.Current.Dispatcher.Dispatch(() =>
                {
                    var tasksToAdd = _tasks.Take(pageSize).ToList();
                    foreach(var task in tasksToAdd)
                    {
                        Tasks.Add(task);
                    }
                });
            });

            isRefreshing = false;

        }


    }
}
