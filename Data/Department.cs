namespace web_api.Data;

public class Department
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Student> Students { get; set; }
    
    public virtual ICollection<Employee> Employees { get; set; }
}