using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Demo.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Demo.Models;
using Demo.Services;
using Newtonsoft.Json.Serialization;
using Marvin.Cache.Headers;
using Microsoft.OpenApi.Models;

namespace Demo
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
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Demo",
                    Description = "v1版本的Api"
                });
            }) ;
            services.AddHttpCacheHeaders(expris=>
                {
                    expris.MaxAge = 60;
                    expris.CacheLocation = CacheLocation.Private;
                }, validation =>
                {
                    validation.MustRevalidate = true;
                }
                );
            services.AddControllers().AddNewtonsoftJson(setup =>
            {
                setup.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            }).AddXmlDataContractSerializerFormatters();
            services.AddScoped<ICompanyRepository,CompanyRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<DemoDbContext>(option=> { option.UseSqlServer(Configuration.GetConnectionString("SqlDB")); });
            services.AddDefaultIdentity<ApplicationUser>(
                options => { 
                options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 1;
                }
                ).AddEntityFrameworkStores<DemoDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //定义返回故障
                app.UseExceptionHandler(appBulider=> 
                {
                    appBulider.Run(async context =>
                  {
                      context.Response.StatusCode = 500;
                      await context.Response.WriteAsync("Unexpected Error!");
                  });
                });
            }
            app.UseSwagger();
            app.UseSwaggerUI(c=> {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","v1");
            });
            app.UseHttpCacheHeaders();
            app.UseHsts();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
