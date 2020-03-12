using ForeignLangTutors.Models;
using System.Collections.Generic;

namespace ForeignLangTutorsMVC.Reports
{
    public class GroupStudents
    {
        public Groups Group { get; set; }

        public List<Students> Students { get; set; }

        public GroupStudents(Groups group, List<Students> students)
        {
            Group = group;
            Students = students;
        }
    }
}
