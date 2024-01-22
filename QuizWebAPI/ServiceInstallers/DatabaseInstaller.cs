using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace QuizWebAPI.ServiceInstallers
{
    public class DatabaseInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SQLApplicationContext>(options => options.UseSqlServer("name=DefaultConnection"));
            services.AddScoped<IRepository, SQLRepository>();
        }
    }
}
