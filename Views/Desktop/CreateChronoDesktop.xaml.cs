using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class CreateChronoDesktop : ContentPage
{
    private readonly IChronoService _chronoService;
    private readonly ICategoryService _categoryService;
	private readonly CreateChronoViewModel _createChronoViewModel;

    public CreateChronoDesktop(IChronoService chronoService, ICategoryService categoryService)
	{
		_chronoService = chronoService;
		_categoryService = categoryService;
		_createChronoViewModel = new CreateChronoViewModel(_chronoService,_categoryService);
		InitializeComponent();
		this.BindingContext = _createChronoViewModel;
		
	}
}