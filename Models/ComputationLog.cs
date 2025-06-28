using System;

namespace StudentGradeCalculator.Models
{
    public class ComputationLog
    {
        public int LogId { get; set; }
        public int StudentId { get; set; }
        public DateTime ComputedAt { get; set; }
        public float AverageGrade { get; set; }
        public bool IsDeansList { get; set; }
    }
}
