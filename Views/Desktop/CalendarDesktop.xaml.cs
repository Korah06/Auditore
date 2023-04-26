using Auditore.ViewModels;

namespace Auditore.Views.Desktop;

public partial class CalendarDesktop : ContentPage
{
	private readonly CalendarViewModel _calendarViewModel;
	public CalendarDesktop(CalendarViewModel calendarViewModel)
	{
		_calendarViewModel = calendarViewModel;
		InitializeComponent();
		this.BindingContext = _calendarViewModel;
	}
}