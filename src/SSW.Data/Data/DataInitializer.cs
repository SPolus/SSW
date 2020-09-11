using SSW.Data.Contexts;
using SSW.Data.Entitties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.Data
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<UniversityDbContext>
    {
        protected override void Seed(UniversityDbContext context)
        {
            var students = new List<Student>
            {
                new Student { FirstName = "Pavel", LastName = "Solomin" }
            };

            students.ForEach(s => context.Students.Add(s));

            var instructors = new List<Instructor>
            {
                new Instructor { FirstName = "Ruslan", LastName = "Zavyalov" }
            };

            instructors.ForEach(i => context.Instructors.Add(i));

            var courses = new List<Course>
            {
                new Course { Name = "Become a fullstack developer" }
            };

            courses.ForEach(c => context.Courses.Add(c));

            context.SaveChanges();


            var enrollments = new List<Enrollment>
            {
                new Enrollment
                {
                     StudentId = students.Single(s => s.FirstName == "Pavel").Id,
                     CourseId = courses.Single(c => c.Name == "Become a fullstack developer").Id,
                     Grade = Grade.C
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
                    InstructorId = instructors.Single(i => i.FirstName == "Ruslan").Id,
                    CourseId = courses.Single(c => c.Name == "Become a fullstack developer").Id
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
