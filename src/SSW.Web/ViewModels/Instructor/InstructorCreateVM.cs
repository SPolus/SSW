using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSW.Web.ViewModels.Instructor
{
    public class InstructorCreateVM
    {
        public int Id { get; set; }

        [Display(Name = "First Name*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required field")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required field")]
        public string LastName { get; set; }

        [Display(Name = "Email*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required field")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required field")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters")]
        public string Password { get; set; }
    }
}