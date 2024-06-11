using System.Dynamic;
using Microsoft.EntityFrameworkCore;

namespace web_api.Data.Repository;

//the StudentRepository has all methods inherited from the CollegeRepository (common repository)
public class StudentRepository : CollegeRepository<Student>, IStudentRepository
{
    private readonly CollegeDbContext _dbContext;

    public StudentRepository(CollegeDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Student>> GetStudentByFeeStatusAsync(int feeStatus)
    {
        //Write code to return students having fee status pending
        return null;
    }
}