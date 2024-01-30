using Microsoft.EntityFrameworkCore;
using Models.Models.Domain;

namespace DataAccess.Data
{
    public class SQLApplicationContext : DbContext
    {
        public DbSet<Quiz> Quizzes { get; private set; } = null!;

        public DbSet<Question> Questions { get; private set; } = null!;

        public DbSet<Answer> Answers { get; set; } = null!;

        public SQLApplicationContext(DbContextOptions<SQLApplicationContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
