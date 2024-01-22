using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace QuizWebAPI.ServiceInstallers
{
    public class SwaggerInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSwaggerGen();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Quiz Manager API - V1", Version = "v1" });

                x.ExampleFilters();

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "QuizWebAPI.xml");
                x.IncludeXmlComments(filePath);
            });

            services.AddSwaggerExamplesFromAssemblyOf<Program>();
        }
    }
}
