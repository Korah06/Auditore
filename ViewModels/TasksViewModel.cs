using Auditore.Models;
using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
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
        public bool isRefreshing
        {
            get { return _isRefreshing; }
            set { _isRefreshing = value; }
        }
        public List<MyTask> tasks
        {
            get { return _tasks; }
            set { _tasks = value; }
        }
        private readonly ITaskService _taskService;

        public Command RefreshCommand { get; set; }
        public TasksViewModel(ITaskService taskService) 
        { 
            _taskService = taskService;
            RefreshCommand = new Command(async () => OnRefresh());
            Task.Run(ObtainTasks);
        }

        public void OnRefresh()
        {
            Task.Run(ObtainTasks);
        }

        private async Task ObtainTasks()
        {
            isRefreshing = true;
            tasks = await _taskService.GetTasks(Preferences.Default.Get("token",""));
            isRefreshing = false;
        }
        

    }
}
