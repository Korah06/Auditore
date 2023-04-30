using Auditore.Services;
using Auditore.Services.Interfaces;
using Auditore.ViewModels;
using Microsoft.Maui.Controls;

namespace Auditore.Views.Desktop;

public partial class ProfileDesktop : ContentPage
{
    private readonly ProfileViewModel _profileViewModel;
	public ProfileDesktop(ITaskService taskService, ICategoryService categoryService, IChronoService chronoService)
	{
        Appearing += AppearingFunction;
        _profileViewModel = new ProfileViewModel(taskService,categoryService,chronoService);
		InitializeComponent();
		this.BindingContext = _profileViewModel;
		
    }

    private void AppearingFunction(object sender, EventArgs e)
    {
        _profileViewModel.GetClassifyInfo();
        progressTasks.Progress = _profileViewModel.TaskPercentage;
    }
}