namespace StudentGradeCalculator.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        public string SubjectName { get; set; }
        public float Grade { get; set; }
    }
}
