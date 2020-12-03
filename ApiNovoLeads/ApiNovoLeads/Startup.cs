using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiNovoLeads.AutoMapper;
using ApiPlafonesWeb.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;

namespace ApiNovoLeads
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

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddAutoMapper(typeof(AutoMappers));


            /*Documentation*/
          

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("ApiContactos", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Api Novo Leads",
                    Version = "v1",

                });

<<<<<<< HEAD
                options.SwaggerDoc("ApiSeguimientos", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Seguimientos",
=======
                //File Comments Documentation
                //var FileCommentsDocumentation = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var PathFileCommentsDocumentation = Path.Combine(AppContext.BaseDirectory, "ApiNovoLeadsDocumentation.xml");
                options.IncludeXmlComments(PathFileCommentsDocumentation);

            });


            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("ApiTiposDeSeguimiento", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Api Novo Leads",
>>>>>>> aa5f06cdeaf1b5889b30bff6415f74cb30805667
                    Version = "v1",

                });


                //File Comments Documentation
                //var FileCommentsDocumentation = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var PathFileCommentsDocumentation = Path.Combine(AppContext.BaseDirectory, "ApiNovoLeadsDocumentation.xml");
                options.IncludeXmlComments(PathFileCommentsDocumentation);

            });


            /*End Documentation*/


            services.AddCors();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                /*Soporte para CORS*/
                app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                IdentityModelEventSource.ShowPII = true;
            }
            else
            {
                /*Exceptions in production*/
                app.UseExceptionHandler(a => a.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;

                    var result = JsonConvert.SerializeObject(new { error = exception.Message });
                    context.Response.ContentType = "application/json";

                    var response = new Response();
                    await context.Response.WriteAsync((string)response.ResponseValues(context.Response.StatusCode, null, result.ToString()));
                }));
            }

            /*Documentation*/
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
<<<<<<< HEAD
                options.SwaggerEndpoint("/swagger/ApiContactos/swagger.json", "Api Novo Leads");
                options.SwaggerEndpoint("/swagger/ApiSeguimientos/swagger.json", "Seguimientos");
=======
                options.SwaggerEndpoint("/swagger/ApiContactos/swagger.json", "Api Contactos");
                options.SwaggerEndpoint("/swagger/ApiTiposDeSeguimiento/swagger.json", "Api Tipos De Seguimiento");
>>>>>>> aa5f06cdeaf1b5889b30bff6415f74cb30805667
                options.RoutePrefix = "";
            });
            /*End Documentation*/


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            /*Soporte para CORS*/
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        }
    }
}
