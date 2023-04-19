using Auditore.Models;
using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.ViewModels
{
    public class ChronosViewModel
    {
        public ObservableCollection<MyTask> Tasks { get; set; } = new ObservableCollection<MyTask>();
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public ObservableCollection<Chrono> Chronos { get; set; } = new ObservableCollection<Chrono>();

        private readonly ITaskService _taskService;
        private readonly ICategoryService _categoryService;
        private readonly IChronoService _chronoService;
        public ChronosViewModel
            (IChronoService chronoService, ITaskService taskService, ICategoryService categoryService) 
        {
            _categoryService = categoryService;
            _taskService = taskService;
            _chronoService = chronoService;
        }

    }
}
