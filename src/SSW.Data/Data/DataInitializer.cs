using SSW.Data.Contexts;
using SSW.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SSW.Data.Data
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<UniversityDbContext> //DropCreateDatabaseAlways<UniversityDbContext>// 
    {
        protected override void Seed(UniversityDbContext context)
        {
            var users = new List<User>
            {
                new User { Email = "fullstack@mail.com", Password = "fullstack", FirstName = "Full", LastName = "Stack" },
                new User { Email = "billgates@mail.com", Password = "billgates", FirstName = "Bill", LastName = "Gates" },
                new User { Email = "stevejobs@mail.com", Password = "stevejobs", FirstName = "Steve", LastName = "Jobs" },
                new User { Email = "pavel@mail.com", Password = "pavelsolomin", FirstName = "Pavel", LastName = "Solomin"},
                new User { Email = "ivan@mail.com", Password = "ivanivanov", FirstName = "Ivan", LastName = "Ivanov"},
                new User { Email = "peter@mail.com", Password = "peterpetrov", FirstName = "Peter", LastName = "Petrov" },
                new User { Email = "john@mail.com", Password = "johnsmith", FirstName = "John", LastName = "Smith" }
            };

            context.SaveChanges();

            var students = new List<Student>
            {
                new Student { User = users.Single(e => e.Email == "pavel@mail.com") },
                new Student { User = users.Single(e => e.Email == "ivan@mail.com") },
                new Student { User = users.Single(e => e.Email == "peter@mail.com") },
                new Student { User = users.Single(e => e.Email == "john@mail.com") },
            };

            students.ForEach(s => context.Students.Add(s));

            var instructors = new List<Instructor>
            {
                new Instructor { User = users.Single(e => e.Email == "fullstack@mail.com") },
                new Instructor { User = users.Single(e => e.Email == "billgates@mail.com") },
                new Instructor { User = users.Single(e => e.Email == "stevejobs@mail.com") }
            };

            instructors.ForEach(i => context.Instructors.Add(i));

            var courses = new List<Course>
            {
                new Course { Name = "Become a fullstack developer" },
                new Course { Name = "Learn Backbone" },
                new Course { Name = "Introduction to Microsoft" },
                new Course { Name = "Introduction to iOS" }
            };

            courses.ForEach(c => context.Courses.Add(c));

            context.SaveChanges();


            var enrollments = new List<Enrollment>
            {
                new Enrollment
                {
                     StudentId = students.Single(s => s.User.Email == "pavel@mail.com").Id,
                     CourseId = courses.Single(c => c.Name == "Become a fullstack developer").Id,
                     Grade = Grade.C
                },

                new Enrollment
                {
                    StudentId = students.Single(s => s.User.Email == "pavel@mail.com").Id,
                    CourseId = courses.Single(c => c.Name == "Learn Backbone").Id,
                    Grade = Grade.F
                },

                new Enrollment
                {
                    StudentId = students.Single(s => s.User.Email == "ivan@mail.com").Id,
                    CourseId = courses.Single(c => c.Name == "Become a fullstack developer").Id,
                    Grade = Grade.A
                },

                new Enrollment
                {
                    StudentId = students.Single(s => s.User.Email == "ivan@mail.com").Id,
                    CourseId = courses.Single(c => c.Name == "Learn Backbone").Id,
                    //Grade = Grade.A
                },

                new Enrollment
                {
                    StudentId = students.Single(s => s.User.Email == "peter@mail.com").Id,
                    CourseId = courses.Single(c => c.Name == "Become a fullstack developer").Id,
                    Grade = Grade.B
                }
            };

            foreach (var enrollment in enrollments)
            {
                var enrollmentInDatabase = context.Enrollments
                    .Where(s => s.Student.Id == enrollment.StudentId && s.Course.Id == enrollment.CourseId)
                    .FirstOrDefault();

                if (enrollmentInDatabase == null)
                {
                    context.Enrollments.Add(enrollment);
                }
            }

            var courseAssignments = new List<CourseAssignment>
            {
                new CourseAssignment
                {
                    InstructorId = instructors.Single(i => i.User.Email == "fullstack@mail.com").Id,
                    CourseId = courses.Single(c => c.Name == "Become a fullstack developer").Id
                },

                new CourseAssignment
                {
                    InstructorId = instructors.Single(i => i.User.Email == "fullstack@mail.com").Id,
                    CourseId = courses.Single(c => c.Name == "Learn Backbone").Id
                },

                new CourseAssignment
                {
                    InstructorId = instructors.Single(i => i.User.Email == "billgates@mail.com").Id,
                    CourseId = courses.Single(c => c.Name == "Introduction to Microsoft").Id
                },

                new CourseAssignment
                {
                    InstructorId = instructors.Single(i => i.User.Email == "stevejobs@mail.com").Id,
                    CourseId = courses.Single(c => c.Name == "Introduction to iOS").Id
                }
            };

            foreach (var ca in courseAssignments)
            {
                var courseAssignmentsInDatabase = context.CourseAssignments
                    .Where(i => i.Instructor.Id == ca.InstructorId && i.Course.Id == ca.CourseId)
                    .FirstOrDefault();

                if (courseAssignmentsInDatabase == null)
                {
                    context.CourseAssignments.Add(ca);
                }
            }

            context.SaveChanges();
        }
    }
}
