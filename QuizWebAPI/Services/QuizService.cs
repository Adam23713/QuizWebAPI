using DataAccess.Repository.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Models.Modles.Domain;
using QuizWebAPI.Services.Interfaces;

namespace QuizWebAPI.Services
{
    public class QuizService : IQuizService
    {
        private readonly int cacheExpireTimeInMinutes = 10;
        private readonly string allQuizKey = "AllQuiz";

        private readonly ILogger logger;
        ICacheService cacheService;
        private readonly IRepository quizRepository;

        public QuizService(ILogger<QuizService> logger, IRepository quizRepository, ICacheService cacheService)
        {
            this.logger = logger;
            this.quizRepository = quizRepository;
            this.cacheService = cacheService;
        }

        private async Task CacheDate<T>(string key, T data)
        {
            var succesCaching = await cacheService.SetData<T>(key, data, DateTimeOffset.Now.AddMinutes(cacheExpireTimeInMinutes));
            if (!succesCaching)
            {
                logger.LogError("All quiz caching failed!");
            }
        }

        private async Task<IEnumerable<Quiz>> ReadAndCacheAllQuiz()
        {
            var quizzes = await quizRepository.GetQuizzesAsync();
            await CacheDate(allQuizKey, quizzes);
            return quizzes;
        }

        #region CRUD_FOR_QUIZ

        public async Task<IEnumerable<Quiz>> GetQuizzesAsync()
        {
            var quizzes = await cacheService.GetData<IEnumerable<Quiz>>(allQuizKey);
            if(quizzes == null)
            {
                quizzes = await ReadAndCacheAllQuiz();
            }
            return quizzes;
        }

        public async Task<Quiz?> GetQuizByIdAsync(int id)
        {
            Quiz? selectedQuiz = null;
            var quizzes = await GetQuizzesAsync();
            if (quizzes != null)
            {
                selectedQuiz = quizzes.FirstOrDefault(quiz => quiz.Id == id);
            }
            return selectedQuiz;
        }

        public async Task<Quiz> CreateQuizAsync(Quiz quiz)
        {
            var newQuiz = await quizRepository.CreateQuizAsync(quiz);
            var removeResult = await cacheService.RemoveData(allQuizKey);
            if (removeResult is bool && (bool)removeResult)
            {
                var list = await ReadAndCacheAllQuiz();
                if(list.IsNullOrEmpty())
                {
                    logger.LogError("Failed to create new quiz");
                }
            }
            else
            {
                logger.LogError("Quiz remove and re caching failed");
            }
            return newQuiz;
        }

        #endregion
    }
}
