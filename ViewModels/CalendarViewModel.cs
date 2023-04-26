using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.ViewModels
{
    public class CalendarViewModel
    {
        private readonly ITaskService _taskService;
        private readonly ICategoryService _categoryService;
        public CalendarViewModel(ITaskService taskService, ICategoryService categoryService)
        {
            _taskService = taskService;
            _categoryService = categoryService;
        }
    }
}
