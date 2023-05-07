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

    private void collectionUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		if (sender != null)
		{
            ContentGrid.IsVisible = true;

            lblusername.SetBinding(Entry.TextProperty, new Binding("ItemSelected.username"));
            lblname.SetBinding(Entry.TextProperty, new Binding("ItemSelected.name"));
            surnamelbl.SetBinding(Entry.TextProperty, new Binding("ItemSelected.surname"));
            emaillbl.SetBinding(Entry.TextProperty, new Binding("ItemSelected.email"));
            if(_adminViewModel.ItemSelected.rol == "admin")
            {
                userRoles.SelectedItem = "admin";
            }
            else
            {
                userRoles.SelectedItem = "basic";
            }
        }
    }
}