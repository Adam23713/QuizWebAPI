using Quartz;
using QuizWebAPI.Jobs;
using QuizWebAPI.Services.Interfaces;

namespace QuizWebAPI.ServiceInstallers
{
    public class CacheJobInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddQuartz(q =>
            {
                q.AddJob<CacheJob>(j => j.WithIdentity("CacheJob"));
                q.AddTrigger(t => t
                    .WithIdentity("CacheTrigger")
                    .StartAt(DateBuilder.FutureDate(10, IntervalUnit.Second)) // 10-second delay
                    .WithSimpleSchedule(s => s
                        .WithRepeatCount(0) // Run only once
                    )
                    .ForJob("CacheJob") // Associate the trigger with the job
                );
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}
