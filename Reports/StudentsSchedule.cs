using ForeignLangTutors.Models;
using System.Collections.Generic;

namespace ForeignLangTutorsMVC.Reports
{
    public class StudentsSchedule
    {
        public Students Student { get; set; }

        public List<Classes> Classes { get; set; }

        public StudentsSchedule(Students students, List<Classes> classes)
        {
            Student = students;
            Classes = classes;
        }
    }
}
