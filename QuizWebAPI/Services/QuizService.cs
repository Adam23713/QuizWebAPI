using DataAccess.Repository.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Models.Models.Domain;
using Models.Models.DTO;
using Models.Requests.Quiz;
using QuizWebAPI.Services.Interfaces;
using System.Xml.Serialization;

namespace QuizWebAPI.Services
{
    public class QuizService : IQuizService
    {
        private readonly int cacheExpireTimeInMinutes = 10;
        private readonly string allQuizKey = "AllQuiz";
        private readonly string allQuizByIdKey = "AllQuizById";

        private readonly ILogger logger;
        ICacheService cacheService;
        private readonly IQuizRepository quizRepository;

        public QuizService(ILogger<QuizService> logger, IQuizRepository quizRepository, ICacheService cacheService)
        {
            this.logger = logger;
            this.quizRepository = quizRepository;
            this.cacheService = cacheService;
        }

        #region Game

        public async Task<string> GetResultAsync(int quizId, int userId)
        {
            string result = "0";
            string statusKey = $"game_status:{userId}:{quizId}";
            var status = await cacheService.GetData<GameStatus>(statusKey);
            if (status != null) //Todo: search db to
            {
                result = status.Points.ToString();
            }
            return result;
        }

        public async Task SubmitAnswer(UserAnswerDTO userAnswer)
        {
            string statusKey = $"game_status:{userAnswer.UserId}:{userAnswer.QuizId}";
            var status = await cacheService.GetData<GameStatus>(statusKey);
            if (status != null) //Todo: search db to
            {
                var quizzes = await cacheService.GetData<Dictionary<int, Quiz>>(allQuizByIdKey);
                if (quizzes != null && quizzes.ContainsKey(userAnswer.QuizId))
                {
                    var quiz = quizzes[userAnswer.QuizId];
                    var question = quiz.Questions.FirstOrDefault(q => q.Id == userAnswer.QuestionId);
                    if(question != null)
                    {
                        if(!status.QuestionWithAnswers.ContainsKey(userAnswer.QuestionId))
                        {
                            status.QuestionWithAnswers.Add(userAnswer.QuestionId, new List<int>());
                        }
                        userAnswer.AnswersList.ForEach(answerId =>
                        {
                            if(question.Answers.First(a => a.Id == answerId).IsRight)
                            {
                                status.Points += 1;
                            }
                            status.QuestionWithAnswers[userAnswer.QuestionId].Add(answerId);
                        });
                        await CacheDateAsync(statusKey, status);
                    }
                }
            }
        }

        public async Task<int> SubmitQuizStart(int quizId, int userId, ulong tmeLimitInMinutes)
        {
            string statusKey = $"game_status:{userId}:{quizId}";
            var status = await cacheService.GetData<GameStatus>(statusKey);
            if(status == null) //Todo: search db to
            {
                var now = DateTime.Now;
                status = new GameStatus()
                {
                    QuizId = quizId,
                    UserId = userId, 
                    Started = now,
                    Points = 0,
                    WillEndAt = now.AddMinutes(tmeLimitInMinutes),
                };

                //Todo experi time
                await CacheDateAsync(statusKey, status);
            }
            else 
            {
                //Todo: get last filled question
            }

            return 0;
        }

        #endregion

        #region Cache

        private async Task ForceReCacheAllQuiz()
        {
            var removeResult = await cacheService.RemoveData(allQuizKey);
            var removeResult2 = await cacheService.RemoveData(allQuizByIdKey);
            if (removeResult is bool && (bool)removeResult && removeResult2 is bool && (bool)removeResult2)
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

        public async Task CacheQuizzesById(IEnumerable<Quiz>? quizzes)
        {
            if(quizzes == null)
            {
                return;
            }

            Dictionary<int, Quiz> dictionary = new Dictionary<int, Quiz>();
            foreach (var quiz in quizzes)
            {
                dictionary.Add(quiz.Id, quiz);
            }

            await CacheDateAsync(allQuizByIdKey, dictionary);
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
            Task.Factory.StartNew(async () => { await CacheQuizzesById(quizzes); }).Wait();
            CacheDateAsync(allQuizKey, quizzes).Wait();
        }

        private async Task<IEnumerable<Quiz>> ReadAndCacheAllQuizAsync()
        {
            var quizzes = await quizRepository.GetQuizzesAsync();
            await CacheDateAsync(allQuizKey, quizzes);
            await CacheQuizzesById(quizzes);
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

        public async Task<bool> UpdateQuizAsync(UpdateQuizRequest updatedQuiz, Quiz original)
        {
            var result = await quizRepository.UpdateQuizAsync(updatedQuiz, original);
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
