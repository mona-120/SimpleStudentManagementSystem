using Microsoft.EntityFrameworkCore;
using SimpleStudentManagementSystem.Common;
using SimpleStudentManagementSystem.Data;
using SimpleStudentManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStudentManagementSystem.Services
{
    public class CourseInstructorService
    {
        private readonly AppDbContext _context;
        public CourseInstructorService(AppDbContext context)
        {
            _context = context;
        }

        // add CourseInstructor
        public async Task<Result<CourseInstructor>> AddCourseInstructor(CourseInstructor courseInstructor)
        {
            bool instructor = await _context.Instructors.AnyAsync(i => i.InstructorId == courseInstructor.InstructorId);
            bool course = await _context.Courses.AnyAsync(c => c.CourseId == courseInstructor.CourseId);

            if (!course)
                return new Result<CourseInstructor>(false, "Invalid input, Course doesn't exist", courseInstructor);

            if (!instructor)
                return new Result<CourseInstructor>(false, "Invalid input, Instructor doesn't exist", courseInstructor);

            if (await _context.CourseInstructors.AnyAsync(ci => ci.InstructorId == courseInstructor.InstructorId
                                  && ci.CourseId == courseInstructor.CourseId))
                return new Result<CourseInstructor>(false, "Invalid input, record Already exist", courseInstructor);

            await _context.AddAsync(courseInstructor);
            await _context.SaveChangesAsync();
            return new Result<CourseInstructor>(true, "Record added successfully", courseInstructor);
        }

        // Get Instructor Courses
        public async Task<Result<List<CourseInstructor>>> GetInstructorCourses(int insId)
        {
            List<CourseInstructor> courses = await _context.CourseInstructors.Where(ci => ci.InstructorId == insId)
                           .AsNoTracking().ToListAsync();
            if (courses.Count == 0)
                return new Result<List<CourseInstructor>>(false, "Instructor has no courses", []);
            return new Result<List<CourseInstructor>>(true, "Found Instructor courses", courses);
        }

        // get Course Instructors
        public async Task<Result<List<CourseInstructor>>> GetCourseInstructors(int crId)
        {
            List<CourseInstructor> Instructors = await _context.CourseInstructors.Where(ci => ci.CourseId == crId)
                           .AsNoTracking().ToListAsync();
            if (Instructors.Count == 0)
                return new Result<List<CourseInstructor>>(false, "course has no Instructors", []);
            return new Result<List<CourseInstructor>>(true, "Found course Instructors", Instructors);
        }

        // delete CourseInstructor
        public async Task<Result<CourseInstructor>> DeleteCourseInstructor(CourseInstructor courseInstructor)
        {
            if (!await _context.CourseInstructors.AnyAsync(ci => ci.InstructorId == courseInstructor.InstructorId
                                     && ci.CourseId == courseInstructor.CourseId))
                return new Result<CourseInstructor>(false, "Record doesn't exist", courseInstructor);

            _context.CourseInstructors.Remove(courseInstructor);
            await _context.SaveChangesAsync();
            return new Result<CourseInstructor>(true, "Record deleted successfully", courseInstructor);
        }

        // get CourseInstructor list
        public async Task<Result<List<CourseInstructor>>> GetCourseInstructorsList()
        {
            List<CourseInstructor> courseInstructors = await _context.CourseInstructors.AsNoTracking().ToListAsync();
            if (courseInstructors.Count == 0)
                return new Result<List<CourseInstructor>>(false, "CourseInstructor List is Empty", []);
            return new Result<List<CourseInstructor>>(true, "CourseInstructor List Retrived successfully", courseInstructors);
        }
    }
}
