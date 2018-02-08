using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace EntityFrameworkCoreApp.DataStorage.Models
{
    public class QuestionEntity
    {
        public Guid QuestionId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<AnswerEntity> Answers { get; set; }
    }

    class QuestionEntityConfiguration : IEntityTypeConfiguration<QuestionEntity>
    {
        public void Configure(EntityTypeBuilder<QuestionEntity> builder)
        {
            builder.ToTable("Question");
            builder.HasKey(c => c.QuestionId);
            builder.Property(c => c.Description).HasMaxLength(2000);
            builder.Property(c => c.Name).HasMaxLength(200);

            builder
                .HasMany(t => t.Answers)
                .WithOne(t => t.Question)
                .HasForeignKey(t => t.QuestionId)
                .HasPrincipalKey(t => t.QuestionId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
