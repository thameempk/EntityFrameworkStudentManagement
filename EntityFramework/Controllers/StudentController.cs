using EntityFramework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace EntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class StudentController : ControllerBase
    {
        private readonly IStudents _students;
        private static Dictionary<string, string> otpDictionary = new Dictionary<string, string>();

        public StudentController(IStudents students)
        {
            _students = students;
        }


        [HttpPost]
        [Route("sendOTP")]
        public async Task<IActionResult> sendOtp(string email)
        {
            string otp = GenerateOTP();

            await SendEmail(email, otp);
            otpDictionary[email] = otp;
            return Ok("otp send successfully");

        }

        [HttpPost]
        [Route("verifyOTP")]
        public IActionResult verifyOtp(string email, string otp)
        {
            if(otpDictionary.ContainsKey(email) && otpDictionary[email] == otp)
            {
                otpDictionary.Remove(email);
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        private string GenerateOTP()
        {
            // Generate a 6-digit OTP
            Random rnd = new Random();
            int otp = rnd.Next(100000, 999999);
            return otp.ToString();
        }

        private async Task SendEmail(string toAddress, string otp)
        {
           
            // Implement sending email with OTP here (similar to previous example)
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587; // Port number may vary based on your email provider
            smtpClient.Credentials = new NetworkCredential("thameempk292@gmail.com", "xnuo hihq cfaw mkkg");
            smtpClient.EnableSsl = true; // Enable SSL for secure connection

            // Create a MailMessage object
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("thameempk292@gmail.com");
            mailMessage.To.Add(toAddress);
            mailMessage.Subject = "OTP Verification";
            mailMessage.Body = "Your OTP for email verification is: " + otp;
            // Send the email
            await smtpClient.SendMailAsync(mailMessage);
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
