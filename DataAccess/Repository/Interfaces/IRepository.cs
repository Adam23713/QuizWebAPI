﻿using Models.Modles.Domain;

namespace DataAccess.Repository.Interfaces
{
    public interface IRepository
    {
        IEnumerable<Quiz> GetQuizzes();

        Task<IEnumerable<Quiz>> GetQuizzesAsync();

        Task<Quiz?> GetQuizByIdAsync(int id);

        Task<Quiz> CreateQuizAsync(Quiz quiz);
    }
}
