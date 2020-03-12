using ForeignLangTutors.Models;
using System;

namespace ForeignLangTutorsMVC.Statistics
{
    public class RoomStats
    {
        public Rooms Room { get; set; }

        public int NumberOfClasses { get; set; }

        public DateTime DateOfFirstClass { get; set; }

        public RoomStats(Rooms room, int numberOfClasses, DateTime dateOfFirstClass)
        {
            Room = room;
            NumberOfClasses = numberOfClasses;
            DateOfFirstClass = dateOfFirstClass;
        }

        public RoomStats(Rooms room, int numberOfClasses)
        {
            Room = room;
            NumberOfClasses = numberOfClasses;
            DateOfFirstClass = default;
        }
    }
}
