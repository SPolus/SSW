using SSW.Data.Entitties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSW.Web.ViewModels.Instructor
{
    public class CourseStudents
    {
        [Display(Name = "Course")]
        public Course Course { get; set; }

        [Display(Name = "Count of students")]
        public int StudentsCount { get; set; }
    }
}