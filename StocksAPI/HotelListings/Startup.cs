using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Services.Configurations;
using Services.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Services.IRepository;
using Services.Repository;
using Services.Services;
using Services.Interfaces.Repository;
using Services.Repository.GetStockDataRepository;
using Services.Interfaces.Services;
using Services.Services.GetStockDataService;

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

            services.AddCors(cors =>
            {
                cors.AddPolicy("corsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // Adds autoMapper to list of services - use the MapperInitialiser class setup in Configuration file
            services.AddAutoMapper(typeof(MapperInitialiser));

            // AddScoped - new instance is created for period / lifetime of certain set of requests
            // AddSingleton - one instance of the service is provided across application
            // AddTransient - everytime its needed, a new instance is created

            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IAuthManager, AuthManager>();

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IAuthManager), typeof(AuthManager));
            services.AddScoped(typeof(IStocksRepository), typeof(GetStockDataRepository));
            services.AddScoped(typeof(IStocksService), typeof(GetStockDataService));

            services.AddMediatR(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StocksAPI", Version = "v1" });
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

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelListings v1"));

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseCors("corsPolicy");

            app.UseRouting();

            // functions required for caching

            app.UseResponseCaching();
            app.UseHttpCacheHeaders();

            // function required for rate limit

            app.UseIpRateLimiting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}