using IposTransferData.core.ProductService;
using IposTransferData.Core.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace IposTransferData
{
    public  class Startup
    {
        private ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, IHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public static IConfiguration Configuration { get; set; }
        private static IHostEnvironment HostingEnvironment { get; set; }

        public void ConfigureService(IServiceCollection Services)
        {
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);
            Services.AddScoped<IDataDapperService, DataDapperService>()
                .AddScoped<IProductService, ProductService>()
                .AddSingleton<IDbConnection>(db =>
                {
                    var connectionString = Configuration.GetConnectionString("OldConnection");
                    var connection = new SqlConnection(connectionString);
                    return connection;
                });
            //.AddTransient<IDataProvider, IposMssqlProvider>(
            //db =>
            //{
            //    var connectionString = Configuration.GetConnectionString("Default");
            //    var provider = new IposMssqlProvider(connectionString);
            //    return provider;
            //});
        }
    }
}