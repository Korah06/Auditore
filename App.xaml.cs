using Auditore.Services.Interfaces;

namespace Auditore;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        MainPage = new AppShell();
	}
}
