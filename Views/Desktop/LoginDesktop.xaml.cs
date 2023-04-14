using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class LoginDesktop : ContentPage
{
    private readonly IUserService _userService;
    public LoginDesktop(IUserService userService)
	{
		InitializeComponent();
		_userService = userService;
		this.BindingContext = new LoginViewModel(_userService);
	}

    private void GoToRegisterAsync(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("//Login/RegisterDesktop");
    }
}