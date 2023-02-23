using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gorold.Common.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; init; }

        public int Port { get; init; }

        private string connectionString;

        public string ConnectionString
        {
            get
            {
                return string.IsNullOrWhiteSpace(connectionString)
                    ? $"mongodb://{Host}:{Port}" : connectionString;
            }
            init { connectionString = value; }
        }
    }
}