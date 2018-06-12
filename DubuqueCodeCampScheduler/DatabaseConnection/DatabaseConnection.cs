using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;

namespace DubuqueCodeCampScheduler
{
    public class DatabaseConnection : DataContext, IDbConnection
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DCCKellyDatabase"].ConnectionString;

        public void GetDatabaseConnection()
        {
            // Currently, this block of code does nothing, just reiterates most of what is already in the connection string
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "tcp:pltnm-dev-testing.database.windows.net,1433",
                UserID = "kstrandholm",
                Password = "",
                InitialCatalog = "DCC_Kelly",
                Encrypt = true,
                TrustServerCertificate = false,
                Authentication = SqlAuthenticationMethod.SqlPassword,
                PersistSecurityInfo = false,
                MultipleActiveResultSets = false,
            };

            var connection = new SqlConnection(_connectionString);
        }

        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="T:System.Data.Linq.DataContext" /> class by referencing a file source.</summary>
        /// <param name="fileOrServerOrConnection">This argument can be any one of the following:The name of a file where a SQL Server Express database resides.The name of a server where a database is present. In this case the provider uses the default database for a user.A complete connection string. LINQ to SQL just passes the string to the provider without modification.</param>
        public DatabaseConnection(string fileOrServerOrConnection) : base(fileOrServerOrConnection) { }

        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="T:System.Data.Linq.DataContext" /> class by referencing a file source and a mapping source.</summary>
        /// <param name="fileOrServerOrConnection">This argument can be any one of the following:The name of a file where a SQL Server Express database resides.The name of a server where a database is present. In this case the provider uses the default database for a user.A complete connection string. LINQ to SQL just passes the string to the provider without modification.</param>
        /// <param name="mapping">A source for mapping.</param>
        public DatabaseConnection(string fileOrServerOrConnection, MappingSource mapping) : base(fileOrServerOrConnection, mapping) { }

        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="T:System.Data.Linq.DataContext" /> class by referencing the connection used by the .NET Framework.</summary>
        /// <param name="connection">The connection used by the .NET Framework.</param>
        public DatabaseConnection(IDbConnection connection) : base(connection) { }

        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="T:System.Data.Linq.DataContext" /> class by referencing a connection and a mapping source.</summary>
        /// <param name="connection">The connection used by the .NET Framework.</param>
        /// <param name="mapping">A source for mapping.</param>
        public DatabaseConnection(IDbConnection connection, MappingSource mapping) : base(connection, mapping) { }

        /// <inheritdoc />
        /// <summary>Begins a database transaction.</summary>
        /// <returns>An object representing the new transaction.</returns>
        public IDbTransaction BeginTransaction()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        /// <summary>Begins a database transaction with the specified <see cref="T:System.Data.IsolationLevel" /> value.</summary>
        /// <param name="il">One of the <see cref="T:System.Data.IsolationLevel" /> values. </param>
        /// <returns>An object representing the new transaction.</returns>
        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        /// <summary>Closes the connection to the database.</summary>
        public void Close()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        /// <summary>Changes the current database for an open <see langword="Connection" /> object.</summary>
        /// <param name="databaseName">The name of the database to use in place of the current database. </param>
        public void ChangeDatabase(string databaseName)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        /// <summary>Creates and returns a Command object associated with the connection.</summary>
        /// <returns>A Command object associated with the connection.</returns>
        public IDbCommand CreateCommand()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        /// <summary>Opens a database connection with the settings specified by the <see langword="ConnectionString" /> property of the provider-specific Connection object.</summary>
        public void Open()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        /// <summary>Gets or sets the string used to open a database.</summary>
        /// <returns>A string containing connection settings.</returns>
        public string ConnectionString { get; set; }

        /// <summary>Gets the time to wait while trying to establish a connection before terminating the attempt and generating an error.</summary>
        /// <returns>The time (in seconds) to wait for a connection to open. The default value is 15 seconds.</returns>
        public int ConnectionTimeout { get; }

        /// <inheritdoc />
        /// <summary>Gets the name of the current database or the database to be used after a connection is opened.</summary>
        /// <returns>The name of the current database or the name of the database to be used once a connection is open. The default value is an empty string.</returns>
        public string Database => "DCC_Kelly";

        /// <summary>Gets the current state of the connection.</summary>
        /// <returns>One of the <see cref="T:System.Data.ConnectionState" /> values.</returns>
        public ConnectionState State { get; }
    }
}
