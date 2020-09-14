using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.Entitties
{
    public class Enrollment : BaseEntity
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public Grade? Grade { get; set; }
    }

    public enum Grade
    {
        A = 5,
        B = 4,
        C = 3,
        D = 2,
        F = 1
    }
}
