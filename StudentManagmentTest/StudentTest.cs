using AutoMapper;
using EntityFramework.Controllers;
using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Configuration;
using Xunit;

namespace StudentManagmentTest
{
    public class StudentTest
    {
        [Fact]
        public void GetStudents_ReturnsAllStudents()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContext = new StudentDbContext(dbContextOptions);
            SeedData(dbContext);

            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<StudentServices>>();

            var services = new StudentServices(dbContext, mockMapper.Object, mockLogger.Object);

            // Act
            var result = services.GetStudents();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(21, result[0].Age);
            Assert.Equal("Bob", result[1].Name);
        }


        [Fact]

        public void GetStudentById_ReturnOneStudent()
        {
            var dbContextOption = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDataBase")
                .Options;
            var dbContext = new StudentDbContext(dbContextOption);
            SeedData(dbContext);

            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<StudentServices>>();

            var services = new StudentServices(dbContext, mockMapper.Object, mockLogger.Object);

            var result = services.GetStudentById(1);

            Assert.Equal("Alice", result.Name);
        }

        [Fact]

        public void AddStudentTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName : "TestDataBase")
                .Options;
            var dbContext = new StudentDbContext(dbContextOptions);
          
            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<StudentServices>>();

            var services = new StudentServices(dbContext, mockMapper.Object, mockLogger.Object);

            var Student = new StudentsDto { Id = 3,  Name = "shaan", Age = 21 };
            var status = services.AddStudent(Student);
            Assert.True(status);
        

        }

        [Fact]
        public void DeleteStudent()
        {
            var dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDataBase")
                .Options;
            var dbContext = new StudentDbContext(dbContextOptions);
            
            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<StudentServices>>();

            var services = new StudentServices(dbContext, mockMapper.Object, mockLogger.Object);

            var status = services.DeleteStudent(1);
            Assert.True(status);
        }


        private void SeedData(StudentDbContext dbContext)
        {
            var students = new List<Students>
            {
                new Students { Id = 1, Name = "Alice" , Age = 21},
                new Students { Id = 2, Name = "Bob", Age = 21 }
            };

            dbContext.students.AddRange(students);
            dbContext.SaveChanges();
        }
    }
}
