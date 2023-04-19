using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class CreateChronoDesktop : ContentPage
{
	IChronoService _chronoService;
	CreateChronoViewModel _createChronoViewModel;

    public CreateChronoDesktop(IChronoService chronoService)
	{
		_chronoService = chronoService;
		_createChronoViewModel = new CreateChronoViewModel(_chronoService);
		InitializeComponent();
		this.BindingContext = _createChronoViewModel;
		
	}
}