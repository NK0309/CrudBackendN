using System.ComponentModel.DataAnnotations;

namespace WebApplication3.DTOs
{
    public class CustomerDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public int? Age { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string City { get; set; } = string.Empty;
        public string? modifiedOn { get; set; }
    }
}
