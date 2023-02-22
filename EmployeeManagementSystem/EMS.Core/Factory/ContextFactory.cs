using System;
using System.Data.SqlClient;
using EMS.Core.Configuration;
using EMS.Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EMS.Core.Factory
{
    /// <summary>
    /// context factory for ef
    /// </summary>
    public class ContextFactory : IContextFactory
    {
        private readonly IOptions<ConnectionSettings> _connectionOptions;

        public ContextFactory(IOptions<ConnectionSettings> connectionOptions)
        {
            _connectionOptions = connectionOptions;
        }

        public IDatabaseContext DbContext => new DatabaseContext(GetDataContext().Options);

        private DbContextOptionsBuilder<DatabaseContext> GetDataContext()
        {
            //ValidateDefaultConnection();

            //var sqlConnectionBuilder = new SqlConnectionStringBuilder(_connectionOptions.Value.DefaultConnection);

            var contextOptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            contextOptionsBuilder.UseInMemoryDatabase("EmployeeDB");

            return contextOptionsBuilder;
        }

        private void ValidateDefaultConnection()
        {
            if (string.IsNullOrEmpty(_connectionOptions.Value.DefaultConnection))
            {
                throw new ArgumentNullException(nameof(_connectionOptions.Value));
            }
        }
    }
}
