using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gorold.Common.HealthCheck;
using Gorold.Common.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

namespace Gorold.Common.HealthChecks
{
    public static class Extensions
    {
        public static IHealthChecksBuilder AddMongoDbCheck(this IHealthChecksBuilder healthChecksBuilder)
        {
            return healthChecksBuilder.Add(
                new HealthCheckRegistration(
                        "MongoDbCheck",
                        serviceProvider =>
                            {
                                var configuration = serviceProvider.GetService<IConfiguration>();
                                var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings))
                                                                .Get<MongoDbSettings>();
                                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
                                return new MongoDbHealthCheck(mongoClient);
                            },
                        HealthStatus.Unhealthy,
                        new[] { "ready" },
                        TimeSpan.FromSeconds(10)
                    )
                );
        }
    }
}