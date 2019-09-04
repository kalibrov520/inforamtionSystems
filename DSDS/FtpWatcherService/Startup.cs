using System;
using Camunda.Worker;
using Camunda.Worker.Extensions;
using FileLoader;
using FileLoader.File;
using FtpWatcherService.Handlers;
using FtpWatcherService.Models;
using FtpWatcherService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FtpWatcher
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<PoCDatabaseSettings>(
                Configuration.GetSection(nameof(PoCDatabaseSettings)));

            services.AddSingleton<IPoCDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<PoCDatabaseSettings>>().Value);

            services.AddSingleton<BatFileService>();
            services.AddSingleton<IFileLoader, FromFileLoader>();
            services.AddSingleton<IFileWriter, FileWriter>();

            services.AddCamundaWorker(options =>
                {
                    options.WorkerId = "sampleWorker";
                    options.WorkerCount = 1;
                    options.BaseUri = new Uri("http://localhost:8080/engine-rest");
                })
                .AddHandler<FtpWatcherHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var host = new WebHostBuilder()
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            app.UseMvc();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}