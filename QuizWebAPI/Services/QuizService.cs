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
        private readonly IQuizRepository quizRepository;

        public QuizService(ILogger<QuizService> logger, IQuizRepository quizRepository, ICacheService cacheService)
        {
            this.logger = logger;
            this.quizRepository = quizRepository;
            this.cacheService = cacheService;
        }

        #region Cache

        private async Task ForceReCacheAllQuiz()
        {
            var removeResult = await cacheService.RemoveData(allQuizKey);
            if (removeResult is bool && (bool)removeResult)
            {
                var list = await ReadAndCacheAllQuizAsync();
                if (list.IsNullOrEmpty())
                {
                    logger.LogError("Failed to cache quizzes");
                }
            }
            else
            {
                logger.LogError("Quiz remove and re-caching failed");
            }
        }

        private async Task CacheDateAsync<T>(string key, T data)
        {
            var succesCaching = await cacheService.SetData<T>(key, data, DateTimeOffset.Now.AddMinutes(cacheExpireTimeInMinutes));
            if (!succesCaching)
            {
                logger.LogError("Data caching failed");
            }
            else 
            {
                logger.LogInformation("Data cached");
            }
        }

        public void CacheAllQuizzes()
        {
            var quizzes = GetQuizzes();
            CacheDateAsync(allQuizKey, quizzes).Wait();
        }

        private async Task<IEnumerable<Quiz>> ReadAndCacheAllQuizAsync()
        {
            var quizzes = await quizRepository.GetQuizzesAsync();
            await CacheDateAsync(allQuizKey, quizzes);
            return quizzes;
        }

        #endregion

        #region CRUD_FOR_QUIZ

        public async Task<IEnumerable<Quiz>> GetQuizzesAsync()
        {
            var quizzes = await cacheService.GetData<IEnumerable<Quiz>>(allQuizKey);
            if(quizzes == null)
            {
                quizzes = await ReadAndCacheAllQuizAsync();
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

        public IEnumerable<Quiz> GetQuizzes()
        {
            return quizRepository.GetQuizzes();
        }

        public async Task<Quiz> CreateQuizAsync(Quiz quiz)
        {
            var newQuiz = await quizRepository.CreateQuizAsync(quiz);
            await ForceReCacheAllQuiz();
            return newQuiz;
        }

        public async Task<bool> UpdateQuizAsync(Quiz quiz)
        {
            var result = await quizRepository.UpdateQuizAsync(quiz);
            if (result)
            {
                await ForceReCacheAllQuiz();
            }
            return result;
        }

        public async Task<bool> DeleteQuizAsync(Quiz quiz)
        {
            var result = await quizRepository.DeleteQuizAsync(quiz);
            if(result)
            {
                await ForceReCacheAllQuiz();
            }
            return result;
        }

        #endregion
    }
}
