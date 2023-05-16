using Auditore.Models;
using Auditore.Services.Interfaces;
using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class ChronoDesktop : ContentPage
{
    private readonly ITaskService _taskService;
    private readonly ICategoryService _categoryService;
    private readonly IChronoService _chronoService;
    private readonly INotificationService _notificationService;
    private readonly IDiagnosticService _diagnosticService;
    private ChronosViewModel _viewModel;

    private Chrono _selected;
    public ChronoDesktop
        (ITaskService taskService, ICategoryService categoryService, 
        IChronoService chronoService, INotificationService notificationService,
        IDiagnosticService diagnosticService)
	{   
        _taskService = taskService;
        _categoryService = categoryService;
        _chronoService = chronoService;
        _notificationService = notificationService;
        _diagnosticService = diagnosticService;
        _viewModel = 
            new ChronosViewModel(_chronoService, _taskService, _categoryService,_notificationService, _diagnosticService);
		InitializeComponent();
        this.BindingContext = _viewModel;
        Appearing += MyPage_AppearingAsync;
    }

    bool init = false;
    private void MyPage_AppearingAsync(object sender, EventArgs e)
    {
        if (init)
        {
            _viewModel.OnRefresh();
            //_viewModel.SelectedChrono = _selected;
        }
        else
        {
            init = true;
        }
    }

    private void ChronoCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_viewModel.SelectedChrono != null)
        {
            chronoFounded.IsVisible = false;
            contentGrid.IsVisible = true;

            if (_viewModel.SelectedChrono.IsPomodoro)
            {stackRepeat.IsVisible = true;}
            else{stackRepeat.IsVisible = false;}

            lblName.SetBinding(Label.TextProperty, new Binding("SelectedChrono.name"));
            lblCrono.SetBinding(Label.TextProperty, new Binding("ShowTime"));
            //_viewModel.getTasksForChrono();
            //TaskCollection.SetBinding(CollectionView.ItemsSourceProperty, "Tasks");
            _selected = _viewModel.SelectedChrono;
        }
    }
}