using Auditore.Services;
using Auditore.Services.Interfaces;
using Auditore.ViewModels;
using Microsoft.Maui.Controls;

namespace Auditore.Views.Desktop;

public partial class ProfileDesktop : ContentPage
{
    private readonly ProfileViewModel _profileViewModel;
	public ProfileDesktop
		(ITaskService taskService, ICategoryService categoryService, 
		IChronoService chronoService, IUserService userService,IDiagnosticService diagnosticService)
	{
        _profileViewModel = 
			new ProfileViewModel(taskService,categoryService,chronoService,userService,diagnosticService);
		InitializeComponent();
		this.BindingContext = _profileViewModel;
        Appearing += ProfileDesktop_Appearing;
    }

    private void ProfileDesktop_Appearing(object sender, EventArgs e)
    {
        _profileViewModel.GetClassifyInfo();
    }
}