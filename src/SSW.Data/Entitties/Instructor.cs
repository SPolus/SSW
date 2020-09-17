using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSW.Data.Entitties
{
    public class Instructor : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<CourseAssignment> CourseAssignments { get; set; }
    }
}
