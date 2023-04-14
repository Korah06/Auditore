using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class LoginDesktop : ContentPage
{
	public LoginDesktop()
	{
		InitializeComponent();
		this.BindingContext = new LoginViewModel();
	}
}