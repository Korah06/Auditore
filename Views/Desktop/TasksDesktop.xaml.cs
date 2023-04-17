using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class TasksDesktop : ContentPage
{
	private readonly ITaskService _taskService;
	private readonly ICategoryService _categoryService;
	private TasksViewModel _viewModel;
	public TasksDesktop(ITaskService taskService, ICategoryService categoryService)
	{
		InitializeComponent();
        _taskService = taskService;
		_categoryService = categoryService;
		_viewModel = new TasksViewModel(_taskService, _categoryService);
        this.BindingContext = _viewModel;
	}

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		taskFounded.IsVisible = false;
    }
}