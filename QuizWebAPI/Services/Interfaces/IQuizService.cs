using DataAccess.Repository.Interfaces;
using Models.Models.Domain;

namespace QuizWebAPI.Services.Interfaces
{
    public interface IQuizService : IQuizRepository
    {
        void CacheAllQuizzes();
    }
}
