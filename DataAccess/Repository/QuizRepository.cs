﻿using DataAccess.Data;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Modles.Domain;

namespace DataAccess.Repository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly ILogger logger;
        private readonly SQLApplicationContext context;

        public QuizRepository(SQLApplicationContext context, ILogger<QuizRepository> logger)
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

        public async Task<bool> DeleteQuizAsync(Quiz quiz)
        {
            if(quiz != null)
            {
                var result = context.Quizzes.Remove(quiz);
                if(result != null && result.State == EntityState.Deleted)
                {
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<Quiz?> GetQuizByIdAsync(int id)
        {
            return await context.Quizzes.Include(q => q.Questions).ThenInclude(a => a.Answers).FirstOrDefaultAsync(q => q.Id == id);
        }

        public IEnumerable<Quiz> GetQuizzes()
        {
            return context.Quizzes.Include(q => q.Questions).ThenInclude(a => a.Answers).ToList();
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesAsync()
        {
            return await context.Quizzes.Include(q => q.Questions).ThenInclude(a => a.Answers).ToListAsync();
        }

        public async Task<bool> UpdateQuizAsync(Quiz quiz)
        {
            await Task.Delay(1000);
            return true;
        }
    }
}
