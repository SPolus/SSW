using SSW.Data.Entitties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSW.Web.ViewModels.Instructor
{
    public class InstructorIndexVM
    {
        public int Id { get; set; }
        [Display(Name = "Instructor")]
        public string FullName { get; set; }
        public List<CourseStudents> CourseStudents { get; set; }
    }
}