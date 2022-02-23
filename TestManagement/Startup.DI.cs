using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace TestManagement
{
    public partial class Startup
    {
        public void ConfigureDIService(IServiceCollection services)
        {
            services.AddScoped(typeof(TestManagement.Core.Dapper.Interfaces.IUnitOfWork), typeof(TestManagement.Core.Dapper.Repository.UnitOfWork));
            services.AddScoped(typeof(TestManagement.Core.Dapper.Interfaces.IDapperRepository<>),
                typeof(TestManagement.Core.Dapper.Repository.DapperRepository<>));

            services.AddScoped<IDbConnection>(db =>
            {
                var connectionString = Configuration.GetConnectionString("Default");
                var connection = new SqlConnection(connectionString);
                return connection;
            });
        }
    }
}
