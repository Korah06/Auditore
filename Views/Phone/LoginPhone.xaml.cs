using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Phone;

public partial class LoginPhone : ContentPage
{
    private readonly IUserService _userService;
    public LoginPhone(IUserService userService)
	{
		InitializeComponent();
        _userService = userService;
        this.BindingContext = new LoginViewModel(_userService);
    }

    private void GoToRegisterAsync(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("//Login/RegisterPhone");
    }
}