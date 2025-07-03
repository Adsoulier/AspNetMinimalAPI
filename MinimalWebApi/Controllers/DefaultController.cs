using Microsoft.AspNetCore.Mvc;
using MinimalWebApi.Data;
using MinimalWebApi.Models;
using MinimalWebApi.Services;

namespace MinimalWebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class DefaultController : ControllerBase
    {
        private ContactService _service;


        public DefaultController(ContactService service)
        {
            _service = service;
        }

        [HttpGet("contacts", Name = "GetContacts")]
        public ActionResult<ApiResponse<IEnumerable<Contact>>> GetContacts(int page = 1, int pageSize = 10, string sortBy="Id", string sortDir = "asc")
        {
            var sample = _service.GetAllContacts()
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var isValidSort = typeof(Contact)
                .GetProperties()
                .Any(p => p.Name.Equals(sortBy));

            if (!isValidSort)
            {
                return BadRequest("Invalid sortBy field.Accepted fields are Id,Email,Name");
            }

            if (sortDir == "desc")
                {
                    sample = sample.OrderByDescending(c => c.GetType().GetProperty(sortBy)?.GetValue(c, null));
                }
                else
                {
                    sample = sample.OrderBy(c => c.GetType().GetProperty(sortBy)?.GetValue(c, null));
            }


            var testResponse = new ApiResponse<IEnumerable<Contact>>(sample, true);
            return Ok(testResponse);
        }

        [HttpGet("contacts/{id:int}", Name = "GetContactById")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _service.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost("contacts", Name = "CreateContact")]
        public async Task<ActionResult<Contact>> CreateContact([FromBody] Contact contact)
        {
            contact.Id = _service.GetAllContacts().Max(c => c.Id) + 1;
            await _service.CreateContactAsync(contact);
            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
        }

        [HttpDelete("contacts/{id:int}", Name = "DeleteContact")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            try
            {
                await _service.DeleteContactAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Contact with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpPut("contacts/{id:int}", Name = "UpdateContact")]
        public async Task<ActionResult<Contact>> UpdateContact(int id, [FromBody] Contact updatedContact)
        {
            var existingContact = await _service.GetContactByIdAsync(id);
            if (existingContact == null)
            {
                return NotFound();
            }
            existingContact.Name = updatedContact.Name;
            existingContact.FistName = updatedContact.FistName;
            existingContact.Email = updatedContact.Email;
            await _service.SaveChangesAsync();
            return Ok(existingContact);
        }

    }
}
