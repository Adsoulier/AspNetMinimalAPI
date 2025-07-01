using MinimalWebApi.Data;
using MinimalWebApi.Models;

namespace MinimalWebApi.Services
{
    public class ContactService
    {
        private readonly ContactsContext _context;
        public ContactService(ContactsContext context)
        {
            _context = context;
            if (!_context.Contacts.Any())
            {
                _context.Contacts.AddRange(
                [
                        new Contact { Id = 1, Name = "John Doe", FistName = "John", Email = "oui@oui" },
                        new Contact { Id = 2, Name = "Jane Smith", FistName = "Jane", Email = "non@non" },
                        new Contact { Id = 3, Name = "Alice Johnson", FistName = "Alice", Email = "maybe@maybe" },
                        new Contact { Id = 4, Name = "Bob Brown", FistName = "Bob", Email = "test@test" },
                        new Contact { Id = 5, Name = "Charlie Black", FistName = "Charlie", Email = "mock@mock" },
                        new Contact { Id = 6, Name = "Diana White", FistName = "Diana", Email = "example@example" },
                        new Contact { Id = 7, Name = "Ethan Green", FistName = "Ethan", Email = "sample@sample" },
                        new Contact { Id = 8, Name = "Fiona Blue", FistName = "Fiona", Email = "demo@demo" },
                        new Contact { Id = 9, Name = "George Yellow", FistName = "George", Email = "test@test" }

                    ]);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return _context.Contacts.ToList();
        }

        public async Task<Contact?> GetContactByIdAsync(int id)
        {
            return await _context.FindAsync<Contact>(id);
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            contact.Id = _context.Contacts.Max(c => c.Id) + 1;
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task DeleteContactAsync(int id)
        {
            var contact = await GetContactByIdAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Contact with ID {id} not found.");
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
