using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EntityFrameworkCoreApp.DataStorage.Models
{
    public class EmailEntity
    {
        public Guid EmailId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string SendTo { get; set; }

        public bool IsSent { get; set; }

        public DateTime GenerateDateTimeUTC { get; set; }

        public DateTime? SendDateTimeUTC { get; set; }
    }

    class EmailEntityConfiguration : IEntityTypeConfiguration<EmailEntity>
    {
        public void Configure(EntityTypeBuilder<EmailEntity> builder)
        {
            builder.ToTable("Email");
            builder.HasKey(c => c.EmailId);
            builder.Property(c => c.Body).HasMaxLength(2000);
            builder.Property(c => c.Title).HasMaxLength(200);
        }
    }
}
