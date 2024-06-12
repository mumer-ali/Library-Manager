using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}