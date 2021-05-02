using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PortfolioTrackerFunction.Infrastructure.Interfaces;
using PortfolioTrackerFunction.Infrastructure.Repository;
using PortfolioTrackerFunction.Infrastructure.Services;

[assembly: FunctionsStartup(typeof(PortfolioTrackerFunction.Startup))]

namespace PortfolioTrackerFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IStocksApiService, StocksApiService>();
            builder.Services.AddSingleton<IStocksApiRepository, StocksApiRepository>();
        }
    }
}