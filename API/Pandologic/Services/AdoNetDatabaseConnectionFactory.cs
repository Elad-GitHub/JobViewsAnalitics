using System.Data.Common;

namespace Services
{
    public class AdoNetDatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        // Creates the specified connection instance
        public DbConnection Create(DatabaseConnectionString connectionString)
        {
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);
            var factory = DbProviderFactories.GetFactory(connectionString.Provider);
            var connection = factory.CreateConnection();

            connection.ConnectionString = connectionString.ConnectionString;

            return connection;
        }

        // Creates the data adapter.
        public DbDataAdapter CreateDataAdapter(DatabaseConnectionString connectionString)
        {
            var factory = DbProviderFactories.GetFactory(connectionString.Provider);
            var dataAdapter = factory.CreateDataAdapter();
            return dataAdapter;
        }
    }
}
