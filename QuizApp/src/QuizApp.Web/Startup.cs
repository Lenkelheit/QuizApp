﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AutoMapper;

using QuizApp.Data.Context;
using QuizApp.Data.Interfaces;
using QuizApp.BLL.MappingProfiles;
using QuizApp.Web.Extensions;

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
            services.AddDbContext<QuizAppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("QuizAppConnection"));
            });

            services.AddAutoMapper(typeof(TestProfile).Assembly);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddCustomServices();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

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
