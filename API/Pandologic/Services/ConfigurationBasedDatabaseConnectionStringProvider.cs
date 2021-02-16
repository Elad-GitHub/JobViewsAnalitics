using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;

namespace Services
{
    public class ConfigurationBasedDatabaseConnectionStringProvider : IDatabaseConnectionStringProvider
    {
        public DatabaseConnectionString GetConnectionString(string databaseLogicalName)
        {
            var connectionStringSettings = new ConnectionStringSettings()
            {
                ConnectionString = @"Data Source=LAPTOP-FMJUMSNP\ELAD;Initial Catalog=PandologicDataBase;Integrated Security=True;",
                ProviderName = "System.Data.SqlClient"
            };

            //string conString = ConfigurationExtensions.GetConnectionString(Configuration, databaseLogicalName);

            if (connectionStringSettings == null)
            {
                throw new ApplicationException();
            }

            var result = new DatabaseConnectionString
            {
                ConnectionString = connectionStringSettings.ConnectionString,
                Provider = connectionStringSettings.ProviderName
            };

            return result;
        }
    }
}
