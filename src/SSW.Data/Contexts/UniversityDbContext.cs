using SSW.Data.Data;
using SSW.Data.Entitties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.Contexts
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext() : base("UniversityConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOptional(s => s.Student)
                .WithRequired(k => k.User);

            modelBuilder.Entity<User>()
                .HasOptional(i => i.Instructor)
                .WithRequired(k => k.User);
                
        }
    }
}
