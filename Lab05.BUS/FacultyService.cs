using Lab05.DAL;
using Lab05.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Lab05.BUS
{
    public class FacultyService
    {
        public List<Faculty> GetAll()
        {
            using (StudentModel context = new StudentModel())
            {
                return context.Faculty.ToList();
            }
        }
    }
}
