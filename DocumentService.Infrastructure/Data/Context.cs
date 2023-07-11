using DocumentService.Domain;
using Microsoft.EntityFrameworkCore;

namespace DocumentService.Infrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Document> Documents { get; set; }
        public DbSet<FileLink> FileLinks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
