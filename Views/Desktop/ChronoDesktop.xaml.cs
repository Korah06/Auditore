using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class ChronoDesktop : ContentPage
{
    private readonly ITaskService _taskService;
    private readonly ICategoryService _categoryService;
    private readonly IChronoService _chronoService;
    private ChronosViewModel _viewModel;
    public ChronoDesktop(ITaskService taskService, ICategoryService categoryService, IChronoService chronoService)
	{   
        _taskService = taskService;
        _categoryService = categoryService;
        _chronoService = chronoService;
        _viewModel = new ChronosViewModel(_chronoService, _taskService, _categoryService);
		InitializeComponent();
        this.BindingContext = _viewModel;
	}
}