using DataAccess.Data;
using DataAccess.Repository;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace QuizWebAPI.ServiceInstallers
{
    public class DatabaseInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SQLApplicationContext>(options => options.UseSqlServer("name=DefaultConnection", b => b.MigrationsAssembly("QuizWebAPI")));
            services.AddScoped<IQuizRepository, QuizRepository>();
        }
    }
}
