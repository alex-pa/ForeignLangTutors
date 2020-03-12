using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForeignLangTutors.Models;
using ForeignLangTutorsMVC.ModelsView;

namespace ForeignLangTutorsMVC.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ForeignLangTutorsDBContext _context;

        public GroupsController(ForeignLangTutorsDBContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var foreignLangTutorsDBContext = _context.Groups.Include(g => g.Tutor);
            return View(await foreignLangTutorsDBContext.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups
                .Include(g => g.Tutor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groups == null)
            {
                return NotFound();
            }

            return View(groups);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            SelectData selectData = new SelectData();
            ViewBag.Languages = new SelectList(selectData.languages);
            ViewBag.Levels = new SelectList(selectData.levels);
            ViewData["TutorId"] = new SelectList(_context.Tutors, "Id", "FullName");

            return View();
        }

        // POST: Groups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TutorId,Language,Level,NumberOfStudents")] Groups groups)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groups);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TutorId"] = new SelectList(_context.Tutors, "Id", "FullName", groups.TutorId);
            return View(groups);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups.FindAsync(id);
            if (groups == null)
            {
                return NotFound();
            }

            SelectData selectData = new SelectData();
            ViewBag.Languages = new SelectList(selectData.languages);
            ViewBag.Levels = new SelectList(selectData.levels);

            ViewData["TutorId"] = new SelectList(_context.Tutors, "Id", "FullName", groups.TutorId);
            return View(groups);
        }

        // POST: Groups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TutorId,Language,Level,NumberOfStudents")] Groups groups)
        {
            if (id != groups.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groups);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupsExists(groups.Id))
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
            ViewData["TutorId"] = new SelectList(_context.Tutors, "Id", "FullName", groups.TutorId);
            return View(groups);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups
                .Include(g => g.Tutor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groups == null)
            {
                return NotFound();
            }

            return View(groups);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groups = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(groups);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupsExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
