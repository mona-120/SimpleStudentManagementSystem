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
    public class CourseService
    {
        private readonly AppDbContext _context;
        public CourseService(AppDbContext context)
        {
            _context = context;
        }


        // Add course
        public async Task<Result<Course>> AddCourse(Course course)
        {
            if (string.IsNullOrWhiteSpace(course.Title))
                return new Result<Course>(false, "Invalid input ,please enter course Title!", course);

            if (course.Title.Length > 50)
                return new Result<Course>(false, "Invalid input ,Title must be <= 50", course);

            if (await _context.Courses.AnyAsync(c => c.Title == course.Title))
                return new Result<Course>(false, "Invalid Input, Course Already exist", course);

            if (course.Credits < 1)
                return new Result<Course>(false, "Invalid input, course credits must be >= 1", course);

            if (string.IsNullOrWhiteSpace(course.Description))
                return new Result<Course>(false, "Invalid input ,please enter Course Description!", course);

            if (course.Description.Length > 500)
                return new Result<Course>(false, "Invalid input ,Description must be <= 500", course);

            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return new Result<Course>(true, $"Course {course.Title} Added Successfully!",course);
        }


        // Get Course By Id
        public async Task<Result<Course>> GetCourseById(int id)
        {
            Course? course = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(c=> c.CourseId == id);

            if (course == null)
                return new Result<Course>(false, $"Course with Id {id} not found!", course);

            return new Result<Course>(true, $"Course {course.Title} Found With Id {id}",course);
        }


        // Update Course data
        public async Task<Result<Course>> UpdateCourse(int  id, Course course)
        {
            Course? _course = await _context.Courses.FindAsync(id);

            if (_course == null)
                return new Result<Course>(false, $"Course with Id {id} not found!", _course);

            if (_course.Title != course.Title)
            {
                if (string.IsNullOrWhiteSpace(course.Title))
                    return new Result<Course>(false, "Invalid input ,please enter course Title!", course);

                if (course.Title.Length > 50)
                    return new Result<Course>(false, "Invalid input ,Title must be <= 50", course);

                if (await _context.Courses.AnyAsync(c => c.Title == course.Title))
                    return new Result<Course>(false, "Invalid Input, Course Already exist", course);
            }

            if (_course.Credits != course.Credits)
            {
                if (course.Credits < 1)
                    return new Result<Course>(false, "Invalid input, course credits must be >= 1", course);
            }

            if (_course.Description != course.Description)
            {
                if (string.IsNullOrWhiteSpace(course.Description))
                    return new Result<Course>(false, "Invalid input ,please enter Course Description!", course);

                if (course.Description.Length > 500)
                    return new Result<Course>(false, "Invalid input ,Description must be <= 500", course);
            }


            _course.Title = course.Title;
            _course.Description = course.Description;
            _course.Credits = course.Credits;
            await _context.SaveChangesAsync();
            return new Result<Course>(true, $"Update Course {_course.Title} Successfully!",_course);
        }


        // delete a corse
        public async Task<Result<Course>> DeleteCourse(int id)
        {
            Course? course = await _context.Courses.FindAsync(id);

            if (course == null)
                return new Result<Course>(false, $"Course with Id {id} not found!", course);

            if(await _context.Enrollments.AnyAsync(e => e.CourseId == id))
                return new Result<Course>(false, $"Can't delete Course with Id {id} As it contains students", course);

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return new Result<Course>(true, $"Course {course.Title} Deleted Successfully", course); 
        }



        // Return List of Course
          public async Task<Result<List<Course>>> GetCourseList()
        {
            List<Course> courses = await _context.Courses.AsNoTracking().ToListAsync();
            // Process of get courses is 'AsNoTracking()' as it doesn't change data and that save memory resources
            if( courses.Count == 0 )
                return new Result<List<Course>>(false, "Course List is empty", []);

            return new Result<List<Course>>(true, "Courses retrieved successfully", courses);
        }
    }
}
