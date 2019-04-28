using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using SimplCommerce.Infrastructure.Modules;
using SimplCommerce.Module.Tax.Services;
using TaxService = SimplCommerce.Module.GSTIndia.Services.TaxService;

namespace SimplCommerce.Module.GSTIndia
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITaxService, TaxService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
        }
    }
}
