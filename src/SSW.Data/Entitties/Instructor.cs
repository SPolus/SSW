using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.Entitties
{
    public class Instructor : User
    {
        public ICollection<CourseAssignment> CourseAssignments { get; set; }
    }
}
