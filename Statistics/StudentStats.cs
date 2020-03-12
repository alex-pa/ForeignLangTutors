using ForeignLangTutors.Models;

namespace ForeignLangTutorsMVC.Statistics
{
    public class StudentStats
    {
        public Students Student { get; set; }

        public int NumberOfClasses { get; set; }

        public int NumberOfGroups { get; set; }

        public StudentStats(Students student, int numberOfClasses, int numberOfGroups)
        {
            Student = student;
            NumberOfClasses = numberOfClasses;
            NumberOfGroups = numberOfGroups;
        }
    }
}
