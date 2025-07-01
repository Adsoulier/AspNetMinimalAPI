using System.ComponentModel.DataAnnotations;

namespace MinimalWebApi.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string FistName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
