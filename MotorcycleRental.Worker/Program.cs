using Microsoft.Extensions.DependencyInjection;
using MotorcycleRental.Infra.CrossCutting.ExtensionMethods;
using Microsoft.Extensions.Configuration;
using MotorcycleRental.Worker;
using Microsoft.Extensions.Hosting;
using MotorcycleRental.Infra.IoC;
using Serilog;


var services = new ServiceCollection();
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
    .Build();

ExatractConfiguration.Initialize(configuration);

IHost host = Host.CreateDefaultBuilder()
    .UseSerilog()
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddInfrastructure(configuration);
    })
    .Build();

await host.RunAsync();


