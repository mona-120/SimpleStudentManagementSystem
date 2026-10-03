using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;
using SimpleStudentManagementSystem.Common;
using SimpleStudentManagementSystem.Data;
using SimpleStudentManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStudentManagementSystem.Services
{
    public class EnrollmentService
    {
        private readonly AppDbContext _context;
        public EnrollmentService(AppDbContext context)
        {
            _context = context; 
        }

        // add new enrollment row
        public async Task<Result<Enrollment>> AddEnrollment(Enrollment enrollment)
        {
            bool course = await _context.Courses.AnyAsync(c => c.CourseId == enrollment.CourseId);
            bool student = await _context.Students.AnyAsync(s => s.StudentId == enrollment.StudentId);

            if (!course)
                return new Result<Enrollment>(false, "Invalid input, Course doesn't exist", enrollment);

            if (!student)
                return new Result<Enrollment>(false, "Invalid input, Student doesn't exist", enrollment);

            if (await _context.Enrollments.AnyAsync(e => e.CourseId == enrollment.CourseId && e.StudentId == enrollment.StudentId))
                return new Result<Enrollment>(false, "Invalid input, Student Already exist", enrollment);

            if (enrollment.Grade != null && (enrollment.Grade < 0 || enrollment.Grade > 100))
                return new Result<Enrollment>(false, "Grade must be >= 0 OR <= 100", enrollment);

            await _context.AddAsync(enrollment);
            await _context.SaveChangesAsync();
            return new Result<Enrollment>(true, $"Enrollment of  student {enrollment.StudentId} for Course {enrollment.CourseId} Added Successfully", enrollment);
        }


        // Get Enrollment by course id
        public async Task<Result<List<Enrollment>>> GetCourseEnrollments(int crsId)
        {
            List<Enrollment> students = await _context.Enrollments.Where(e => e.CourseId == crsId)
                                       .Include(e=> e.Student).AsNoTracking().ToListAsync();

            if (students.Count == 0)
                return new Result<List<Enrollment>>(false, "Course doesn't contain students", []);

            return new Result<List<Enrollment>>(true, "Found Course Students", students);

        }

        // Get Enrollment by student id
        public async Task<Result<List<Enrollment>>> GetStudentEnrollments(int stuId)
        {
            List<Enrollment> courses = await _context.Enrollments.Where(e => e.StudentId == stuId)
                                        .Include(e=> e.Course).AsNoTracking().ToListAsync();

            if (courses.Count == 0)
                return new Result<List<Enrollment>>(false, "Student doesn't have courses", []);

            return new Result<List<Enrollment>>(true, "Found Student Courses", courses);

        }


        // Get Enrollment by ID
        public async Task<Result<Enrollment>> GetEnrollmentByID(int StudentId, int courseId)
        {
            var enrollment = await _context.Enrollments
                                         .Include(e=> e.Student).Include(e=> e.Course).AsNoTracking()
                                         .FirstOrDefaultAsync(e=> e.StudentId == StudentId  && e.CourseId == courseId);
            if (enrollment == null)
                return new Result<Enrollment>(false, "Enrollment Doesn't Exist", null);

            return new Result<Enrollment>(true, "Enrollment Found", enrollment);
        }


        // Update enrollment (update grade)
        public async Task<Result<Enrollment>> UpdateEnrollement(Enrollment enrollment)
        {
            Enrollment? _enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.CourseId == enrollment.CourseId && e.StudentId == enrollment.StudentId);

            if (_enrollment == null)
                return new Result<Enrollment>(false, "Enrollment Doesn't exist", _enrollment);

            if (enrollment.Grade < 0 || enrollment.Grade > 100)
                return new Result<Enrollment>(false,"Grade must be >= 0 OR <= 100", _enrollment);
            
            _enrollment.Grade = enrollment.Grade;
            await _context.SaveChangesAsync();
            return new Result<Enrollment>(true, $"Grade Updated Successfully to {_enrollment.Grade}", _enrollment);
        }

       
        // Delete Enrollment
        public async Task<Result<Enrollment>> DeleteEnrollment(int stuId,int crId)
        {
            Enrollment? _enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.CourseId == crId && e.StudentId == stuId);

            if (_enrollment == null)
                return new Result<Enrollment>(false, "Enrollment Doesn't exist", _enrollment);

            _context.Enrollments.Remove(_enrollment);
            await _context.SaveChangesAsync();
            return new Result<Enrollment>(true, $"Student {_enrollment.StudentId} with course {_enrollment.CourseId} Deleted Successfully", _enrollment);
        }


        // Average grades per course
        public async Task<Result<double?>> CourseAverageGrade(int crId)
        {
            bool course = await _context.Enrollments.AnyAsync(e=> e.CourseId == crId);
            if(!course)
                return new Result<double?>(false, "Course doesn't have enrollments", null);

            double? Avg = await _context.Enrollments.Where(e=> e.CourseId == crId)
                                    .AverageAsync(e=> e.Grade);

            if(Avg == null)
                return new Result<double?>(false, "Students don't receive grades", null);

            return new Result<double?>(true, $"Average Grades for course {crId} = {Avg}", Avg);
        }



        // Get EnrollmentList
        public async Task<Result<List<Enrollment>>> GetEnrollments()
        {
            List<Enrollment> _enrollments = await _context.Enrollments.AsNoTracking().ToListAsync();
            if (_enrollments.Count == 0)
                return new Result<List<Enrollment>>(false, "Enrollment List is Empty", []);
            return new Result<List<Enrollment>>(true, "Enrollment List Retrived successfully", _enrollments);
        }

        
    }
}
