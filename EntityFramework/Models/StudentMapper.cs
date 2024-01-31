using AutoMapper;

namespace EntityFramework.Models
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<Students, StudentsDto>().ReverseMap();
        }
    }
}
