using ForeignLangTutors.Models;

namespace ForeignLangTutorsMVC.Statistics
{
    public class TutorStats
    {
        public Tutors Tutor { get; set; }

        public int NumberOfClasses { get; set; }

        public int NumberOfStudents { get; set; }

        public TutorStats(Tutors tutor, int numberOfClasses, int numberOfStudents)
        {
            Tutor = tutor;
            NumberOfClasses = numberOfClasses;
            NumberOfStudents = numberOfStudents;
        }
    }
}
