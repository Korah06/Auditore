using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Phone;

public partial class CreateTaskPhone : ContentPage
{
    private readonly ITaskService _taskService;
    private readonly ICategoryService _categoryService;
    private CreateTaskViewModel _taskViewModel;
    public CreateTaskPhone(ITaskService taskService, ICategoryService categoryService)
    {
        _categoryService = categoryService;
        _taskService = taskService;
        InitializeComponent();
        _taskViewModel = new CreateTaskViewModel(_categoryService, _taskService);
        this.BindingContext = _taskViewModel;
    }

    private void endPicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        startPicker.Date = endPicker.Date;
    }

    private void startPicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (startPicker.Date > endPicker.Date)
        {
            startPicker.Date = endPicker.Date;
        }
    }
}