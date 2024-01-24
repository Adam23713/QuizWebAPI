using DataAccess.Data;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Modles.Domain;

namespace DataAccess.Repository
{
    public class SQLRepository : IRepository
    {
        private readonly ILogger logger;
        private readonly SQLApplicationContext context;

        public SQLRepository(SQLApplicationContext context, ILogger<SQLRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<Quiz> CreateQuizAsync(Quiz quiz)
        {
            context.Quizzes.Add(quiz);
            await context.SaveChangesAsync();
            return quiz;
        }

        public async Task<Quiz?> GetQuizByIdAsync(int id)
        {
            return await context.Quizzes.Include(q => q.Questions).ThenInclude(a => a.Answers).FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesAsync()
        {
            return await context.Quizzes
                .Include(q => q.Questions).ThenInclude(a => a.Answers)
                .ToListAsync();
        }
    }
}
