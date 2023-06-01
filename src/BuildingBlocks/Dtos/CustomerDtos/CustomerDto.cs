using System.ComponentModel.DataAnnotations;

namespace CustomerDtos
{
    public class CustomerDto
    {
        [Required]
        [MaxLength(20)]
        public string SurName { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        
        [MaxLength(20)]
        public string MiddleName { get; set; }
        [Required]
        [MaxLength(15)]
        public string Phone { get; set; }
        [Required]
        [MaxLength(20)]
        [EmailAddress]
        public string Email { get; set; }
    }
}