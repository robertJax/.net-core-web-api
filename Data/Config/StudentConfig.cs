using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace web_api.Data.Config;

public class StudentConfig : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseMySqlIdentityColumn();
        
        builder.Property(n => n.StudentName).IsRequired();
        builder.Property(n => n.StudentName).HasMaxLength(250);
        builder.Property(n => n.Address).IsRequired(false).HasMaxLength(500);
        builder.Property(n => n.Email).IsRequired().HasMaxLength(250);
        
        builder.HasData(new List<Student>()
        {
            new Student
            {
                Id = 1,
                StudentName = "John",
                Address = "India",
                Email = "j@gmail.com",
                DOB = new DateTime(2022, 12, 12)
            },
            new Student
            {
                Id = 2,
                StudentName = "Tom",
                Address = "Vanuatu",
                Email = "t@gmail.com",
                DOB = new DateTime(2021, 01, 01)
            }
        });

        builder.HasOne(n => n.Department)
            .WithMany(n => n.Students)
            .HasForeignKey(n => n.DepartmentId)
            .HasConstraintName("FK_Students_Department");
    }
}