using Auditore.Models;
using Auditore.ViewModels;
using System.Collections.ObjectModel;

namespace Auditore.Views.Desktop;

public partial class CalendarDesktop : ContentPage
{
	private readonly CalendarViewModel _calendarViewModel;
	public CalendarDesktop(CalendarViewModel calendarViewModel)
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