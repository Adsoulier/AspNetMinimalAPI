using Microsoft.AspNetCore.Mvc;
using MinimalWebApi.Models;

namespace MinimalWebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class DefaultController : ControllerBase
    {
        private IEnumerable<Contact> _contacts =
            [
                new Contact { Id = 1, Name = "John Doe", FistName = "John", Email = "oui@oui" },
                new Contact { Id = 2, Name = "Jane Smith", FistName = "Jane", Email = "non@non" },
            ];

        [HttpGet(Name = "AutomaticGet")]
        public string GetBaseString()
        {
            return "Hello, World! This is the default controller response.";
        }

        [HttpGet("contacts", Name = "GetContacts")]
        public IEnumerable<Contact> GetContacts()
        {
            return _contacts;
        }

        [HttpGet("contacts/{id:int}", Name = "GetContactById")]
        public ActionResult<Contact> GetContact(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost("contacts", Name = "CreateContact")]
        public ActionResult<Contact> CreateContact([FromBody] Contact contact)
        {
            if(contact == null || string.IsNullOrWhiteSpace(contact.Name) || string.IsNullOrWhiteSpace(contact.Email))
            {
                return BadRequest("Invalid contact data.");
            }
            contact.Id = _contacts.Max(c => c.Id) + 1;
            _contacts = _contacts.Append(contact);
            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
        }


    }
}
