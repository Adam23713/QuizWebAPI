using Models.Modles.Domain;

namespace DataAccess.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Quiz>> GetQuizzesAsync();

        Task<Quiz?> GetQuizByIdAsync(int id);

        Task<Quiz> CreateQuizAsync(Quiz quiz);
    }
}
