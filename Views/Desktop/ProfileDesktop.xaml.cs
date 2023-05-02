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
		IChronoService chronoService, IUserService userService)
	{
        _profileViewModel = new ProfileViewModel(taskService,categoryService,chronoService,userService);
		InitializeComponent();
		this.BindingContext = _profileViewModel;
    }

}