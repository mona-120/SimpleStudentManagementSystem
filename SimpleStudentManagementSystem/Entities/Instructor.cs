using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStudentManagementSystem.Entities
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string FullName { get; set; }

        public List<CourseInstructor> courses { get; set; } = new List<CourseInstructor>();
    }
}
