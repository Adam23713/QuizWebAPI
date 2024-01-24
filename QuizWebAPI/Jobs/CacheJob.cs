using Quartz;
using QuizWebAPI.Services.Interfaces;

namespace QuizWebAPI.Jobs
{
    public class CacheJob : IJob
    {
        private readonly IQuizService quizService;

        public CacheJob(IQuizService quizService)
        {
            this.quizService = quizService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            quizService.CacheAllQuizzes();
            return Task.CompletedTask;
        }
    }
}
