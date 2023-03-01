using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Gorold.Common.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Gorold.Common.Configurations
{
    public static class Extensions
    {
        public static IHostBuilder ConfigureAzureKeyVault(this IHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                //we only want to use Azure Key Vault when we are running in production
                if (context.HostingEnvironment.IsProduction())
                {
                    var configuration = configurationBuilder.Build();
                    var serviceSettings = configuration
                                                                .GetSection(nameof(ServiceSettings))
                                                                .Get<ServiceSettings>();
                    configurationBuilder.AddAzureKeyVault(
                        new Uri($"https://{serviceSettings.KeyVaultName}.vault.azure.net/"),
                        //Azure.Identity will fill the best way to fill the credentials based on the environment it's based
                        //in production, it will use the kubernetes context to fill in those credentials
                        new DefaultAzureCredential()
                    );
                }
            });
            return builder;
        }
    }
}