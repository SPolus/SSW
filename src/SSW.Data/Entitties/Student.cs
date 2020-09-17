using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSW.Data.Entitties
{
    public class Student : BaseEntity
    {
        //public int Id { get; set; }
        public virtual User User { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
