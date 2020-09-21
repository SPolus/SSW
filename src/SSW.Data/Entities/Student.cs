using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSW.Data.Entities
{
    public class Student : BaseEntity
    {
        public User User { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
