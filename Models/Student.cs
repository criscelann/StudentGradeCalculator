namespace StudentGradeCalculator.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }

        // Navigation Property (optional for future use)
        public List<Subject> Subjects { get; set; }
    }
}
