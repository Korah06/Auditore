using Auditore.Services;
using Auditore.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Auditore;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		//Services
        builder.Services.AddSingleton<ITaskService, TaskService>();
        builder.Services.AddSingleton<IHttpsClientHandlerService, HttpsClientHandlerService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
