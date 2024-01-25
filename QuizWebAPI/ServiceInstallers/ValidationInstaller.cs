using Microsoft.AspNetCore.Mvc;
using Models.Filters;

namespace QuizWebAPI.ServiceInstallers
{
    public class ValidationInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //Add fluent validation
            //services.AddFluentValidationAutoValidation();
            //services.AddScoped<IValidator<AddUserRequest>, UserRequestValidator>();
            //services.AddScoped<IValidator<UpdateUserRequest>, UserRequestValidator>();

            //Add action filter
            services.AddScoped<ValidateModelFilter>();
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        }
    }
}
