using DocumentUploader.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentUploader.Infrastructure.Config
{
    internal class DocumentEntityConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.File).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Location).IsRequired();
        }
    }
}
