CREATE DATABASE IF NOT EXISTS todo_app;

USE todo_app;

CREATE TABLE IF NOT EXISTS user (
    id INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS todo (
    id INT AUTO_INCREMENT PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    userId INT,
    FOREIGN KEY (userId) REFERENCES user(id)
);


INSERT INTO user (email, password) VALUES
('mario@example.com', 'password123'),
('joel@example.com', 'password123'),
('markus@example.com', 'password123'),
('pascal@example.com', 'password123'),
('noah@example.com', 'password123'),
('marcel@example.com', 'password123');

-- Insert sample todos
INSERT INTO todo (title, description, userId) VALUES
('Buy groceries', 'Milk, Bread, Eggs, Cheese', 1),
('Finish report', 'Due by Friday, needs final review', 1),
('Book flight', 'Flight to Zürich for conference', 2),
('Read book', 'Start reading "Clean Code"', 3),
('Workout', '45 minutes cardio and strength training', 3),
('Plan vacation', 'Research destinations and budget', 4),
('Call mom', 'Check in and see how she is doing', 5),
('Organize desk', 'Clear clutter and sort papers', 6);