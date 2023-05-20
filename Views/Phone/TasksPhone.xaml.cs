using Auditore.Services.Interfaces;
using Auditore.ViewModels;
using CommunityToolkit.Maui.Views;

namespace Auditore.Views.Phone;

public partial class TasksPhone : ContentPage
{
    private readonly ITaskService _taskService;
    private readonly ICategoryService _categoryService;
    private TasksViewModel _viewModel;
    public TasksPhone(ITaskService taskService, ICategoryService categoryService)
	{
		InitializeComponent();
        _taskService = taskService;
        _categoryService = categoryService;
        _viewModel = new TasksViewModel(_taskService, _categoryService);
        this.BindingContext = _viewModel;
        Appearing += MyPage_AppearingAsync;
    }

    bool init = false;
    private void MyPage_AppearingAsync(object sender, EventArgs e)
    {
        if (init)
        {
            _viewModel.OnRefresh();

        }
        else
        {
            init = true;
        }
    }
    private bool initSelect = true;
    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (initSelect)
        {
            await this.ShowPopupAsync(new TaskInfoPopUp(_viewModel));
            initSelect = false;
            TaskCollection.SelectedItem = null;
        }
        else
        {
            initSelect = true;
        }
    }
}