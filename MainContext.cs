using Microsoft.EntityFrameworkCore;
using Web_API.Models;
using WebApi.Models;

namespace WebShop
{
    public class ItemsContext: DbContext
    {
        private const string connectionString = "server=localhost;port=3306;database=Items;user=root;password=1234";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, MariaDbServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Name property as the primary
            // key of the Items table
            modelBuilder.Entity<Contact>().HasKey(e => e.Id);
            modelBuilder.Entity<Conversation>().HasKey(e => e.id);
            modelBuilder.Entity<Message>().HasKey(e => e.Id);

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
