﻿using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        [Required]
        public int CourseId { get; set; }

        public Course? Course { get; set; }
    }

    public class StudentsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        [Required]
        public int CourseId { get; set; }
    }


    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }

        public List<Students>? Students { get; set; }
    }
}
