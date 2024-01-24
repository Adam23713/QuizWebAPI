
using QuizWebAPI.Services;
using QuizWebAPI.Services.Interfaces;

namespace QuizWebAPI.ServiceInstallers
{
    public class QuizServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IQuizService, QuizService>();
        }
    }
}
