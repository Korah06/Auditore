using Auditore.Models;
using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class TasksDesktop : ContentPage
{
	private readonly ITaskService _taskService;
	private readonly ICategoryService _categoryService;
	private TasksViewModel _viewModel;
	public TasksDesktop(ITaskService taskService, ICategoryService categoryService)
	{
		InitializeComponent();
        _taskService = taskService;
		_categoryService = categoryService;
		_viewModel = new TasksViewModel(_taskService, _categoryService);
        this.BindingContext = _viewModel;
	}

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		taskFounded.IsVisible = false;
		ContentGrid.IsVisible = true;

		TaskName.Text = _viewModel.ItemSelected.Name;
		TaskDescription.Text = _viewModel.ItemSelected.Description;
		EndDate.Date = _viewModel.ItemSelected.EndDate;
		StartDate.Date = _viewModel.ItemSelected.StartDate;
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
}