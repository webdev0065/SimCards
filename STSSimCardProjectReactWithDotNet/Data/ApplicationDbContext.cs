using Microsoft.EntityFrameworkCore;
using STSSimCardProjectReactWithDotNet.Models;

namespace STSSimCardProjectReactWithDotNet.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }
            public DbSet<Register> Registers { get; set; }
            public DbSet<Login> Logins { get; set; }
            public DbSet<User> Users { get; set; }
            public DbSet<Provider> Providers { get; set; }
            public DbSet<Devices> Devicess { get; set; }
            public DbSet<SimCard> SimCards { get; set; }
            public DbSet<Customer> Customers { get; set; }
            
           
        }
}
