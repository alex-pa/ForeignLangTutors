using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForeignLangTutors.Models;

namespace ForeignLangTutorsMVC.Controllers
{
    public class AutomatizationController : Controller
    {
        private readonly ForeignLangTutorsDBContext _context;

        public AutomatizationController(ForeignLangTutorsDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Automatization/DeleteOutdatedClasses
        public async Task<IActionResult> DeleteOutdatedClasses()
        {
            var query = from cls in _context.Classes
                        where cls.СompletionTime < DateTime.Now
                        select cls;

            var result = new List<Classes> { };
            result = query.ToList();

            return View(result);
        }

        // POST: Automatization/DeleteOutdatedClasses
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOutdatedClassesConfirmed()
        {
            var query = from cls in _context.Classes
                        where cls.СompletionTime < DateTime.Now
                        select cls;

            var result = new List<Classes> { };  
            result = query.ToList();
            foreach (var item in result)
            {
                _context.Classes.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}