using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStudentManagementSystem.Entities
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public int? Grade { get; set; }

        public Student student { get; set; } = null!;
        public Course course { get; set; } = null!;


        public Enrollment() { }
        public Enrollment(int stId, int crId,int? grade = null)
        {
            StudentId = stId;
            CourseId = crId;
            Grade = grade;
        }
        public override string ToString()
        {
            return $"StudentId: {StudentId} - CourseId: {CourseId} - EnrollmentDate: {EnrollmentDate} - Grade: {Grade}";
        }
    }
}
