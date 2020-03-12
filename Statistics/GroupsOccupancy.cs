using ForeignLangTutors.Models;

namespace ForeignLangTutorsMVC.Statistics
{
    public class GroupsOccupancy
    {
        public Groups Group { get; set; }

        public int GroupTotalStudents { get; set; }

        public int GroupVacancies { get; set; }

        public double OccupationRate { get; set; }

        public GroupsOccupancy(Groups group ,int groupTotalStudents)
        {
            Group = group;
            GroupTotalStudents = groupTotalStudents;
            GroupVacancies = Group.NumberOfStudents - GroupTotalStudents;
            OccupationRate = ((double)GroupTotalStudents / (double)Group.NumberOfStudents) * 100;
        }
    }
}
