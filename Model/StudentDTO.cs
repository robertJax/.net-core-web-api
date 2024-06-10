using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using web_api.Validation;

namespace web_api.Model
{
	public class StudentDTO
	{
        [ValidateNever] //make sure Id field is never validated
        public int Id { get; set; }

        [Required(ErrorMessage = "Student name is required")]
        [StringLength(30)] //to accept max charact only
        public string StudentName { get; set; }

        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }

        //[Range(10,20)] //accept age range 10-20 only
        //public int Age { get; set; }

        [Required(ErrorMessage = "Please enter valid Address")]
        public string Address { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        //public string Password { get; set; }

        //[Compare(nameof(Password))] //Compare match Confirm password with password validator
        //public string ConfirmPassword { get; set; }

        //[DateCheckAttribute]
        //public DateTime AdmissionDate { get; set; }
	}
}

