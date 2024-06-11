using web_api.Model;

namespace web_api.Data.Repository;

public interface IStudentRepository : ICollegeRepositiory<Student>
{
    Task<List<Student>> GetStudentByFeeStatusAsync(int feeStatus);
}