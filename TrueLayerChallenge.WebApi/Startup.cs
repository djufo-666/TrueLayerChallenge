// # define SIMULATED

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
using TrueLayerChallenge.ExternalServices;
using TrueLayerChallenge.Services;

namespace TrueLayerChallenge.WebApi
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
            services.AddLogging();

            services.AddSingleton<Filters.ApiExceptionFilter>();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(Filters.ApiExceptionFilter));
            });

            services.AddTransient<IHttpClientService, HttpClientService>();

#if !SIMULATED
            services.AddTransient<IFunTranslationsService, FunTranslationsService>();
            services.AddTransient<IPokeApiService, PokeApiService>();
#else
            services.AddTransient<IFunTranslationsService, FunTranslationsServiceSimulated>();
            services.AddTransient<IPokeApiService, PokeApiServiceSimulated>();
#endif
            services.AddTransient<ITranslationStrategy, TranslationStrategy>();
            services.AddTransient<IPokemonApiService, PokemonApiService>();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy", builder =>
            //    {
            //        builder
            //        .AllowAnyOrigin()
            //        .WithMethods("GET")
            //        .AllowAnyHeader()
            //        .AllowCredentials()
            //        ;
            //    });
            //});

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TrueLayerChallenge", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrueLayerChallenge v1"));
            }

            app.UseRouting();

            // app.UseCors("CorsPolicy");

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
