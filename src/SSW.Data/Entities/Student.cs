using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSW.Data.Entities
{
    public class Student : BaseEntity
    {
        public User User { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
