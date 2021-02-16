using System.Data.Common;

namespace Services
{
    public interface IDatabaseConnectionFactory
    {
        DbConnection Create(DatabaseConnectionString connectionString);

        DbDataAdapter CreateDataAdapter(DatabaseConnectionString connectionString);
    }
}
