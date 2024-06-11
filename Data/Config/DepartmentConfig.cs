using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace web_api.Data.Config;

public class DepartmentConfig : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseMySqlIdentityColumn();
        
        builder.Property(n => n.DepartmentName).IsRequired().HasMaxLength(200);
        builder.Property(n => n.Description).HasMaxLength(500).IsRequired(false);
        
        builder.HasData(new List<Department>()
        {
            new Department
            {
                Id = 1,
                DepartmentName = "Finance and Treasury",
                Description = "Finance Department",
            },
            new Department
            {
                Id = 2,
                DepartmentName = "Customs",
                Description = "Customs Department",
            },
        });
    }
}