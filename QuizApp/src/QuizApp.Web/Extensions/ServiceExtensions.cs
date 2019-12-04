using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using QuizApp.BLL.Interfaces;
using QuizApp.BLL.Services;

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
    }
}
