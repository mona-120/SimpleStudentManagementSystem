using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStudentManagementSystem.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public string Description { get; set; }

        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public List<CourseInstructor> instructors { get; set; } = new List<CourseInstructor>();

        public override string ToString()
        {
            return $"CourseID: {CourseId} - Title: {Title} - Credits: {Credits} - Description: {Description}";
        }
    }
}
