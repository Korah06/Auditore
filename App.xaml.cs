using Auditore.Services.Interfaces;

namespace Auditore;

public partial class App : Application
{
	private readonly ITaskService _taskService;
	public App(ITaskService taskService)
	{
		InitializeComponent();
        _taskService = taskService;

		taskService.GetTasks("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY0MzdjOGFjNzM3ZWZjNDM3NjNkY2E1MSIsImVtYWlsIjoiYWRtaW5AYWRtaW4uY29tIiwicm9sIjoiYWRtaW4iLCJpYXQiOjE2ODE0Njg1MDQsImV4cCI6MTY4MTQ3MjEwNH0.sEWXo1A5pTQ29XFYF5QQNjhO_ZFeZvj6QswFigTobbI");
        MainPage = new AppShell();
	}
}
