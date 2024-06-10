using AutoMapper;
using web_api.Data;
using web_api.Model;

namespace web_api.Configurations;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        // CreateMap<Student, StudentDTO>();
        // CreateMap<StudentDTO, Student>();

        CreateMap<StudentDTO, Student>().ReverseMap();
    }
}