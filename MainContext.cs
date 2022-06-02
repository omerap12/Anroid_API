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
            modelBuilder.Entity<Contact>().HasKey(p => p.Id);
            modelBuilder.Entity<Contact>().HasKey(p => p.Id);
            modelBuilder.Entity<Contact>().HasMany<Contact>(p => p.Contacts).WithOne(p=> p.RefContact).HasForeignKey(p => p.Id);
            modelBuilder.Entity<Contact>().HasMany<Conversation>(p => p.Conversations)
                .WithMany(p => p.Contacts);

            modelBuilder.Entity<Message>().HasKey(p => p.Id);
            modelBuilder.Entity<Message>().HasOne(p => p.Contact);

            modelBuilder.Entity<Conversation>().HasKey(p => p.Id);
            modelBuilder.Entity<Conversation>().HasMany<Message>(p => p.Messages).WithOne(p => p.RefConversation)
                .HasForeignKey(p => p.ConversationId);
            modelBuilder.Entity<Conversation>().HasMany<Contact>(p => p.Contacts).WithMany(p => p.Conversations);




            
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
