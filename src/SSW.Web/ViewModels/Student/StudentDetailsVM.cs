using SSW.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSW.Web.ViewModels.Student
{
    public class StudentDetailsVM
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Courses")]
        public ICollection<Course> Courses { get; set; }
    }
}