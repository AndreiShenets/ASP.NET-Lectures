using EntityFrameworkCoreApp.DataStorage.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreApp.DataStorage
{
    public class QADbContext : DbContext
    {
        public QADbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            BuildAllModels(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected void BuildAllModels(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmailEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionEntityConfiguration());
        }
    }
}
