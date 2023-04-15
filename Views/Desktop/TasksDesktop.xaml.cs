using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class TasksDesktop : ContentPage
{
	private readonly ITaskService _taskService;
	private readonly ICategoryService _categoryService;
	public TasksDesktop(ITaskService taskService, ICategoryService categoryService)
	{
		InitializeComponent();
        _taskService = taskService;
		_categoryService = categoryService;

        this.BindingContext = new TasksViewModel(_taskService, _categoryService);
	}
}