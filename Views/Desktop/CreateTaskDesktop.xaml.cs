using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class CreateTaskDesktop : ContentPage
{
    private readonly ITaskService _taskService;
    private readonly ICategoryService _categoryService;
    public CreateTaskDesktop(ITaskService taskService, ICategoryService categoryService)
	{
		_categoryService = categoryService;
		_taskService = taskService;
		InitializeComponent();
		this.BindingContext = new CreateTaskViewModel(_categoryService,_taskService);
	}
}