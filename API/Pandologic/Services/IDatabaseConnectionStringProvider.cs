namespace Services
{
    public interface IDatabaseConnectionStringProvider
    {
        DatabaseConnectionString GetConnectionString(string databaseLogicalName);
    }
}
