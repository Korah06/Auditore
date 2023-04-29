using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class ProfileDesktop : ContentPage
{
	private readonly ProfileViewModel _profileViewModel;
	public ProfileDesktop(ProfileViewModel profileViewModel)
	{
		_profileViewModel = profileViewModel;
		InitializeComponent();
		this.BindingContext = _profileViewModel;
	}
}