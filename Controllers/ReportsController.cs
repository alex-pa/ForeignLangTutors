using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForeignLangTutors.Models;
using ForeignLangTutorsMVC.Reports;

namespace ForeignLangTutorsMVC.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ForeignLangTutorsDBContext _context;

        public ReportsController(ForeignLangTutorsDBContext context)
        {
            _context = context;
        }

        // GET: GroupStudents
        public async Task<IActionResult> GroupStudents()
        {
            var groups = await _context.Groups.ToListAsync();
            var groupStudents = new List<GroupStudents> { };

            foreach (var item in groups)
            {
                var query =  from std in _context.Students
                             join grp in _context.GroupsStudents on std.Id equals grp.StudentId
                             where grp.GroupId == item.Id
                             orderby std.FullName
                             select std;

                groupStudents.Add(new GroupStudents(item, query.ToList()));
            }

            return View(groupStudents);
        }

        // GET: StudentsSchedule
        public async Task<IActionResult> StudentsSchedule()
        {
            var students = await _context.Students.ToListAsync();
            var studentsSchedule = new List<StudentsSchedule> { };

            foreach (var item in students)
            {
                var query = from cls in _context.Classes
                            join grp in _context.Groups on cls.GroupId equals grp.Id
                            join grpStd in _context.GroupsStudents on grp.Id equals grpStd.GroupId
                            where grpStd.StudentId == item.Id
                            orderby cls.BeginningTime
                            select cls;

                var result = query.ToList();
                
                if (!result.Any()) continue;

                studentsSchedule.Add(new StudentsSchedule(item, result));
            }

            return View(studentsSchedule);
        }
    }
}