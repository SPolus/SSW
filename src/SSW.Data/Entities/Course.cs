using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSW.Data.Entities
{
    public class Course : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<CourseAssignment> CourseAssignments { get; set; }
    }
}
