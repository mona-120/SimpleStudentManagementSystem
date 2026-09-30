# Student Management System

A console application built with **C#**, **.NET**, **LINQ**, and **Entity Framework Core (EF Core)**. The application handles student enrollments, instructors, and course management using modern enterprise software patterns, asynchronous non-blocking operations, and defensive validation.

---

## 🚀 Features

The system allows users to:
* **Add & Update Students:** Applied with strict input validation to guarantee data accuracy before storing records in the database.
* **Search Students by ID or Name:** Supports partial name matching rather than strict exact matching.
* **Soft Delete for Students:** Marks students as deleted (`IsDeleted`) to preserve academic records and permit auditing or recovery.
* **Add & Update Courses:** Validates course properties and credit hours prior to persisting changes.
* **Delete Courses Safely:** Ensures a course contains no active enrolled students before allowing removal.
* **Manage Enrollments:** Registers students to courses after ensuring both entities exist while actively preventing duplicate enrollments.
* **Relational Reporting & Aggregations:** Displays all courses for a given student, lists all students within a course, and computes average course grades via SQL aggregation.
* **Instructor Management:** Full CRUD operations for instructors and a **CourseInstructor** join table to manage Many-to-Many relationships between instructors and courses.

---

## 🛠 Key Architectural Patterns

### Result Pattern (`Result<T>`)
Replaces flow-control exceptions with a unified outcome container:
* **`IsSuccess` (bool):** Indicates whether the business operation succeeded.
* **`Message` (string):** User-friendly diagnostic or domain feedback.
* **`Data` (T?):** The underlying domain entity payload (or `null` on failure).

### High-Performance Eager Loading
* Eliminates the **$N+1$ query problem** of lazy loading by eagerly loading relational graphs (`Include` / `ThenInclude`) in single optimized SQL queries.

### Defensive Validation & Resilient Execution
* Enforces input sanitization via `TryParse` to prevent invalid values before touching the database, minimizing unhandled exceptions.
* Uses targeted exception handling (`try-catch`) as a safety net strictly for infrastructure and database connectivity faults.

### Database Configurations via Fluent API
* Decouples data annotations from domain models by centralizing database rules, relationships, cascading behaviors, and query filters using **Fluent API** (`IEntityTypeConfiguration<T>`).

---

## 📂 Project Structure

```text
StudentManagementSystem/
│
├── Common/
│   └── Result.cs                     # Generic Result Pattern implementation (Result<T>)
│
├── Data/
│   ├── Configurations/
│   │   ├── CourseConfig.cs           # Fluent API entity configuration for Course
│   │   ├── CourseInstructorConfig.cs # Fluent API configuration for Course-Instructor relation
│   │   ├── EnrollmentConfig.cs       # Fluent API configuration for Enrollment & grade mapping
│   │   ├── InstructorConfig.cs       # Fluent API configuration for Instructor entity
│   │   └── StudentConfig.cs          # Fluent API configuration (Soft-delete query filter & constraints)
│   └── AppDbContext.cs               # EF Core DbContext & connection pipeline
│
├── Entities/
│   ├── Course.cs                     # Course domain model
│   ├── CourseInstructor.cs           # Join entity linking Courses and Instructors
│   ├── Enrollment.cs                 # Join entity linking Students and Courses with grades
│   ├── Instructor.cs                 # Instructor domain model
│   └── Student.cs                    # Student entity (includes IsDeleted soft-delete flag)
│
├── Migrations/                       # Database schema versions and migration snapshots
│
├── Services/
│   ├── CourseInstructorService.cs    # Assignment & mapping operations for course instructors
│   ├── CoursesService.cs             # Course catalog management & business rules
│   ├── EnrollmentService.cs          # Enrollment validation, grading, and server-side aggregation
│   ├── InstructorService.cs          # Instructor profile operations
│   └── StudentService.cs             # Student CRUD, validation, and soft-delete/filter bypass
│
└── Program.cs                        # Interactive CLI loop, entry point & dependency orchestration
