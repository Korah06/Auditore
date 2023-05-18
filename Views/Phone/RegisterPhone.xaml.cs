using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Phone;

public partial class RegisterPhone : ContentPage
{
    private readonly IUserService _userService;
    public RegisterPhone(IUserService userService)
    {
        InitializeComponent();
        _userService = userService;
        this.BindingContext = new RegisterViewModel(_userService);
    }
}