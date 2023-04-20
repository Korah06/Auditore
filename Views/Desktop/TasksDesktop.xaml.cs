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

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		if (_viewModel.ItemSelected!=null)
		{
            taskFounded.IsVisible = false;
            ContentGrid.IsVisible = true;


            TaskName.SetBinding(Entry.TextProperty, new Binding("ItemSelected.Name"));
            TaskDescription.SetBinding(Entry.TextProperty, new Binding("ItemSelected.Description"));
            EndDate.SetBinding(DatePicker.DateProperty, new Binding("ItemSelected.EndDate"));
            StartDate.SetBinding(DatePicker.DateProperty, new Binding("ItemSelected.StartDate"));


            if(VerticalDad.Children.Count > 7)
            {
                VerticalDad.Children.RemoveAt(VerticalDad.Children.Count - 1);
            }
                var buttonGrid = new Grid
                {

                    Margin = new Thickness(0, 10, 0, 0),
                    ColumnDefinitions = new ColumnDefinitionCollection
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    }
                };

                var updateButton = new Button
                {
                    Text = "Actualizar Tarea",
                    HorizontalOptions = LayoutOptions.Start
                };
                updateButton.SetBinding(Button.CommandProperty, new Binding("UpdateTask"));
                updateButton.Clicked += Button_Clicked_1;

                var deleteButton = new Button
                {
                    Text = "Eliminar Tarea",
                    HorizontalOptions = LayoutOptions.End,
                    BackgroundColor = Color.FromArgb("#6a040f")
                };
                deleteButton.SetBinding(Button.CommandProperty, new Binding("DeleteTask"));
                deleteButton.Clicked += Button_Clicked;

                Grid.SetColumn(updateButton, 0);
                Grid.SetColumn(deleteButton, 1);

                buttonGrid.Children.Add(updateButton);
                buttonGrid.Children.Add(deleteButton);

                // Agrega el grid al layout principal
                VerticalDad.Children.Add(buttonGrid);
            

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

    private void Button_Clicked(object sender, EventArgs e)
    {
        taskFounded.IsVisible = true;
        ContentGrid.IsVisible = false;
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {

    }
}