﻿using Auditore.Views.Desktop;
using Auditore.Views.Phone;

namespace Auditore;

public partial class AppShell : Shell
{
	public AppShell()
	{
		
		InitializeComponent();

		Routing.RegisterRoute("RegisterDesktop", typeof(RegisterDesktop));
		Routing.RegisterRoute("CreateTaskDesktop", typeof(CreateTaskDesktop));
		Routing.RegisterRoute("CreateChronoDesktop", typeof(CreateChronoDesktop));
#if ANDROID || IOS
        loginPage.ContentTemplate = new DataTemplate(typeof(LoginPhone));
#else
		loginPage.ContentTemplate= new DataTemplate(typeof(LoginDesktop));
		tasksPage.ContentTemplate = new DataTemplate(typeof(TasksDesktop));
		chronoPage.ContentTemplate = new DataTemplate(typeof(ChronoDesktop));
#endif
	}
}
