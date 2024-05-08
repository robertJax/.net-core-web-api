using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_api.Data
{
	public class Student
	{
        public int Id { get; set; }

        public string StudentName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public DateTime DOB { get; set; }
    }
}

