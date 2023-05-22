using Auditore.Models;
using Auditore.Services.Interfaces;
using Auditore.ViewModels;
using CommunityToolkit.Maui.Views;

namespace Auditore.Views.Phone;

public partial class DiagnosticPopUp : Popup
{
    private readonly DiagnosticViewModel _viewModel;
    public DiagnosticPopUp(IDiagnosticService diagnosticService)
	{
		InitializeComponent();
		_viewModel = new DiagnosticViewModel(diagnosticService);
		this.BindingContext = _viewModel;
	}
    private void CloseButton(object sender, EventArgs e)
    {
        Close(true);
    }

}