using Financial_App.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Financial_App.InfraStructure.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<MovementModel> Movements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(a => a.Balance)
                 .IsRequired();

                entity.Property(a => a.Limit)
                    .IsRequired();

                entity.HasMany(a => a.Movements)
                    .WithOne(m => m.Account)
                    .HasForeignKey(m => m.AccountId);
            });

            modelBuilder.Entity<MovementModel>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.AccountId)
                    .IsRequired();

                entity.Property(m => m.Description)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(m => m.Amount)
                    .IsRequired();

                entity.Property(m => m.Type)
                    .IsRequired();

                entity.Property(m => m.Data)
                    .IsRequired();

                entity.HasOne(m => m.Account)
                    .WithMany(a => a.Movements)
                    .HasForeignKey(m => m.AccountId);
            });
        }

    }
}
