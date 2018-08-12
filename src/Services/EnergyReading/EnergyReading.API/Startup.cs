using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergyReading.Core.EnergyReadingAggregate;
using EnergyReading.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EnergyReading.API.Application;

namespace EnergyReading.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        readonly IHostingEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var csvFilesFolder = Configuration.GetValue<string>("CsvFilesFolderPath");

            var webRoot = _env.ContentRootPath;
            var file = System.IO.Path.Combine(webRoot, csvFilesFolder);
            var repo = new CsvEnergyReadingsRepository(file);

            services.AddScoped<IEnergyReadingsRepository>(s => new CsvEnergyReadingsRepository(file));

            services.AddTransient<IEnergyReadingQueries, EnergyReadingQueries>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
