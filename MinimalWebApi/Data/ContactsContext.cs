using Microsoft.EntityFrameworkCore;

namespace MinimalWebApi.Data
{
    public class ContactsContext : DbContext
    {
        public ContactsContext(DbContextOptions<ContactsContext> options) : base(options)
        {
        }
        public DbSet<Models.Contact> Contacts { get; set; }
    }
}
