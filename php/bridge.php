// php/bridge.php
<?php
$action = $_POST['action'];

if ($action == 'login') {
    $username = $_POST['username'];
    $password = $_POST['password'];
    $result = shell_exec("GradeApp.exe login $username $password");
    if (trim($result) == 'success') {
        header("Location: ../dashboard.html");
    } else {
        echo "Login failed.";
    }
}

if ($action == 'add_student') {
    $conn = new mysqli("localhost", "root", "", "grade_calculator");
    $stmt = $conn->prepare("INSERT INTO students (student_id, full_name) VALUES (?, ?) ON DUPLICATE KEY UPDATE full_name = VALUES(full_name)");
    $stmt->bind_param("ss", $_POST['student_id'], $_POST['full_name']);
    $stmt->execute();

    $stmt2 = $conn->prepare("INSERT INTO grades (student_id, subject, grade) VALUES (?, ?, ?)");
    $stmt2->bind_param("ssd", $_POST['student_id'], $_POST['subject'], $_POST['grade']);
    $stmt2->execute();

    echo "Student and grade saved.";
}
?>

// php/view_students.php
<?php
$conn = new mysqli("localhost", "root", "", "grade_calculator");
$result = $conn->query("SELECT * FROM students");
echo "<table border='1'><tr><th>Student ID</th><th>Name</th></tr>";
while ($row = $result->fetch_assoc()) {
    echo "<tr><td>{$row['student_id']}</td><td>{$row['full_name']}</td></tr>";
}
echo "</table>";
?>