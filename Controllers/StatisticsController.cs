using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForeignLangTutors.Models;
using ForeignLangTutorsMVC.Statistics;

namespace ForeignLangTutorsMVC.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ForeignLangTutorsDBContext _context;

        public StatisticsController(ForeignLangTutorsDBContext context)
        {
            _context = context;
        }

        // GET: GroupsOccupancy
        public async Task<IActionResult> GroupsOccupancy()
        {
            var groups = await _context.Groups.ToListAsync();
            var groupsOccupancy = new List<GroupsOccupancy> { };

            foreach (var item in groups)
            {
                var query = from grp in _context.Groups
                            join stdGrp in _context.GroupsStudents on grp.Id equals stdGrp.GroupId
                            where grp.Id == item.Id 
                            select stdGrp;

                groupsOccupancy.Add(new GroupsOccupancy(item, query.ToList().Count));
            }

            return View(groupsOccupancy);
        }

        // GET: StudentStats
        public async Task<IActionResult> StudentStats()
        {
            var students = await _context.Students.ToListAsync();
            var studentStats = new List<StudentStats> { };

            foreach (var item in students)
            {
                var query1 = from cls in _context.Classes
                             join grp in _context.Groups on cls.GroupId equals grp.Id
                             join grpStd in _context.GroupsStudents on grp.Id equals grpStd.GroupId
                             where grpStd.StudentId == item.Id
                             select cls;

                var query2 = from grp in _context.GroupsStudents
                             where grp.StudentId == item.Id
                             select grp;

                studentStats.Add(new StudentStats(item, query1.ToList().Count, query2.ToList().Count));
            }

            return View(studentStats);
        }

        // GET: TutorStats
        public async Task<IActionResult> TutorStats()
        {
            var tutors = await _context.Tutors.ToListAsync();
            var tutorStats = new List<TutorStats> { };

            foreach (var item in tutors)
            {
                var query1 = from cls in _context.Classes
                             join grp in _context.Groups on cls.GroupId equals grp.Id
                             where grp.TutorId == item.Id
                             select cls;

                var query2 = from grp in _context.Groups
                             join stdGrp in _context.GroupsStudents on grp.Id equals stdGrp.GroupId
                             where grp.TutorId == item.Id
                             select stdGrp;

                tutorStats.Add(new TutorStats(item, query1.ToList().Count, query2.ToList().Count));
            }

            return View(tutorStats);
        }

        // GET: RoomStats
        public async Task<IActionResult> RoomStats()
        {
            var rooms = await _context.Rooms.ToListAsync();
            var roomStats = new List<RoomStats> { };

            foreach (var item in rooms)
            {
                var query = from rm in _context.Rooms
                             join cls in _context.Classes on rm.Id equals cls.RoomId
                             where cls.RoomId == item.Id
                             orderby cls.BeginningTime
                             select cls;

                if (!query.ToList().Any())
                {
                    roomStats.Add(new RoomStats(item, 0));
                    continue;
                }

                roomStats.Add(new RoomStats(item, query.ToList().Count, query.ToList().First().BeginningTime));
            }

            return View(roomStats);
        }
    }
}