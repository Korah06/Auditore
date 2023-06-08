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
        SelectedCat();
    }

    private void CloseButton(object sender, EventArgs e)
    {
        Close(true);
    }

    private void SelectedCat()
    {
        Category cat;
        foreach (Category item in _viewModel.Categories)
        {
            if (item._id == _viewModel.ItemSelected.CategoryId)
            {
                cat = item;
                TaskCategory.SelectedItem = cat;
            }
        }
    }

    private void StartDate_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (StartDate.Date > EndDate.Date)
        {
            StartDate.Date = EndDate.Date;
        }
    }
}