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
        }
    }
}
