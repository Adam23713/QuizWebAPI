using Models.Modles.Domain;

namespace DataAccess.Repository.Interfaces
{
    public interface IQuizRepository
    {
        IEnumerable<Quiz> GetQuizzes();

        Task<IEnumerable<Quiz>> GetQuizzesAsync();

        Task<Quiz?> GetQuizByIdAsync(int id);

        Task<Quiz> CreateQuizAsync(Quiz quiz);

        Task<bool> DeleteQuizAsync(Quiz quiz);

        Task<bool> UpdateQuizAsync(Quiz quiz);
    }
}
