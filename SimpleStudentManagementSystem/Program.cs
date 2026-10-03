using SimpleStudentManagementSystem.Data;
using SimpleStudentManagementSystem.Entities;
using SimpleStudentManagementSystem.Services;

namespace SimpleStudentManagementSystem
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new AppDbContext();

            var studentService = new StudentService(context);
            var courseService = new CourseService(context);
            var enrollmentService = new EnrollmentService(context);
            var instructorService = new InstructorService(context);
            var courseInstructorService = new CourseInstructorService(context);
            
            while(true)
            {
                Console.WriteLine("----------Welcome in Student Courses Management System----------");
                Console.WriteLine("Please choose the process you need from the following: ");
                Console.WriteLine("===================================================================================");
                Console.WriteLine("1. Add new Student");
                Console.WriteLine("2. Update Student Information");
                Console.WriteLine("3. Get Student by Id");
                Console.WriteLine("4. Get Student by name");
                Console.WriteLine("5. Delete Student Using Hard delete");
                Console.WriteLine("6. Add new Course");
                Console.WriteLine("7. Update Course Information");
                Console.WriteLine("8. Get Course by Id");
                Console.WriteLine("9. Delete Course");
                Console.WriteLine("10. Add new Enrollment");
                Console.WriteLine("11. Update Enrollment Grade");
                Console.WriteLine("12. Get Student Courses by Id");
                Console.WriteLine("13. Get Course Students by Id");
                Console.WriteLine("14. Get Avarage Grade for a Course by Id");
                Console.WriteLine("15. Delete Enrollment");
                Console.WriteLine("16. Add new Instructor");
                Console.WriteLine("17. Get Instructor by Id");
                Console.WriteLine("18. Delete Instructor");
                Console.WriteLine("19. Add new CourseInstructor");
                Console.WriteLine("20. Get Course Instructors");
                Console.WriteLine("21. Get Instructor Courses");
                Console.WriteLine("22. Delete CourseInstructor");
                Console.WriteLine("23. Get Students list");
                Console.WriteLine("24. Get Courses list");
                Console.WriteLine("25. Get Enrollments list");
                Console.WriteLine("26. Get Instructors list");
                Console.WriteLine("27. Get CourseInstructor list");
                Console.WriteLine("28. Get Student With Enrollment Information");
                Console.WriteLine("29. Get Deleted Students");
                Console.WriteLine("30. Update Instructor Name");
                Console.WriteLine("31. Get Enrollment by EnrollmentID(Student Id,Course Id)");
                Console.WriteLine("32. Delete Student Using Soft delete <That allow return student>");
                Console.WriteLine("0. Exit!");
                Console.WriteLine("===================================================================================");

                try
                {
                    string? input = Console.ReadLine();
                    int? choice = int.TryParse(input, out int value) ? value : null;
                    if(choice == null || choice < 0 || choice > 32)
                    {
                        Console.WriteLine("Incorrect choice ,choose from 0 to 32");
                    }

                    switch(choice)
                    {
                        case 0:
                            Environment.Exit(0);
                            break;
                        case 1:
                            Console.Write("Enter Student Full name: ");
                            var name = Console.ReadLine();
                            Console.Write("Enter Student Email: ");
                            var email = Console.ReadLine();
                            Console.Write("Enter Student Birth Date(MM/DD/YYYY): ");
                            var date = Console.ReadLine();
                            DateOnly? birthDate = DateOnly.TryParse(date, out DateOnly d) ? d : null;
                            Student student = new Student(name, email, birthDate);
                            var result1 = await studentService.AddStudent(student);
                            Console.WriteLine(result1.Message);

                            break;

                        case 2:
                            Console.Write("Enter student Id: ");
                            var id = Console.ReadLine();
                            int stuId = int.TryParse(id, out int i) ? i : -1;
                            Console.Write("Enter Student Full name: ");
                            var name_ = Console.ReadLine();
                            Console.Write("Enter Student Email: ");
                            var email_ = Console.ReadLine();
                            Console.Write("Enter Student Birth Date: ");
                            var date_ = Console.ReadLine();
                            DateOnly? birthDate_ = DateOnly.TryParse(date_, out DateOnly dat) ? dat : null;
                            Student student_ = new Student(name_, email_, birthDate_);
                            var result2 = await studentService.UpdateStudent(stuId,student_);
                            Console.WriteLine(result2.Message);
                            break;

                        case 3:
                            Console.Write("Enter student Id: ");
                            var id_ = Console.ReadLine();
                            int stuId_ = int.TryParse(id_, out int i_) ? i_ : -1;
                            var result3 = await studentService.GetStudentByID(stuId_);
                            Console.WriteLine(result3.Data);
                            Console.WriteLine(result3.Message);
                            break;

                        case 4:
                            Console.Write("Enter student name: ");
                            var _name = Console.ReadLine();
                            var result4 = await studentService.GetStudentByName(_name);
                            foreach(var st in result4.Data)
                            {
                                Console.WriteLine(st);
                            }
                            Console.WriteLine(result4.Message);
                            break;

                        case 5:
                            Console.Write("Enter student Id: ");
                            var _id = Console.ReadLine();
                            int _stuId = int.TryParse(_id, out int _i) ? _i : -1;
                            var result5 = await studentService.HardDeleteStudent(_stuId);
                            Console.WriteLine(result5.Message);
                            break;

                        case 6:
                            Console.Write("Enter Course Title: ");
                            var title = Console.ReadLine();
                            Console.Write("Enter Course Credits: ");
                            var cre = Console.ReadLine();
                            var credits = int.TryParse(cre,out int cr)? cr : -1;
                            Console.Write("Enter Course Description: ");
                            var des = Console.ReadLine();
                            var course = new Course(title, credits, des);
                            var result6 = await courseService.AddCourse(course);
                            Console.WriteLine(result6.Message);
                            break;

                        case 7:
                            Console.Write("Enter Course Id: ");
                            var crid = Console.ReadLine();
                            var crId = int.TryParse(crid, out int c) ? c : -1;
                            Console.Write("Enter Course Title: ");
                            var title_ = Console.ReadLine();
                            Console.Write("Enter Course Credits: ");
                            var cre_ = Console.ReadLine();
                            var credits_ = int.TryParse(cre_, out int cr_) ? cr_ : -1;
                            Console.Write("Enter Course Description: ");
                            var des_ = Console.ReadLine();
                            var course_ = new Course(title_, credits_, des_);
                            var result7 = await courseService.UpdateCourse(crId,course_);
                            Console.WriteLine(result7.Message);
                            break;

                        case 8:
                            Console.Write("Enter Course Id: ");
                            var crid_ = Console.ReadLine();
                            var crId_ = int.TryParse(crid_, out int c_) ? c_ : -1;
                            var result8 = await courseService.GetCourseById(crId_);
                            Console.WriteLine(result8.Data);
                            Console.WriteLine(result8.Message);
                            break;

                        case 9:
                            Console.Write("Enter Course Id: ");
                            var _crid = Console.ReadLine();
                            var _crId = int.TryParse(_crid, out int _c) ? _c : -1;
                            var result9 = await courseService.DeleteCourse(_crId);
                            Console.WriteLine(result9.Message);
                            break;

                        case 10:
                            Console.Write("Enter Student Id: ");
                            var sid = Console.ReadLine();
                            var sId = int.TryParse(sid, out int si) ? si : -1;
                            Console.Write("Enter Course Id: ");
                            var cid = Console.ReadLine();
                            var cId = int.TryParse(cid, out int ci) ? ci : -1;
                            Console.Write("Enter Student Grade if Exist OR null if Doesn't receive it: ");
                            var sgrade = Console.ReadLine();
                            int? sGrade = int.TryParse(sgrade, out int sg) ? sg : null;
                            var enrollment = new Enrollment(sId, cId, sGrade);
                            var result10 = await enrollmentService.AddEnrollment(enrollment);
                            Console.WriteLine(result10.Message);
                            break;

                        case 11:
                            Console.Write("Enter Student Id: ");
                            var sid_ = Console.ReadLine();
                            var sId_ = int.TryParse(sid_, out int si_) ? si_ : -1;
                            Console.Write("Enter Course Id: ");
                            var cid_ = Console.ReadLine();
                            var cId_ = int.TryParse(cid_, out int ci_) ? ci_ : -1;
                            Console.Write("Enter Student Grade: ");
                            var sgrade_ = Console.ReadLine();
                            var sGrade_ = int.TryParse(sgrade_, out int sg_) ? sg_ : -1;
                            var enrollment_ = new Enrollment(sId_, cId_, sGrade_);
                            var result11 = await enrollmentService.UpdateEnrollement(enrollment_);
                            Console.WriteLine(result11.Message);
                            break;

                        case 12:
                            Console.Write("Enter Student Id: ");
                            var studentid = Console.ReadLine();
                            var studentId = int.TryParse(studentid, out int stud) ? stud : -1;
                            var result12 = await enrollmentService.GetStudentEnrollments(studentId);
                            foreach(var stcourse  in result12.Data)
                            {
                                Console.WriteLine($"{stcourse} | Course Details: {stcourse.Course}");
                            }
                            Console.WriteLine(result12.Message);
                            break;

                        case 13:
                            Console.Write("Enter Course Id: ");
                            var courseid = Console.ReadLine();
                            var courseId = int.TryParse(courseid, out int cour) ? cour : -1;
                            var result13 = await enrollmentService.GetCourseEnrollments(courseId);
                            foreach (var crstu in result13.Data)
                            {
                                Console.WriteLine($"{crstu} | Student Details: {crstu.Student}");
                            }
                            Console.WriteLine(result13.Message);
                            break;

                        case 14:
                            Console.Write("Enter Course Id: ");
                            var courseid_ = Console.ReadLine();
                            var courseId_ = int.TryParse(courseid_, out int cour_) ? cour_ : -1;
                            var result14 = await enrollmentService.CourseAverageGrade(courseId_);
                            Console.WriteLine(result14.Message);
                            break;

                        case 15:
                            Console.Write("Enter Student Id: ");
                            var _sid = Console.ReadLine();
                            var _sId = int.TryParse(_sid, out int _si) ? _si : -1;
                            Console.Write("Enter Course Id: ");
                            var _cid = Console.ReadLine();
                            var _cId = int.TryParse(_cid, out int _ci) ? _ci : -1;
                            var result15 = await enrollmentService.DeleteEnrollment(_sId, _cId);
                            Console.WriteLine(result15.Message);
                            break;

                        case 16:
                            Console.Write("Enter Instructor Name: ");
                            var insName = Console.ReadLine();
                            var instructor = new Instructor(insName);
                            var result16 = await instructorService.AddInstructor(instructor);
                            Console.WriteLine(result16.Message);
                            break;

                        case 17:
                            Console.Write("Enter Instructor Id: ");
                            var insid = Console.ReadLine();
                            var insId = int.TryParse(insid, out int inid) ? inid : -1;
                            var result17 = await instructorService.GetInstructorById(insId);
                            Console.WriteLine(result17.Message);
                            break;

                        case 18:
                            Console.Write("Enter Instructor Id: ");
                            var insid_ = Console.ReadLine();
                            var insId_ = int.TryParse(insid_, out int inid_) ? inid_ : -1;
                            var result18 = await instructorService.DeleteInstructor(insId_);
                            Console.WriteLine(result18.Message);
                            break;

                        case 19:
                            Console.Write("Enter Instructor Id: ");
                            var instructorid = Console.ReadLine();
                            var instructorId = int.TryParse(instructorid, out int inId) ? inId : -1;
                            Console.Write("Enter Course Id: ");
                            var courid = Console.ReadLine();
                            var courId = int.TryParse(courid, out int corId) ? corId : -1;
                            var courseInstructor = new CourseInstructor(instructorId, courId);
                            var result19 = await courseInstructorService.AddCourseInstructor(courseInstructor);
                            Console.WriteLine(result19.Message);
                            break;

                        case 20:
                            Console.Write("Enter Course Id: ");
                            var courid_ = Console.ReadLine();
                            var courId_ = int.TryParse(courid_, out int corId_) ? corId_ : -1;
                            var result20 = await courseInstructorService.GetCourseInstructors(courId_);
                            foreach(var inst in result20.Data)
                            {
                                Console.WriteLine(inst);
                            }
                            Console.WriteLine(result20.Message);
                            break;


                        case 21:
                            Console.Write("Enter Instructor Id: ");
                            var instructorid_ = Console.ReadLine();
                            var instructorId_ = int.TryParse(instructorid_, out int inId_) ? inId_ : -1;
                            var result21 = await courseInstructorService.GetInstructorCourses(instructorId_);
                            foreach(var _course in result21.Data)
                            {
                                Console.WriteLine(_course);
                            }
                            Console.WriteLine(result21.Message);
                            break;

                        case 22:
                            Console.Write("Enter Instructor Id: ");
                            var _instructorid = Console.ReadLine();
                            var _instructorId = int.TryParse(_instructorid, out int _inId) ? _inId : -1;
                            Console.Write("Enter Course Id: ");
                            var _courid = Console.ReadLine();
                            var _courId = int.TryParse(_courid, out int _corId) ? _corId : -1;
                            var _courseInstructor = new CourseInstructor(_instructorId, _courId);
                            var result22 = await courseInstructorService.DeleteCourseInstructor(_courseInstructor);
                            Console.WriteLine(result22.Message);
                            break;

                        case 23:
                            Console.WriteLine("All Students in our system: ");
                            var result23 = await studentService.GetStudentsList();
                            foreach(var _student in result23.Data)
                            {
                                Console.WriteLine(_student);
                            }
                            Console.WriteLine(result23.Message);
                            break;

                        case 24:
                            Console.WriteLine("All Courses in our system: ");
                            var result24 = await courseService.GetCourseList();
                            foreach (var _course in result24.Data)
                            {
                                Console.WriteLine(_course);
                            }
                            Console.WriteLine(result24.Message);
                            break;

                        case 25:
                            Console.WriteLine("All Enrollments in our system: ");
                            var result25 = await enrollmentService.GetEnrollments();
                            foreach (var _enrollment in result25.Data)
                            {
                                Console.WriteLine(_enrollment);
                            }
                            Console.WriteLine(result25.Message);
                            break;

                        case 26:
                            Console.WriteLine("All Instructors in our system: ");
                            var result26 = await instructorService.GetInstructors();
                            foreach (var _instructor in result26.Data)
                            {
                                Console.WriteLine(_instructor);
                            }
                            Console.WriteLine(result26.Message);
                            break;

                        case 27:
                            Console.WriteLine("All CourseInstructor in our system: ");
                            var result27 = await courseInstructorService.GetCourseInstructorsList();
                            foreach (var _courseinstructor in result27.Data)
                            {
                                Console.WriteLine(_courseinstructor);
                            }
                            Console.WriteLine(result27.Message);
                            break;

                        case 28:
                            Console.WriteLine("Students With Enrollment Info: ");
                            var result28 = await studentService.GetStudentInfo();
                            foreach (var _student in result28.Data)
                            {
                                Console.WriteLine(_student);
                                foreach(var en in _student.Enrollments)
                                {
                                    Console.WriteLine($"Enrollment Date: {en.EnrollmentDate} - Grade: {en.Grade}");
                                }
                            }
                            Console.WriteLine(result28.Message);
                            break;

                        case 29:
                            Console.WriteLine("Deleted Students: ");
                            var result29 = await studentService.GetDeletedStudents();
                            foreach(var _student in result29.Data)
                            {
                                Console.WriteLine(_student);
                            }
                            Console.WriteLine(result29.Message);
                            break;

                        case 30:
                            Console.Write("Enter Instructor Id: ");
                            var insID = Console.ReadLine();
                            int insID_ = int.TryParse(insID, out int ins) ? ins : -1;
                            Console.Write("Enter New Name: ");
                            var newName = Console.ReadLine();
                            var result30 = await instructorService.UpdateInstructor(insID_, newName);
                            Console.WriteLine(result30.Message);
                            break;

                        case 31:
                            Console.Write("Enter Student Id: ");
                            var Studentid = Console.ReadLine();
                            int StudentId = int.TryParse(Studentid, out int Student) ? Student : -1;
                            Console.Write("Enter Course Id: ");
                            var Courseid = Console.ReadLine();
                            int CoursetId = int.TryParse(Courseid, out int Course) ? Course : -1;
                            var result31 = await enrollmentService.GetEnrollmentByID(StudentId, CoursetId);
                            Console.WriteLine(result31.Message);
                            if (result31.IsSuccess && result31.Data != null)
                                Console.WriteLine($"Enrollment Details: {result31.Data} | Student Details: {result31.Data.Student} Course Details: | {result31.Data.Course}");
                            Console.WriteLine(result31.Message);
                            break;

                        case 32:
                            Console.Write("Enter student Id: ");
                            var _Id = Console.ReadLine();
                            int _StuId = int.TryParse(_Id, out int _I) ? _I : -1;
                            var result32 = await studentService.SoftDeleteStudent(_StuId);
                            Console.WriteLine(result32.Message);
                            break;
                    }
                }
                catch(Exception ex) 
                {
                        Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
