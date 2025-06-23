using Microsoft.AspNetCore.Mvc;
using MinimalWebApi.Data;
using MinimalWebApi.Models;

namespace MinimalWebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class DefaultController : ControllerBase
    {
        private ContactsContext _context;


        public DefaultController(ContactsContext context)
        {
            _context = context;
            if (!_context.Contacts.Any())
            {
                _context.Contacts.AddRange([
                new Contact { Id = 1, Name = "John Doe", FistName = "John", Email = "oui@oui" },
                new Contact { Id = 2, Name = "Jane Smith", FistName = "Jane", Email = "non@non" },
            ]);
                _context.SaveChanges();
            }
        }

        [HttpGet(Name = "AutomaticGet")]
        public string GetBaseString()
        {
            return "Hello, World! This is the default controller response.";
        }

        [HttpGet("contacts", Name = "GetContacts")]
        public IEnumerable<Contact> GetContacts()
        {
            return _context.Contacts;
        }

        [HttpGet("contacts/{id:int}", Name = "GetContactById")]
        public ActionResult<Contact> GetContact(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost("contacts", Name = "CreateContact")]
        public async Task<ActionResult<Contact>> CreateContact([FromBody] Contact contact)
        {
            if(contact == null || string.IsNullOrWhiteSpace(contact.Name) || string.IsNullOrWhiteSpace(contact.Email))
            {
                return BadRequest("Invalid contact data.");
            }
            contact.Id = _context.Contacts.Max(c => c.Id) + 1;
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
        }

        [HttpDelete("contacts/{id:int}", Name = "DeleteContact")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            if(!_context.Contacts.Any(c => c.Id == id))
            {
                return NotFound();
            }
            _context.Contacts.Remove(_context.Contacts.First(c => c.Id == id));
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
