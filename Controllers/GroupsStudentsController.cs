using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForeignLangTutors.Models;

namespace ForeignLangTutorsMVC.Controllers
{
    public class GroupsStudentsController : Controller
    {
        private readonly ForeignLangTutorsDBContext _context;

        public GroupsStudentsController(ForeignLangTutorsDBContext context)
        {
            _context = context;
        }

        // GET: GroupsStudents
        public async Task<IActionResult> Index()
        {
            var foreignLangTutorsDBContext = _context.GroupsStudents.Include(g => g.Group).Include(g => g.Student);
            return View(await foreignLangTutorsDBContext.ToListAsync());
        }

        // GET: GroupsStudents/Group/5
        public async Task<IActionResult> Group(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!_context.Groups.Where(g => g.Id == id).Any())
            {
                return NotFound();
            }

            var groupStudents = _context.GroupsStudents.Include(g => g.Group).Include(g => g.Student).Where(g => g.GroupId == id);
            
            return View(await groupStudents.ToListAsync());
        }

        // GET: GroupsStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupsStudents = await _context.GroupsStudents
                .Include(g => g.Group)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupsStudents == null)
            {
                return NotFound();
            }

            return View(groupsStudents);
        }

        // GET: GroupsStudents/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName");
            return View();
        }

        // POST: GroupsStudents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,GroupId")] GroupsStudents groupsStudents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupsStudents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", groupsStudents.GroupId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", groupsStudents.StudentId);
            return View(groupsStudents);
        }

        // GET: GroupsStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupsStudents = await _context.GroupsStudents.FindAsync(id);
            if (groupsStudents == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", groupsStudents.GroupId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", groupsStudents.StudentId);
            return View(groupsStudents);
        }

        // POST: GroupsStudents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,GroupId")] GroupsStudents groupsStudents)
        {
            if (id != groupsStudents.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupsStudents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupsStudentsExists(groupsStudents.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", groupsStudents.GroupId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", groupsStudents.StudentId);
            return View(groupsStudents);
        }

        // GET: GroupsStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupsStudents = await _context.GroupsStudents
                .Include(g => g.Group)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupsStudents == null)
            {
                return NotFound();
            }

            return View(groupsStudents);
        }

        // POST: GroupsStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupsStudents = await _context.GroupsStudents.FindAsync(id);
            _context.GroupsStudents.Remove(groupsStudents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupsStudentsExists(int id)
        {
            return _context.GroupsStudents.Any(e => e.Id == id);
        }
    }
}
