﻿using Auditore.Views.Desktop;
using Auditore.Views.Phone;

namespace Auditore;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
#if ANDROID || IOS
        loginPage.ContentTemplate = new DataTemplate(typeof(LoginPhone));
#else
		loginPage.ContentTemplate= new DataTemplate(typeof(LoginDesktop));
#endif
    }
}
