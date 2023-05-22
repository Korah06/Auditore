using Auditore.ViewModels;
using CommunityToolkit.Maui.Views;

namespace Auditore.Views.Phone;

public partial class UserDetailPopUp : Popup
{
    private readonly AdminViewModel _adminViewModel;
    public UserDetailPopUp(AdminViewModel adminViewModel)
	{
		InitializeComponent();
		_adminViewModel = adminViewModel;
		this.BindingContext = _adminViewModel;
	}
    private void CloseButton(object sender, EventArgs e)
    {
        Close(true);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        while (_adminViewModel.Deleting)
        {
            await Task.Delay(50);
        }
        Close(true);
    }
}