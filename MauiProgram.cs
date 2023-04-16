using Auditore.Services;
using Auditore.Services.Interfaces;
using Auditore.ViewModels;
using Auditore.Views.Desktop;
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
				fonts.AddFont("Cairo-Regular.ttf", "Cairo");
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		//Services
        builder.Services.AddSingleton<ITaskService, TaskService>();
        builder.Services.AddSingleton<ICategoryService, CategoryService>();
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<IHttpsClientHandlerService, HttpsClientHandlerService>();

		//views
		builder.Services.AddSingleton<LoginDesktop>();
        builder.Services.AddSingleton<RegisterDesktop>();
		builder.Services.AddTransient<TasksDesktop>();
		builder.Services.AddTransient<CreateTaskDesktop>();

        //viewModels
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<RegisterViewModel>();
        builder.Services.AddSingleton<TasksViewModel>();
        builder.Services.AddSingleton<CreateTaskViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
