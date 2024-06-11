using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace web_api.Data.Config;

public class EmployeeConfig : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
        builder.HasKey(x => x.EmpId);

        builder.Property(x => x.EmpId).UseMySqlIdentityColumn();
        
        builder.Property(n => n.EmployeeName).IsRequired().HasMaxLength(200);
        builder.Property(n => n.DOB).IsRequired().HasMaxLength(200);
        builder.Property(n => n.NationalId).HasMaxLength(500).IsRequired();
        builder.Property(n => n.Island).IsRequired(false).HasMaxLength(200);
        
        builder.HasData(new List<Employee>()
        {
            new Employee
            {
                EmpId = 1,
                EmployeeName = "Jackson Robert",
                DOB = new DateTime(2022, 12, 12),
                Island = "Efate",
                NationalId = 111,
            },
            new Employee
            {
                EmpId = 2,
                EmployeeName = "Tchilumba Mera",
                DOB = new DateTime(2021, 12, 12),
                Island = "Maewo",
                NationalId = 112,
            }
        });

        builder.HasOne(n => n.Department)
            .WithMany(n => n.Employees)
            .HasForeignKey(n => n.DepartmentId)
            .HasConstraintName("FK_Employees_Department");

    }
}