using SSW.Data.Entitties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSW.Web.ViewModels.Student
{
    public class StudentIndexVM
    {
        public int Id { get; set; }
        [Display(Name = "Student")]
        public string FullName { get; set; }
        [Display(Name = "Average Grade")]
        public Grade? AvgGrade { get; set; }
    }
}