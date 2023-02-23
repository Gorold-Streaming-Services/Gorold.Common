using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Gorold.Common.Settings;
using GreenPipes;
using GreenPipes.Configurators;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gorold.Common.MassTransit
{
    public static class AzureServiceBusExtensions
    {
        public static IServiceCollection AddMassTransitWithAzureServiceBus(
            this IServiceCollection services,
            Action<IRetryConfigurator> configureRetries = null)
        {
            services.AddMassTransit(configure =>
            {
                configure.AddConsumers(Assembly.GetEntryAssembly());
                configure.UsingAzureServiceBus((context, configurator) =>
                {
                    var configuration = context.GetService<IConfiguration>();
                    var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                    var rabbitMQSettings = configuration.GetSection(nameof(ServiceBusSettings)).Get<ServiceBusSettings>();
                    configurator.Host(rabbitMQSettings.ConnectionString);
                    configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, false));

                    if (configureRetries == null)
                    {
                        configureRetries = (retryConfigurator) => retryConfigurator.Interval(3, TimeSpan.FromSeconds(5));
                    }

                    configurator.UseMessageRetry(configureRetries);
                });
            });

            services.AddMassTransitHostedService();
            return services;
        }
    }
}