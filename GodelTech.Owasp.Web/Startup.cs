using System;
using Audit.Core;
using GodelTech.Owasp.Web.Helpers;
using GodelTech.Owasp.Web.Repositories.Implementations;
using GodelTech.Owasp.Web.Repositories.Interfaces;
using GodelTech.Owasp.Web.Services.Implementations;
using GodelTech.Owasp.Web.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using GodelTech.Owasp.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace GodelTech.Owasp.Web
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
            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            
            services.AddDbContext<OwaspContext>(options => options.UseSqlServer(Configuration.GetConnectionString("owasp")));
            // services.AddDbContext<OwaspContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            
            // Register the Swagger generator, defining 1 or more Swagger documents
            // and configure it
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Godel Technologies. OWASP",
                        Description = "OWASP Top 10 (Basics) Course",
                        Version = "v1"
                    });
                options.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = @"JWT Authorization header using the Bearer scheme. <br>
                            Enter 'Bearer' [space] and then your token in the text input below.
                            <br>Example: 'Bearer 12345abcdef'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IAlbumRepository, AlbumRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddScoped<IUserService, UserService>();

            services.AddSingleton<ICryptographicService, CryptographicService>();
            
            // services.AddControllers();
            services.AddControllersWithViews();
        }

        /// <summary>
        /// A6 - Security Misconfiguration - This method can accidentally introduce a security flaw. The env.IsDevelopment sets us to use
        /// Developer Exception page which will link stack trace information to the browser window.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // A10 - Logging & Auditing - adding log4net here to properly log out information.
            loggerFactory.AddLog4Net();

            // A6 - incorrect
            app.UseDeveloperExceptionPage();

            // A6 - correct
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            //     app.UseBrowserLink();
            // }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "OWASP Top 10 API");
                // To serve the Swagger UI at the app's root (http://localhost:<port>/),
                // set the RoutePrefix property to an empty string
                options.RoutePrefix = string.Empty;
            });
            
            // A10 - Logging & Audit - Setup Audit to use log4net.
            Audit.Core.Configuration.Setup()
                .UseLog4net();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
