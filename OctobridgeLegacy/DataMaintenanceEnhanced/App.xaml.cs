namespace DataMaintenanceEnhanced;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        // Create the Host Builder
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Register AppSettings with DI
                services.Configure<AppSettings>(context.Configuration.GetSection("AppSettings"));

                // Register MainForm with DI
                services.AddTransient<MainWindow>();
                // services.AddTransient<AnotherWindow>();
                // services.AddTransient<SomeViewModel>();

            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();

        // Resolve the starting window from the service provider and show it
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        using (_host)
        {
            await _host.StopAsync(TimeSpan.FromSeconds(5));
        }
        base.OnExit(e);
    }
}
