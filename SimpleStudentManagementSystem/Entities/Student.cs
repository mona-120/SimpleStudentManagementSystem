using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStudentManagementSystem.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateOnly? DateOfBirth { get; set; } 
        public DateOnly EnrollmentDate { get; set; } 

        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public override string ToString()
        {
            return $"StudentId: {StudentId} - FullName: {FullName} - Email: {Email} - DateOfBirth: {DateOfBirth} - EnrollmentDate: {EnrollmentDate}";
        }
    }
}
