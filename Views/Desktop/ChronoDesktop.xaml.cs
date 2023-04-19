using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class ChronoDesktop : ContentPage
{
    private readonly ITaskService _taskService;
    private readonly ICategoryService _categoryService;
    private ChronosViewModel _viewModel;
    public ChronoDesktop(ITaskService taskService, ICategoryService categoryService)
	{   
        _taskService = taskService;
        _categoryService = categoryService;
        _viewModel = new ChronosViewModel();
		InitializeComponent();
        this.BindingContext = _viewModel;
	}
}