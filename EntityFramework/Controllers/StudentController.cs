using EntityFramework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudents _students;

        public StudentController(IStudents students)
        {
            _students = students;
        }

        [HttpGet]

        public ActionResult GetStudents()
        {
            return Ok(_students.GetStudents());

        }

        [HttpGet("{id}")]

        public ActionResult GetStudentById(int id)
        {
            return Ok(_students.GetStudentById(id));
        }

        [HttpPost]

        public IActionResult AddStudent([FromBody] StudentsDto studentsDto)
        {
            _students.AddStudent(studentsDto);
            return NoContent();
        }

        [HttpPut("{id}")]

        public IActionResult UpdateStudent(int id, [FromBody] StudentsDto studentsDto)
        {
            _students.UpdateStudent(id, studentsDto);
            return NoContent();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteStudent(int id)
        {
            _students.DeleteStudent(id);
            return NoContent();
        }

        [HttpGet("search")]
        public IActionResult GetStudents([FromQuery] string name, [FromQuery] string course, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var students = _students.SearchStudents(name, course);


                var paginatedStudents = students.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                return Ok(paginatedStudents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
