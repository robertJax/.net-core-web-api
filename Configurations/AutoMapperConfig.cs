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

        //Config for different property names
        // CreateMap<StudentDTO, Student>().ReverseMap().ForMember(n=> n.Name, opt => opt.MapFrom(x =>x.StudentName));
        
        //Config for ignoring some property
        //CreateMap<StudentDTO, Student>().ReverseMap().ForMember(n=> n.StudentName, opt => opt.Ignore());

        //Config for transforming some property
        CreateMap<StudentDTO, Student>().ReverseMap()
            .ForMember(n => n.Address,
                opt => opt.MapFrom(n => string.IsNullOrEmpty(n.Address) ? "No address found" : n.Address))
            .ForMember(n => n.Email,
            opt => opt.MapFrom(n => string.IsNullOrEmpty(n.Email) ? "No email found" : n.Email));
        // .AddTransform<string>(n => string.IsNullOrEmpty(n)?"No address found": n);

        // CreateMap<StudentDTO, Student>().ReverseMap();

        //We can try some options for prefix or postfix
    }
}