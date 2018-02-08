using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EntityFrameworkCoreApp.DataStorage.Models
{
    public class AnswerEntity
    {
        public Guid AnswerId { get; set; }

        public Guid QuestionId { get; set; }

        public string Description { get; set; }

        public virtual QuestionEntity Question { get; set; }
}

    class AnswerEntityConfiguration : IEntityTypeConfiguration<AnswerEntity>
    {
        public void Configure(EntityTypeBuilder<AnswerEntity> builder)
        {
            builder.ToTable("Answer");
            builder.HasKey(c => c.AnswerId);
            builder.Property(c => c.Description).HasMaxLength(2000);
        }
    }
}
