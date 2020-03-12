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
    public class TutorsController : Controller
    {
        private readonly ForeignLangTutorsDBContext _context;

        public TutorsController(ForeignLangTutorsDBContext context)
        {
            _context = context;
        }

        // GET: Tutors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tutors.ToListAsync());
        }

        // GET: Tutors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutors = await _context.Tutors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tutors == null)
            {
                return NotFound();
            }

            return View(tutors);
        }

        // GET: Tutors/Create
        public IActionResult Create()
        {
            SelectData selectData = new SelectData();
            ViewBag.Languages = new SelectList(selectData.languages);
            ViewBag.Levels = new SelectList(selectData.levels);

            return View();
        }

        // POST: Tutors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Adress,Login,Password,CardNumber,Language,Grade,Level")] Tutors tutors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tutors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tutors);
        }

        // GET: Tutors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutors = await _context.Tutors.FindAsync(id);
            if (tutors == null)
            {
                return NotFound();
            }

            SelectData selectData = new SelectData();
            ViewBag.Languages = new SelectList(selectData.languages);
            ViewBag.Levels = new SelectList(selectData.levels);

            return View(tutors);
        }

        // POST: Tutors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Adress,Login,Password,CardNumber,Language,Grade,Level")] Tutors tutors)
        {
            if (id != tutors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tutors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TutorsExists(tutors.Id))
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
            return View(tutors);
        }

        // GET: Tutors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutors = await _context.Tutors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tutors == null)
            {
                return NotFound();
            }

            return View(tutors);
        }

        // POST: Tutors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tutors = await _context.Tutors.FindAsync(id);
            _context.Tutors.Remove(tutors);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TutorsExists(int id)
        {
            return _context.Tutors.Any(e => e.Id == id);
        }
    }
}
