namespace Auditore.Views.Phone;
using Auditore.ViewModels;
using CommunityToolkit.Maui.Views;

public partial class ChronoPopUp : Popup
{
    private ChronosViewModel _viewModel;
    public ChronoPopUp(ChronosViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		this.BindingContext = _viewModel;
        stackRepeat.IsVisible = _viewModel.SelectedChrono.IsPomodoro;
	}
    private void CloseButton(object sender, EventArgs e)
    {
        Close(true);
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        while (_viewModel.Deleting)
        {
            await Task.Delay(50);
        }

        Close(true);
    }
}