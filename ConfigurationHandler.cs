using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseConnectionProvider
{
    public class ConfigurationHandler
    {
        private const string MongoConnectionStringKey = "MongoConnection:ConnectionString";
        private const string MongoDatabaseKey = "MongoConnection:Database";
        
        private readonly IConfiguration _configuration;
        
        public ConfigurationHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureDataBaseSettings(IServiceCollection services)
        {
            services.Configure<MongoDbSettings>(options => {
                options.ConnectionString = _configuration.GetSection(MongoConnectionStringKey).Value;
                options.Database = _configuration.GetSection(MongoDatabaseKey).Value;
            });
        }
    }
}