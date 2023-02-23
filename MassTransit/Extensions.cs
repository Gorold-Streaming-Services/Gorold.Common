using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gorold.Common.Settings;
using GreenPipes.Configurators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gorold.Common.MassTransit
{
    public static class Extensions
    {
        private const string RabbitMq = "RABBITMQ";
        private const string ServiceBus = "SERVICEBUS";

        public static IServiceCollection AddMassTransitWithMessageBroker(
            this IServiceCollection services,
            IConfiguration config,
            Action<IRetryConfigurator> configureRetries = null)
        {
            var serviceSettings = config.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

            switch (serviceSettings.MessageBroker?.ToUpper())
            {
                case ServiceBus:
                    services.AddMassTransitWithAzureServiceBus(configureRetries);
                    break;
                case RabbitMq:
                default:
                    services.AddMassTransitWithRabbitMQ(configureRetries);
                    break;
            }
            return services;
        }
    }
}