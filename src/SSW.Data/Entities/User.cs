using System.ComponentModel.DataAnnotations;

namespace SSW.Data.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public Student Student { get; set; }

        public Instructor Instructor { get; set; }
    }
}
