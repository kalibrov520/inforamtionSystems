﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Camunda.Worker;
using Camunda.Worker.Extensions;
using FileLoader;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TalendService.Handlers;

namespace TalendService
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
            services.AddSingleton<IFileManager, FileSystemFileManager>();

            services.Configure<ApiSettings>(Configuration.GetSection(nameof(ApiSettings)));
            services.AddSingleton<IApiSettings>(sp => sp.GetRequiredService<IOptions<ApiSettings>>().Value);

            services.AddCamundaWorker(options =>
            {
                options.WorkerId = "talend";
                options.WorkerCount = 1;
                options.BaseUri = new Uri(Configuration.GetSection("CamundaApi").Value);
            }).AddHandler<TalendTransformationHandler>();
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
            else
            {
                app.UseHsts();
            }

        }
    }
}
