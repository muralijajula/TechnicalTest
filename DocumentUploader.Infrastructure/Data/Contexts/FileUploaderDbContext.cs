using System.ComponentModel;
using DocumentUploader.Infrastructure.Config;
using DocumentUploader.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocumentUploader.Infrastructure.Data.Contexts
{
    public class FileUploaderDbContext : DbContext
    {
        public FileUploaderDbContext(DbContextOptions<FileUploaderDbContext> options) : base(options)
        {

        }

        internal  virtual DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DocumentEntityConfiguration());
        }
    }
}
