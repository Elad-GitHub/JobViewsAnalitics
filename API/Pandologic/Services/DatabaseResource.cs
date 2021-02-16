using System;
using System.Data;
using System.Data.Common;
using Pandologic;

namespace Services
{
    public abstract class DatabaseResource
    {
        #region Dependencies

        private IDatabaseConnectionStringProvider m_connectionStringProvider;

        private IDatabaseConnectionFactory m_databaseConnectionFactory;

        // Gets or sets the connection string provider.
        public IDatabaseConnectionStringProvider ConnectionStringProvider
        {
            get { return m_connectionStringProvider; }
            set { m_connectionStringProvider = value; }
        }

        // Gets or sets the database connection factory.
        public IDatabaseConnectionFactory DatabaseConnectionFactory
        {
            get { return m_databaseConnectionFactory; }
            set { m_databaseConnectionFactory = value; }
        }

        #endregion

        #region Protected Methods

        // Creates and open connection to db
        protected virtual DbConnection CreateOpenConnection()
        {
            var connection = CreateConnection();

            return OpenConnection(connection);
        }

        protected abstract string GetDatabaseName();

        // Creates connection to db
        protected virtual DbConnection CreateConnection()
        {
            var databaseName = GetDatabaseName();

            var connectionString = ConnectionStringProvider.GetConnectionString(databaseName);

            var connection = DatabaseConnectionFactory.Create(connectionString);

            return connection;
        }

        protected virtual void ExecuteReader(string sql, CommandType commandType, Action<DbCommand> prepare, Action<DbDataReader> read)
        {
            Guard.ArgumentNotNull(read, "read");
            
            Execute(sql, commandType, command =>
            {
                if (prepare != null)
                {
                    prepare(command);
                }

                using (var reader = command.ExecuteReader(CommandBehavior.Default))
                {
                    read(reader);
                }
            });
        }

        protected virtual void ReadStoredProcedure(string spName, object parameters, params IDataReaderMapper[] readers)
        {
            var arguments = new DbCommandArguments(parameters);
            var complexReader = new CompositeReaderMapper(readers);
            ExecuteReader(spName, CommandType.StoredProcedure, arguments.AddCommandParameters, complexReader.Read);
        }

        protected virtual void Execute(string sql, CommandType commandType, Action<DbCommand> executeAction)
        {
            Guard.ArgumentNotNullOrEmptyString(sql, "sql");
            Guard.ArgumentNotNull(executeAction, "executeAction");

            using (var connection = CreateOpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = commandType;

                    executeAction(command);
                }
            }
        }

        #endregion

        #region Private Methods

        // Opens the connection.
        private DbConnection OpenConnection(DbConnection connection)
        {
            Guard.ArgumentNotNull(connection, "connection");

            connection.Open();

            return connection;
        }

        #endregion
    }
}
