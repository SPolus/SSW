namespace SSW.Data.Entities
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public Grade? Grade { get; set; }
    }

    public enum Grade
    {
        A = 5,
        B = 4,
        C = 3,
        D = 2,
        F = 1
    }
}
