using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace EntityFramework.Models
{
    public interface IStudents
    {
         List<Students> GetStudents();
         Students GetStudentById(int id);
         bool AddStudent(StudentsDto studentsDto);
         bool UpdateStudent(int id, StudentsDto studentsDto);
         bool DeleteStudent(int id);
         IEnumerable<StudentsDto> SearchStudents(string name, string course);
    }

    public class StudentServices : IStudents
    {
        private readonly StudentDbContext _dbContext;
        private readonly IMapper _mapper;
        private ILogger<StudentServices> _logger; 

        public StudentServices(StudentDbContext dbContext, IMapper mapper, ILogger<StudentServices> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

       

        public List<Students> GetStudents()
        {
            return _dbContext.students.ToList();
        }

        public Students GetStudentById(int id)
        {
            return _dbContext.students.FirstOrDefault(s => s.Id == id);

        }

        public bool AddStudent(StudentsDto studentsDto)
        {
            var std = _mapper.Map<Students>(studentsDto);
            _dbContext.students.Add(std);
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateStudent(int id, StudentsDto studentsDto)
        {
            var std = _dbContext.students.Find(id);

            std.Name = studentsDto.Name;
            std.Age = studentsDto.Age;
            std.CourseId = studentsDto.CourseId;

            _dbContext.SaveChanges();
            return true;
        }
        public bool DeleteStudent(int id)
        {
            var std = _dbContext.students.FirstOrDefault(s => s.Id == id);
            if(std == null)
            {
                _logger.LogWarning("no student found");
            }

            _dbContext.students.Remove(std);
            return true;
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
