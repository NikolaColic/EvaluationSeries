using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Series.Context;
using EvaluationSeries.Services.Series.Repository;
using EvaluationSeries.Services.Series.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EvaluationSeries.Services.Series
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
            services.AddScoped<ISeriesRepository, SeriesRepository>();
            services.AddScoped<IActorRepository, ActorRepository>();
            //services.AddHttpClient<IActorServices, ActorServices>(c =>
            //   c.BaseAddress = new Uri(Configuration["ApiConfigs:Actor:Uri"]));
            services.AddGrpcClient<ActorsGrpc.ActorsGrpcClient>(o => o.Address = new Uri(Configuration["ApiConfigs:Actor:Uri"]));

            services.AddDbContext<SeriesDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
