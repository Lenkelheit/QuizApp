using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using FluentValidation.AspNetCore;

using QuizApp.Data.Context;
using QuizApp.Data.Interfaces;
using QuizApp.BLL.MappingProfiles;
using QuizApp.Web.Extensions;
using QuizApp.Web.Validators.Test;
using QuizApp.BLL.Settings;

namespace QuizApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TimeErrorSetting>(Configuration.GetSection(nameof(TimeErrorSetting)));
            services.Configure<UserLogin>(Configuration.GetSection(nameof(UserLogin)));

            services.AddDbContext<QuizAppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("QuizAppConnection"));
            });

            services.AddAutoMapper(typeof(TestProfile).Assembly);
            services.AddScoped<DbContext, QuizAppDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddCustomServices();

            services.AddCors();

            services.AddCustomAuthentication(Configuration);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining(typeof(NewTestDtoValidator)));

            services.ConfigureCustomValidationErrors();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("http://localhost:4200"));

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
