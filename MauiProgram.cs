using Auditore.Services;
using Auditore.Services.Interfaces;
using Auditore.ViewModels;
using Auditore.Views.Desktop;
using Auditore.Views.Phone;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;

namespace Auditore;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseLocalNotification()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("Cairo-Regular.ttf", "Cairo");
				fonts.AddFont("Prompt-Medium.ttf", "Prompt");
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


		#region Services
		builder.Services.AddSingleton<ITaskService, TaskService>();
		builder.Services.AddSingleton<IDiagnosticService, DiagnosticService>();
		builder.Services.AddSingleton<Services.Interfaces.INotificationService, NotificationService>();
		builder.Services.AddSingleton<IChronoService, ChronoService>();
		builder.Services.AddSingleton<ICategoryService, CategoryService>();
		builder.Services.AddSingleton<IUserService, UserService>();
		builder.Services.AddSingleton<IHttpsClientHandlerService, HttpsClientHandlerService>();
		#endregion

		#region Views
		builder.Services.AddSingleton<LoginDesktop>();
		builder.Services.AddSingleton<RegisterDesktop>();
		builder.Services.AddTransient<TasksDesktop>();
		builder.Services.AddTransient<CreateTaskDesktop>();
		builder.Services.AddTransient<ChronoDesktop>();
		builder.Services.AddTransient<CreateChronoDesktop>();
		builder.Services.AddTransient<CalendarDesktop>();
		builder.Services.AddTransient<ProfileDesktop>();
		builder.Services.AddTransient<AdminDesktop>();
		builder.Services.AddTransient<DiagnosticDesktop>();

        builder.Services.AddSingleton<LoginPhone>();
        builder.Services.AddSingleton<RegisterPhone>();
        builder.Services.AddTransient<ProfilePhone>();
        builder.Services.AddTransient<AdminPhone>();
        builder.Services.AddTransient<TasksPhone>();
        builder.Services.AddTransient<ChronoPhone>();
        builder.Services.AddTransient<CreateTaskPhone>();
        builder.Services.AddTransient<CreateChronoPhone>();
        builder.Services.AddTransient<ChronoPopUp>();
        builder.Services.AddTransient<UserDetailPopUp>();
        builder.Services.AddTransient<CalendarPhone>();

        #endregion

        #region ViewModels
        builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddSingleton<ShellViewModel>();
		builder.Services.AddSingleton<RegisterViewModel>();
		builder.Services.AddSingleton<TasksViewModel>();
		builder.Services.AddSingleton<CreateTaskViewModel>();
		builder.Services.AddSingleton<ChronosViewModel>();
		builder.Services.AddSingleton<CreateChronoViewModel>();
		builder.Services.AddSingleton<CalendarViewModel>(); 
		builder.Services.AddSingleton<ProfileViewModel>(); 
		builder.Services.AddSingleton<AdminViewModel>(); 
		builder.Services.AddSingleton<DiagnosticViewModel>(); 
		#endregion

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
