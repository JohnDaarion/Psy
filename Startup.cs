using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseConnectionProvider
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly ConfigurationHandler _configurationHandler;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            
            _configurationHandler = new ConfigurationHandler(_configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            _configurationHandler.ConfigureDataBaseSettings(services);      
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
