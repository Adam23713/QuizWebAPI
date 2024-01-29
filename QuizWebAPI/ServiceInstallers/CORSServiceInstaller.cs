namespace QuizWebAPI.ServiceInstallers
{
    public class CORSServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins("https://localhost:7280")
                        .WithHeaders("X-API-Version", "Content-Type");  // Include "Content-Type" in the allowed headers
                });
            });
        }
    }
}
