using Auditore.Models;
using Auditore.Services.Interfaces;
using Auditore.ViewModels;
using CommunityToolkit.Maui.Views;

namespace Auditore.Views.Phone;

public partial class ProfilePhone : ContentPage
{
    private readonly ProfileViewModel _profileViewModel;
    private readonly IDiagnosticService _diagnosticService;
    public ProfilePhone
        (ITaskService taskService, ICategoryService categoryService,
        IChronoService chronoService, IUserService userService, IDiagnosticService diagnosticService)
    {
        _diagnosticService = diagnosticService;
        _profileViewModel =
            new ProfileViewModel(taskService, categoryService, chronoService, userService, diagnosticService);
        InitializeComponent();
        this.BindingContext = _profileViewModel;
        Appearing += ProfileDesktop_Appearing;
    }
    private bool init = true;
    private void ProfileDesktop_Appearing(object sender, EventArgs e)
    {
        if (!init)
        {
            _profileViewModel.GetClassifyInfo();
        }
        else { init = false; }

    }

    private Diagnostic d;
    private bool initSelect = true;
    private async void CollectionDiagnostics_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (initSelect)
        {
            d = e.CurrentSelection[0] as Diagnostic;
            if (Preferences.Default.Get("diagnosticId", "") != "")
            {
                Preferences.Default.Remove("diagnosticId");
            }
            Preferences.Default.Set("diagnosticId", d._id);
            await this.ShowPopupAsync(new DiagnosticPopUp(_diagnosticService));

            initSelect = false;
            CollectionDiagnostics.SelectedItem = null;
        }
        else
        {
            initSelect = true;
        }
    }
}