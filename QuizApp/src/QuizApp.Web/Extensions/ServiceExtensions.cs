using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using QuizApp.BLL.Interfaces;
using QuizApp.BLL.Services;
using AuthenticationService = QuizApp.BLL.Services.AuthenticationService;
using IAuthenticationService = QuizApp.BLL.Interfaces.IAuthenticationService;

namespace QuizApp.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IResultAnswerOptionService, ResultAnswerOptionService>();
            services.AddScoped<IResultAnswerService, ResultAnswerService>();
            services.AddScoped<ITestQuestionOptionService, TestQuestionOptionService>();
            services.AddScoped<ITestQuestionService, TestQuestionService>();
            services.AddScoped<ITestResultService, TestResultService>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IUrlService, UrlService>();
            services.AddScoped<IPassingTestService, PassingTestService>();
            services.AddScoped<ITestEventService, TestEventService>();
            services.AddScoped<IUrlValidatorService, UrlValidatorService>();
            services.AddScoped<ITestCalculationService, TestCalculationService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.ExpireTimeSpan = configuration.GetValue<TimeSpan>("CookieExpiration");
                options.Events.OnRedirectToAccessDenied = ReplaceRedirector(HttpStatusCode.Forbidden);
                options.Events.OnRedirectToLogin = ReplaceRedirector(HttpStatusCode.Unauthorized);
            });
        }

        public static void ConfigureCustomValidationErrors(this IServiceCollection services)
        {
            // Override ModelState.
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage)).ToList();
                    var result = new
                    {
                        Message = "Validation errors",
                        Errors = errors
                    };

                    return new BadRequestObjectResult(result);
                };
            });
        }

        private static Func<RedirectContext<CookieAuthenticationOptions>, Task> ReplaceRedirector(HttpStatusCode statusCode)
        {
            return context =>
            {
                context.Response.StatusCode = (int)statusCode;
                return Task.CompletedTask;
            };
        }
    }
}
