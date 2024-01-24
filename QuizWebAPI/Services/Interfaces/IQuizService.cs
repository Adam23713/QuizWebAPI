using DataAccess.Repository.Interfaces;
using Models.Modles.Domain;

namespace QuizWebAPI.Services.Interfaces
{
    public interface IQuizService : IRepository
    {
        void CacheAllQuizzes();
    }
}
