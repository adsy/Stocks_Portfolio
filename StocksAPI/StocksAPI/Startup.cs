using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AspNetCoreRateLimit;
using Services.Configurations;
using Services.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Services.IRepository;
using Services.Repository;
using Services.Interfaces.Repository;
using Services.Repository.GetStockDataRepository;
using Services.Interfaces.Services;
using Services.Services.GetStockDataService;
using Services.Services.CryptoService;
using Services.Repository.CryptoRepository;
using Services.Repository.PortfolioRepository;
using Services.Services.PortfolioService;
using Microsoft.Net.Http.Headers;
using Services.Interfaces;
using Services.Services.UtilityServices;
using Services.Services.SoldInstrumentsService;
using Services.Repository.SoldInstrumentsRepository;
using Domain.Config;

namespace StockAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Sets up connection to SQL Server db based on the sqlConnection string
            services.AddDbContext<StockDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("StockAPI"));
            });

            // functions required for rate limiting
            services.AddMemoryCache(); // stores and keeps track of who requested what, how many times
            services.ConfigureRateLimiting();
            services.AddHttpContextAccessor();

            services.ConfigureHttpCacheHeaders();

            services.AddAuthentication();

            services.ConfigureIdentity();

            services.ConfigureJWT(Configuration);

            services.AddCors(options =>
              options.AddPolicy("Dev", builder =>
              {
                  // Allow multiple methods
                  builder.WithMethods("GET", "POST", "PATCH", "DELETE", "OPTIONS")
                    .WithHeaders(
                      HeaderNames.Accept,
                      HeaderNames.ContentType,
                      HeaderNames.Authorization)
                    .AllowCredentials()
                    .SetIsOriginAllowed(origin =>
                    {
                        if (string.IsNullOrWhiteSpace(origin)) return false;
                        // Only add this to allow testing with localhost, remove this line in production!
                        if (origin.ToLower().StartsWith("http://localhost")) return true;
                        // Insert your production domain here.
                        if (origin.ToLower().StartsWith("https://stockapi20210415184956.azurewebsites.net")) return true;
                        return false;
                    });
              })
            );

            // Adds autoMapper to list of services - use the MapperInitialiser class setup in Configuration file
            services.AddAutoMapper(typeof(MapperInitialiser));

            //services.Configure<YahooFinanceApiSettings>();

            var test = Configuration.GetSection(YahooFinanceApiSettings.ConfigKey).GetChildren();

            services.AddOptions<YahooFinanceApiSettings>().Bind(Configuration.GetSection(YahooFinanceApiSettings.ConfigKey));

            services.AddOptions<ExchangeRateStorageApiSettings>().Bind(Configuration.GetSection(ExchangeRateStorageApiSettings.ConfigKey));

            services.AddOptions<CoinMarketCapApiSettings>().Bind(Configuration.GetSection(CoinMarketCapApiSettings.ConfigKey));

            services.AddOptions<CoinGeckoApiSettings>().Bind(Configuration.GetSection(CoinGeckoApiSettings.ConfigKey));

            services.AddHttpClient<IStocksRepository, StockRepository>();
            services.AddHttpClient<ICryptoRepository, CryptoRepository>();

            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient(typeof(IAuthManager), typeof(AuthManager));
            services.AddTransient(typeof(IStocksRepository), typeof(StockRepository));
            services.AddTransient(typeof(IStocksService), typeof(StockService));
            services.AddTransient(typeof(ICryptoService), typeof(CryptoService));
            services.AddTransient(typeof(ITokenService), typeof(TokenService));
            services.AddTransient(typeof(ISoldInstrumentsService), typeof(SoldInstrumentsService));

            services.AddTransient(typeof(ICryptoRepository), typeof(CryptoRepository));
            services.AddTransient(typeof(IPortfolioService), typeof(PortfolioService));
            services.AddTransient(typeof(IPortfolioRepository), typeof(PortfolioRepository));
            services.AddTransient(typeof(ISoldInstrumentsRepository), typeof(SoldInstrumentsRepository));

            services.AddMediatR(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StocksAPI", Version = "v1" });
            });
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "stocksapi-fe/build";
            });
            services.AddControllers(config =>
            {
                config.CacheProfiles.Add("60SecondDuration", new CacheProfile
                {
                    Duration = 60
                });
            }).AddNewtonsoftJson(op =>
                op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.ConfigureVersioning();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StocksAPI v1"));

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseCors("Dev");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMvc();

            // functions required for caching

            app.UseResponseCaching();
            app.UseHttpCacheHeaders();

            // function required for rate limit

            app.UseIpRateLimiting();
        }
    }
}