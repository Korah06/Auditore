using Auditore.Models;
using Auditore.ViewModels;
using CommunityToolkit.Maui.Views;

namespace Auditore.Views.Phone;

public partial class TaskInfoPopUp : Popup
{
    private TasksViewModel _viewModel;
    public TaskInfoPopUp(TasksViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = _viewModel;
	}

    private void CloseButton(object sender, EventArgs e)
    {
        Close(true);
    }
}