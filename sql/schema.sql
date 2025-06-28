-- sql/schema.sql --
CREATE DATABASE IF NOT EXISTS grade_calculator;
USE grade_calculator;

CREATE TABLE IF NOT EXISTS teachers (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) UNIQUE,
    password VARCHAR(255)
);

CREATE TABLE IF NOT EXISTS students (
    id INT AUTO_INCREMENT PRIMARY KEY,
    student_id VARCHAR(20) UNIQUE,
    full_name VARCHAR(100)
);

CREATE TABLE IF NOT EXISTS grades (
    id INT AUTO_INCREMENT PRIMARY KEY,
    student_id VARCHAR(20),
    subject VARCHAR(50),
    grade FLOAT,
    FOREIGN KEY (student_id) REFERENCES students(student_id)
);

CREATE TABLE IF NOT EXISTS computation_log (
    id INT AUTO_INCREMENT PRIMARY KEY,
    student_id VARCHAR(20),
    computed_average FLOAT,
    prediction VARCHAR(20),
    computed_at DATETIME DEFAULT CURRENT_TIMESTAMP
);
