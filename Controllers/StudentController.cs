using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using StudentGradeCalculator.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentGradeCalculator.Controllers
{
    public class StudentController : Controller
    {
        private readonly MySqlConnection _conn;

        public StudentController(MySqlConnection conn)
        {
            _conn = conn;
        }

        // GET: /Student/
        public IActionResult Index()
        {
            List<Student> students = new();
            _conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM students", _conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                students.Add(new Student
                {
                    StudentId = reader.GetInt32("student_id"),
                    FullName = reader.GetString("full_name")
                });
            }
            _conn.Close();
            return View(students);
        }

        // GET: /Student/Add
        public IActionResult Add() => View();

        // POST: /Student/Add
        [HttpPost]
        public IActionResult Add(Student student)
        {
            _conn.Open();
            var cmd = new MySqlCommand("INSERT INTO students (full_name) VALUES (@name)", _conn);
            cmd.Parameters.AddWithValue("@name", student.FullName);
            cmd.ExecuteNonQuery();
            _conn.Close();
            return RedirectToAction("Index");
        }

        // GET: /Student/Grades/{id}
        public IActionResult Grades(int id)
        {
            ViewBag.StudentId = id;
            return View();
        }

        // POST: /Student/Grades/{id}
        [HttpPost]
        public IActionResult Grades(int id, string subjectName, float grade)
        {
            _conn.Open();
            var cmd = new MySqlCommand("INSERT INTO subjects (student_id, subject_name, grade) VALUES (@id, @subject, @grade)", _conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@subject", subjectName);
            cmd.Parameters.AddWithValue("@grade", grade);
            cmd.ExecuteNonQuery();
            _conn.Close();

            return RedirectToAction("Grades", new { id });
        }

        // GET: /Student/Log/{id}
        public IActionResult Log(int id)
        {
            List<float> grades = new();

            _conn.Open();
            var cmd = new MySqlCommand("SELECT grade FROM subjects WHERE student_id = @id", _conn);
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                grades.Add(reader.GetFloat("grade"));
            }
            _conn.Close();

            if (grades.Count == 0)
            {
                ViewBag.Message = "No grades found.";
                return View();
            }

            float average = grades.Average();
            bool isDeansList = average >= 90;

            // Save to computation_log
            _conn.Open();
            var logCmd = new MySqlCommand(
                "INSERT INTO computation_log (student_id, computed_at, average_grade, is_deans_list) VALUES (@id, NOW(), @avg, @isdl)",
                _conn
            );
            logCmd.Parameters.AddWithValue("@id", id);
            logCmd.Parameters.AddWithValue("@avg", average);
            logCmd.Parameters.AddWithValue("@isdl", isDeansList);
            logCmd.ExecuteNonQuery();
            _conn.Close();

            ViewBag.Average = average;
            ViewBag.DeansList = isDeansList;
            return View();
        }
    }
}
