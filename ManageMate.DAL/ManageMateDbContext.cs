using ManageMate.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageMate.DAL
{
    public class ManageMateDbContext : DbContext
    {
        public ManageMateDbContext() { }
        public ManageMateDbContext(DbContextOptions<ManageMateDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductID)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=ManageMateDB;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Product>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.ProductID = await GenerateUniqueProductIDAsync();
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        private async Task<int> GenerateUniqueProductIDAsync()
        {
            var random = new Random();
            int newID;
            bool exists;

            do
            {
                newID = random.Next(100000, 999999); // Generate 6-digit ID
                exists = await Products.AnyAsync(p => p.ProductID == newID);
            } while (exists);

            return newID;
        }
    }
}
