using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class AdminDesktop : ContentPage
{
	private AdminViewModel _adminViewModel;
	public AdminDesktop(IUserService userService)
	{
		_adminViewModel = new(userService);
		InitializeComponent();
		this.BindingContext = _adminViewModel;
	}

}