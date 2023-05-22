using Auditore.ViewModels;

namespace Auditore.Views.Phone;

public partial class CalendarPhone : ContentPage
{
    private readonly CalendarViewModel _calendarViewModel;
    public CalendarPhone(CalendarViewModel calendarViewModel)
	{
        _calendarViewModel = calendarViewModel;
        InitializeComponent();
        this.BindingContext = _calendarViewModel;
        Appearing += AppearingFunc;
    }
    private async void AppearingFunc(object sender, EventArgs e)
    {
        await _calendarViewModel.GetData();
    }
}