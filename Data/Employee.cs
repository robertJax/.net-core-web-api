namespace web_api.Data;

public class Employee
{
    public int EmpId { get; set; }
    
    public string EmployeeName { get; set; }
    
    public DateTime DOB { get; set; }
    
    public string Island { get; set; }
    
    public int NationalId { get; set; }
    
    public int? DepartmentId { get; set; }
    
    public virtual Department? Department { get; set; }
}