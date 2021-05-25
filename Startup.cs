using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logzio.DotNet.NLog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Config;

namespace MyLogger
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

            services.AddControllers();

            // Inital config.
            var config = new LoggingConfiguration();

            // Set target config Logzio
            var logzioTarget = new LogzioTarget
            {
                Name = "Logzio",
                Token = "YbVtWAaeuPXKroaoJUKyihqwakZGCMAh",
                // Describe log type
                LogzioType = "nlog",
                // URL API
                ListenerUrl = "https://listener.logz.io:8071",
                // Message quantity before sending
                BufferSize = 100,
                // Message time sending
                BufferTimeout = TimeSpan.Parse("00:00:05"),
                RetriesMaxAttempts = 3,
                RetriesInterval = TimeSpan.Parse("00:00:02"),
                Debug = false,
            };

            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logzioTarget);

            LogManager.Configuration = config;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyLogger", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyLogger v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
