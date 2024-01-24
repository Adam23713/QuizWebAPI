using Microsoft.EntityFrameworkCore;
using Models.BaseModles.Domain.Base;
using Models.Modles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class SQLApplicationContext : DbContext
    {
        public DbSet<Quiz> Quizzes { get; private set; } = null!;

        public DbSet<Question> Questions { get; private set; } = null!;

        public DbSet<Answer> Answers { get; set; } = null!;

        public DbSet<UserAnswer> UserAnswers { get; set; } = null!;

        public SQLApplicationContext(DbContextOptions<SQLApplicationContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
