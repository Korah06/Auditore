using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class RegisterDesktop : ContentPage
{
    private readonly IUserService _userService;
    private readonly RegisterViewModel _viewModel;
    public RegisterDesktop(IUserService userService)
	{
        _viewModel = new RegisterViewModel(userService);
        InitializeComponent();
        _userService = userService;
        this.BindingContext = _viewModel;
        Disappearing += RegisterDesktop_Disappearing;
	}

    private void RegisterDesktop_Disappearing(object sender, EventArgs e)
    {
        usernameEntry.Text= string.Empty;
        cpasswordEntry.Text= string.Empty;
        passwordEntry.Text= string.Empty;
        emailEntry.Text= string.Empty;
        surnamesEntry.Text= string.Empty;
        nameEntry.Text= string.Empty;
    }
}