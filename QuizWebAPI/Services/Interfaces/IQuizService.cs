using DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Models.Domain;
using Models.Models.DTO;

namespace QuizWebAPI.Services.Interfaces
{
    public interface IQuizService : IQuizRepository
    {
        void CacheAllQuizzes();

        Task<string> GetResultAsync(int quizId, int userId);

        Task SubmitAnswer(UserAnswerDTO userAnswer);

        Task<int> SubmitQuizStart(int quizId, int userId, ulong timeLimitInMinutes);
    }
}
