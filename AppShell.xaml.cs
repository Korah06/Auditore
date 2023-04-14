using Auditore.Views.Desktop;

namespace Auditore;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
#if ANDROID || IOS
#else
		loginPage.ContentTemplate= new DataTemplate(typeof(LoginDesktop));
#endif
    }
}
