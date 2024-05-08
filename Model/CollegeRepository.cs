using System;
namespace web_api.Model
{
	public static class CollegeRepository
	{
		public static List<Student> Students
		{
            get;
            set;
        } = new List<Student>()
        {
            new Student
            {
                Id = 1,
                StudentName = "John",
                Email = "john@gmail.com",
                Address = "Teuma"
            },
            new Student
            {
                Id = 2,
                StudentName = "Tom",
                Email = "john@gmail.com",
                Address = "Teuma"

            },
        };
    }
}
