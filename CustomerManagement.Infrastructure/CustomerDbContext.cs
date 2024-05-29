using CustomerManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infrastructure
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Spongebob",
                    LastName = "Squarepants",
                    Email = "test1@test.com",
                    PhoneNumber = "12341234"
                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Patrick",
                    LastName = "Star",
                    Email = "test2@test.com",
                    PhoneNumber = "09870987"
                });
        }
       
    }
}
