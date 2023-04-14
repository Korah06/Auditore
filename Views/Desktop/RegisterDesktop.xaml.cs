using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class RegisterDesktop : ContentPage
{
    private readonly IUserService _userService;
    public RegisterDesktop(IUserService userService)
	{
        InitializeComponent();
        _userService = userService;
        this.BindingContext = new RegisterViewModel(_userService);
	}
}