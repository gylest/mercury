using System.Windows;
using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration; // For ConfigurationBuilder, if not using Host.CreateDefaultBuilder
using Microsoft.Extensions.Options; // For IOptions<T>

namespace DataMaintenanceEnhanced
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            // 1. Create the Host Builder
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    // Host.CreateDefaultBuilder already loads appsettings.json,
                    // but you can customize or add environment-specific files here.
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                })
                .ConfigureServices((context, services) =>
                {
                    // 2. Register strongly-typed configuration objects
                    // The Configure<T> method binds the JSON section to the C# class.
                    services.Configure<AppSettings>(context.Configuration.GetSection("AppSettings"));

                    // 3. Register your WPF Windows and ViewModels for DI
                    services.AddTransient<MainWindow>();
                    // services.AddTransient<AnotherWindow>();
                    // services.AddTransient<SomeViewModel>();

                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            // 4. Resolve the starting window from the service provider and show it
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
}
