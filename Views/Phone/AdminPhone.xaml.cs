using Auditore.Models;
using Auditore.ViewModels;
using CommunityToolkit.Maui.Views;

namespace Auditore.Views.Phone;

public partial class AdminPhone : ContentPage
{
	private readonly AdminViewModel _adminViewModel;
	public AdminPhone(AdminViewModel adminViewModel)
	{
		InitializeComponent();
		_adminViewModel = adminViewModel;
		this.BindingContext = _adminViewModel;
        //Appearing += MyPage_AppearingAsync;
    }
    bool init = false;
    private void MyPage_AppearingAsync(object sender, EventArgs e)
    {
        if (init)
        {
            _adminViewModel.GetData();
            collectionUsers.SelectedItem = new User();
            //_viewModel.SelectedChrono = _selected;
        }
        else
        {
            init = true;
        }
    }
    private bool initSelect = true;
    private async void collectionUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (initSelect)
        {
            await this.ShowPopupAsync(new UserDetailPopUp(_adminViewModel));
            initSelect = false;
            collectionUsers.SelectedItem = new User();
            _adminViewModel.GetData();
        }
        else
        {
            initSelect = true;
        }
    }
}