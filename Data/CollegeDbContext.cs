using System;
using Microsoft.EntityFrameworkCore;
using web_api.Data.Config;

namespace web_api.Data
{
	public class CollegeDbContext : DbContext
	{
		public CollegeDbContext(DbContextOptions<CollegeDbContext>options):base(options)
		{
		
		}

		public DbSet<Student> Students { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Employee> Employees { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Table 1
			modelBuilder.ApplyConfiguration(new StudentConfig());
			
			//Table 2
			modelBuilder.ApplyConfiguration(new DepartmentConfig());
			
			//Table 3
			modelBuilder.ApplyConfiguration(new EmployeeConfig());

			//Table 2
			//modelBuilder.ApplyConfiguration(new CourseConfig());
		}
	}
}

