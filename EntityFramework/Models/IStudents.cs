namespace EntityFramework.Models
{
    public interface IStudents
    {
        public List<Students> GetStudents();
        public Students GetStudentById(int id);
        public void AddStudent(Students studentsDto);
        public void UpdateStudent(int id, Students studentsDto);
        public void DeleteStudent(int id);
        public IEnumerable<Students> SearchStudents(string name, string course);
    }

    public class StudentServices : IStudents
    {
        private readonly StudentDbContext _dbContext;

        public StudentServices(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Students> GetStudents()
        {
            return _dbContext.students.ToList();
        }

        public Students GetStudentById(int id)
        {
            return _dbContext.students.FirstOrDefault(s => s.Id == id);

        }

        public void AddStudent(Students studentsDto)
        {
            _dbContext.students.Add(studentsDto);
            _dbContext.SaveChanges();
        }

        public void UpdateStudent(int id, Students studentsDto)
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

        public IEnumerable<Students> SearchStudents(string name, string course)
        {
            var query = _dbContext.students.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(s => s.Name.Contains(name));
            if (!string.IsNullOrEmpty(course))
                query = query.Where(s => s.Course.Name.Contains(course));
            {
                
            }



            return query.ToList();
        }




    }
}
