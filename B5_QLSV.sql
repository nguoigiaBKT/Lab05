CREATE DATABASE B5_QLSV
USE B5_QLSV

DROP DATABASE B5_QLSV



CREATE TABLE Faculty (
    FacultyID INT PRIMARY KEY,
    FacultyName NVARCHAR(255) NOT NULL
)


CREATE TABLE Major (
    FacultyID INT NOT NULL,
    MajorID INT NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    PRIMARY KEY (FacultyID, MajorID),
    FOREIGN KEY (FacultyID) REFERENCES Faculty(FacultyID)
)


CREATE TABLE Student (
    StudentID NVARCHAR(10) PRIMARY KEY,
    FullName NVARCHAR(255) NOT NULL,
    AverageScore FLOAT NOT NULL,
    FacultyID INT NULL,
    MajorID INT NULL,
    Avatar NVARCHAR(255),
    FOREIGN KEY (FacultyID) REFERENCES Faculty(FacultyID),
    FOREIGN KEY (FacultyID, MajorID) REFERENCES Major(FacultyID, MajorID)
)

INSERT INTO Faculty (FacultyID, FacultyName)
VALUES 
(1, N'Công nghệ thông tin'),
(2, N'Ngôn Ngữ Anh'),
(3, N'Quản Trị Kinh Doanh');


INSERT INTO Major (FacultyID, MajorID, Name)
VALUES 
(1, 1, N'Công nghệ phần mềm'),
(2, 1, N'Tiếng Anh Thương Mại'),
(1, 2, N'Hệ thống thông tin'),
(2, 2, N'Tiếng Anh Truyền Thông'),
(1, 3, N'An toàn thông tin')




INSERT INTO Student (StudentID, FullName, AverageScore, FacultyID, MajorID)
VALUES
('1234567890', N'Nguyễn Văn B', 7.5, 1, 1),
('1234567891', N'Nguyễn Văn A', 4.5, 1, 2)



SELECT * FROM dbo.Faculty

SELECT * FROM dbo.Major


SELECT * FROM dbo.Student


