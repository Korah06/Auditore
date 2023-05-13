using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class DiagnosticDesktop : ContentPage
{
	private readonly DiagnosticViewModel _diagnosticViewModel;
	public DiagnosticDesktop(IDiagnosticService diagnosticService)
	{
		_diagnosticViewModel = new DiagnosticViewModel(diagnosticService);
		InitializeComponent();
		this.BindingContext = _diagnosticViewModel;
	}
}