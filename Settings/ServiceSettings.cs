using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gorold.Common.Settings
{
    public class ServiceSettings
    {
        public string ServiceName { get; init; }
        public string MessageBroker { get; init; }
        public string KeyVaultName { get; init; }
    }
}