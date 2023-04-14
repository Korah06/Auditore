using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class TasksDesktop : ContentPage
{
	private readonly ITaskService _taskService;
	public TasksDesktop(ITaskService taskService)
	{
		InitializeComponent();
        _taskService = taskService;

        this.BindingContext = new TasksViewModel(_taskService);
	}
}