﻿using Auditore.Views.Desktop;
using Auditore.Views.Phone;

namespace Auditore;

public partial class AppShell : Shell
{
	public AppShell()
	{
		
		InitializeComponent();

		Routing.RegisterRoute("RegisterDesktop", typeof(RegisterDesktop));
		Routing.RegisterRoute("RegisterPhone", typeof(RegisterPhone));
		Routing.RegisterRoute("CreateTaskPhone", typeof(CreateTaskPhone));

		Routing.RegisterRoute("CreateTaskDesktop", typeof(CreateTaskDesktop));
		Routing.RegisterRoute("CreateChronoDesktop", typeof(CreateChronoDesktop));
		Routing.RegisterRoute("DiagnosticDesktop", typeof(DiagnosticDesktop));
#if ANDROID || IOS
        loginPage.ContentTemplate = new DataTemplate(typeof(LoginPhone));
        tasksPage.ContentTemplate = new DataTemplate(typeof(TasksPhone));
#else
		loginPage.ContentTemplate= new DataTemplate(typeof(LoginDesktop));
		tasksPage.ContentTemplate = new DataTemplate(typeof(TasksDesktop));
		chronoPage.ContentTemplate = new DataTemplate(typeof(ChronoDesktop));
		calendarPage.ContentTemplate = new DataTemplate(typeof(CalendarDesktop));
		profilePage.ContentTemplate = new DataTemplate(typeof(ProfileDesktop));
		adminPage.ContentTemplate = new DataTemplate(typeof(AdminDesktop));
#endif
	}
}
