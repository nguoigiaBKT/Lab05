using Lab05.DAL;
using Lab05.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Data.Entity;

namespace Lab05.BUS
{
    public class StudentService
    {
        public List<Student> GetAll()
        {
            using (var context = new StudentModel())
            {
                return context.Student
                              .Include(s => s.Faculty)
                              .Include(s => s.Major)
                              .ToList();
            }
        }


        public List<Student> GetAllHasNoMajor(int facultyID)
        {
            using (var context = new StudentModel())
            {
                return context.Student
                              .Include(s => s.Faculty)
                              .Include(s => s.Major)
                              .Where(s => s.FacultyID == facultyID && s.MajorID == null)
                              .ToList();
            }
        }






        public Student FindById(string studentId)
        {
            using (StudentModel context = new StudentModel())
            {
                return context.Student.Include("Faculty").Include("Major").FirstOrDefault(p => p.StudentID == studentId);
            }
        }

        public void InsertOrUpdate(Student s)
        {
            using (StudentModel context = new StudentModel())
            {
                context.Student.AddOrUpdate(s);
                context.SaveChanges();
            }
        }

        public void Delete(Student s)
        {
            using (StudentModel context = new StudentModel())
            {
                context.Student.Attach(s); 
                context.Student.Remove(s);
                context.SaveChanges();
            }
        }
    }
}


