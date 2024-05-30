using Microsoft.EntityFrameworkCore;
using SDSExamNet7.Models.DTOS;
using SDSExamNet7.Models.Entities;

namespace SDSExamNet7.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<RecyclableItem> RecyclableItem { get; set; }
        public DbSet<RecyclableType> RecyclableType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RecyclableItem>()
                .HasOne(ri => ri.RecyclableType)
                .WithMany(rt => rt.RecyclableItems)
                .HasForeignKey(ri => ri.RecyclableTypeId);


            modelBuilder.Entity<RecyclableType>()
               .HasIndex(rt => rt.Type)
               .IsUnique();

            modelBuilder.Entity<RecyclableItem>()
                .Property(r => r.ComputedRate)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<RecyclableItem>()
                .Property(r => r.Weight)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<RecyclableType>()
                .Property(r => r.MinKg)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<RecyclableType>()
                .Property(r => r.MaxKg)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<RecyclableType>()
                .Property(r => r.Rate)
                .HasColumnType("decimal(18,2)");
        }

        public async Task<List<RecyclableItemWithTypeDTO>> GetRecyclableItemsWithTypesAsync()
        {
            return await Set<RecyclableItemWithTypeDTO>().FromSqlRaw("EXEC GetRecyclableItemsWithTypes").ToListAsync();
        }

    }
}
