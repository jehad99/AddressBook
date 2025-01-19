using AddressBook.src.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.src.Infrastructure.DbConext
{
    public class AddressBookDbContext : DbContext
    {
        public AddressBookDbContext(DbContextOptions<AddressBookDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<AddressEntry> AddressEntries { get; set; }

    }

}
