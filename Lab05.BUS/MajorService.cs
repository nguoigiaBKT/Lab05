using Lab05.DAL;
using Lab05.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Lab05.BUS
{
    public class MajorService
    {
        public List<Major> GetByFacultyId(int facultyID)
        {
            using (StudentModel context = new StudentModel())
            {
                return context.Major.Where(m => m.FacultyID == facultyID).ToList();
            }
        }
    }
}
