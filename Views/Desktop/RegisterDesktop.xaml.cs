using Auditore.Services.Interfaces;

namespace Auditore.Views.Desktop;

public partial class RegisterDesktop : ContentPage
{
    private readonly IUserService _userService;
    public RegisterDesktop(IUserService userService)
	{
        InitializeComponent();
        _userService = userService;
        InitializeComponent();
	}
}