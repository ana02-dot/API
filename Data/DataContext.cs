using BankAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(a => a.Balance)
                .HasPrecision(18, 2); 
            modelBuilder.Entity<Transaction>()
                .Property(a => a.Amount)
                .HasPrecision(18, 2);
        }

    }
}
