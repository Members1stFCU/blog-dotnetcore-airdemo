using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirDemo.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using AirDemo.Service;

namespace AirDemo.Api
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
            services.AddDbContext<AirplaneContext>(opt => opt.UseInMemoryDatabase("HelloWorld"));
            services.AddScoped<IAirplaneService, AirplaneService>();
            
            // AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AirplaneServiceMappingProfile>();
            });
            services.AddSingleton<MapperConfiguration>(config);

            services.AddMvc();
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
