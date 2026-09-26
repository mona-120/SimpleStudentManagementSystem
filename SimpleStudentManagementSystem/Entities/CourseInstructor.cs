using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStudentManagementSystem.Entities
{
    public class CourseInstructor
    {
        public int InstructorId { get; set; }
        public int CourseId { get; set; }

        public Instructor instructor { get; set; } = null!;
        public Course course { get; set; } = null!;
    }
}
