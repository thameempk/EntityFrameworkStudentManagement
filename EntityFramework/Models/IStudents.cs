using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace EntityFramework.Models
{
    public interface IStudents
    {
         List<Students> GetStudents();
         Students GetStudentById(int id);
         void AddStudent(StudentsDto studentsDto);
         void UpdateStudent(int id, StudentsDto studentsDto);
         void DeleteStudent(int id);
         IEnumerable<StudentsDto> SearchStudents(string name, string course);
    }

    public class StudentServices : IStudents
    {
        private readonly StudentDbContext _dbContext;
        private readonly IMapper _mapper;

        public StudentServices(StudentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<Students> GetStudents()
        {
            return _dbContext.students.ToList();
        }

        public Students GetStudentById(int id)
        {
            return _dbContext.students.FirstOrDefault(s => s.Id == id);

        }

        public void AddStudent(StudentsDto studentsDto)
        {
            var std = _mapper.Map<Students>(studentsDto);
            _dbContext.students.Add(std);
            _dbContext.SaveChanges();
        }

        public void UpdateStudent(int id, StudentsDto studentsDto)
        {
            var std = _dbContext.students.Find(id);

            std.Name = studentsDto.Name;
            std.Age = studentsDto.Age;
            std.Email = studentsDto.Email;
            std.Phone = studentsDto.Phone;
            std.Password = studentsDto.Password;
            std.CourseId = studentsDto.CourseId;

            _dbContext.SaveChanges();
        }
        public void DeleteStudent(int id)
        {
            var std = _dbContext.students.FirstOrDefault(s => s.Id == id);

            _dbContext.students.Remove(std);
        }

        public IEnumerable<StudentsDto> SearchStudents(string name, string course)
        {
            var query = _dbContext.students.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(s => s.Name.Contains(name));
            if (!string.IsNullOrEmpty(course))
                query = query.Where(s => s.Course.Name.Contains(course));
            {
                
            }

            var result = query.ProjectTo<StudentsDto>(_mapper.ConfigurationProvider).ToList();



            return result;
        }




    }
}
