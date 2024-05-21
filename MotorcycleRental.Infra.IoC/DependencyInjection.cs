using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Mappings;
using MotorcycleRental.Infra.CrossCutting.ExtensionMethods;
using MotorcycleRental.Infra.Data.Connection;
using MotorcycleRental.Infra.Data.Repositories;
using MotorcycleRental.Service.Service;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;


namespace MotorcycleRental.Infra.IoC
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMotoService, MotoService>();
            services.AddScoped<IMotoRepository, MotoRepository>();
            services.AddScoped<ILocacaoRepository, LocacaoRepository>();
            services.AddScoped<ILocacaoService, LocacaoService>();
            services.AddScoped<IEntregadorRepository, EntregadorRepository>();
            services.AddScoped<IEntregadorService, EntregadorService>();
            services.AddAutoMapper(typeof(DtoMappingToProfileDomain));
            services.AddAutoMapper(typeof(DomainMappingToProfileDto));
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<IQueueService, QueueService>();
            services.AddScoped<IProducer, Producer>();
            services.AddScoped<IContratoService, ContratoService>();
            services.AddScoped<IContratoRepository, ContratoRepository>();
            services.AddScoped<IPlanosService, PlanosService>();
            services.AddScoped<IPlanosRepository, PlanosRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<ITokenService, TokenService>();
            ConfigureLogging();

            ExatractConfiguration.Initialize(configuration);
        }

        public static void ConfigureLogging()
        {
            var environment = "Development";
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings{environment}.json", optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSkin(configuration, environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        static ElasticsearchSinkOptions ConfigureElasticSkin(IConfigurationRoot configuration, string environment)
        {
            var teste = configuration["ElasticConfiguration:Uri"];

            var elasticsearchSinkOptions = new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = configuration["ElasticConfiguration:Index"],
                NumberOfReplicas = 1,
                NumberOfShards = 2
            };

            return elasticsearchSinkOptions;
        }

    }
}
