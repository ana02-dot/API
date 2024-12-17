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
        public DbSet<BankTransactions> BankTransactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(a => a.Balance)
                .HasPrecision(18, 2); 
            modelBuilder.Entity<BankTransactions>()
                .Property(a => a.Amount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Account>()
               .HasOne(a => a.User)
               .WithMany(u => u.Accounts)
               .HasForeignKey(a => a.UserID);
        }


    }
}
