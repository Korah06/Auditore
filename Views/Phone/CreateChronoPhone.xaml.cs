using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Phone;

public partial class CreateChronoPhone : ContentPage
{
    private readonly IChronoService _chronoService;
    private readonly ICategoryService _categoryService;
    private readonly CreateChronoViewModel _createChronoViewModel;
    public CreateChronoPhone(IChronoService chronoService, ICategoryService categoryService)
    {
        _chronoService = chronoService;
        _categoryService = categoryService;
        _createChronoViewModel = new CreateChronoViewModel(_chronoService, _categoryService);
        InitializeComponent();
        this.BindingContext = _createChronoViewModel;

    }
}