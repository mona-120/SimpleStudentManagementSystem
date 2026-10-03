using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStudentManagementSystem.Entities
{
    public class CourseInstructor
    {
        public int InstructorId { get; set; }
        public int CourseId { get; set; }

        public Instructor Instructor { get; set; } = null!;
        public Course Course { get; set; } = null!;


        public CourseInstructor() { }
        public CourseInstructor(int insId ,int crId)
        {
            InstructorId = insId;
            CourseId = crId;
        }

        public override string ToString()
        {
            return $"InstructorId: {InstructorId} -  CourseId: {CourseId}";
        }
    }
}
