using DocuCheck.Domain.Entities.DocumentHistory;
using DocuCheck.Domain.Entities.DocumentHistory.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DocuCheck.Infrastructure.Persistence
{
    public class DocuCheckDbContext : DbContext
    {
        public DocuCheckDbContext()
        {
            // for EF Core
        }
        
        public DocuCheckDbContext(DbContextOptions<DocuCheckDbContext> options) 
            : base(options)
        {
        }
        
        public const string ConnectionStringKey = "DocuCheckDb";
        
        public DbSet<DocumentHistory> DocumentHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DocumentHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                
                entity.Property(e => e.Number)
                    .HasConversion(
                        number => number.Value,
                        value => DocumentNumber.Create(value))
                    .HasMaxLength(15)
                    .IsRequired();
                
                entity.Property(e => e.CheckedAt)
                    .IsRequired();
                
                entity.Property(e => e.ValidationResult)
                    .IsRequired();
            });
        }
    }
}