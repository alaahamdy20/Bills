using Bills.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bills.Models
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<CompanyData> CompanyDatas { get; set; }
        public virtual DbSet<TypeData> TypeDatas { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        
        public virtual DbSet<CompanyType> CompanyTypes { get; set; }

        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillItem> BillItems { get; set; }
        public virtual DbSet<BillDetails> BillDetails { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CompanyData>(entity => {
                entity.HasIndex(e => e.Name).IsUnique();
            });

            builder.Entity<CompanyType>(entity =>
            {
                entity.HasKey(e => new { e.CompanyDataId, e.TypeDataId });
            });

            builder.Entity<CompanyType>()
            .HasOne<CompanyData>(sc => sc.CompanyData)
            .WithMany(s => s.CompanyTypes)
            .HasForeignKey(sc => sc.CompanyDataId);


            builder.Entity<CompanyType>()
                .HasOne<TypeData>(sc => sc.TypeData)
                .WithMany(s => s.CompanyTypes)
                .HasForeignKey(sc => sc.TypeDataId);

            builder.Entity<Bill>()
               .HasOne(a => a.BillDetails)
               .WithOne(b => b.bill)
               .HasForeignKey<BillDetails>(b => b.BillId);

        }
     

    }
}
