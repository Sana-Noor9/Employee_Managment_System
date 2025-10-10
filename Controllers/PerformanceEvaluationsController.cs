using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeManagement.Controllers
{
    public class PerformanceEvaluationsController : BaseController
    {
        public PerformanceEvaluationsController(ApplicationDbContext context) : base(context)
        {
        }

        // =================== ADMIN / HR ===================

        public async Task<IActionResult> Index()
        {
            var evaluations = _context.PerformanceEvaluations
                .Include(p => p.User);
            return View(await evaluations.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var evaluation = await _context.PerformanceEvaluations
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.EvaluationId == id);

            if (evaluation == null) return NotFound();

            return View(evaluation);
        }

        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Date,Score,Comments")] PerformanceEvaluation evaluation)
        {
            if (ModelState.IsValid)
            {
                if (evaluation.Date == default)
                    evaluation.Date = DateTime.Now;

                _context.PerformanceEvaluations.Add(evaluation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName");
            return View(evaluation);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var evaluation = await _context.PerformanceEvaluations.FindAsync(id);
            if (evaluation == null) return NotFound();

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", evaluation.UserId);
            return View(evaluation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EvaluationId,UserId,Date,Score,Comments")] PerformanceEvaluation evaluation)
        {
            if (id != evaluation.EvaluationId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.PerformanceEvaluations.Any(e => e.EvaluationId == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", evaluation.UserId);
            return View(evaluation);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var evaluation = await _context.PerformanceEvaluations
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.EvaluationId == id);

            if (evaluation == null) return NotFound();

            return View(evaluation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evaluation = await _context.PerformanceEvaluations.FindAsync(id);
            if (evaluation != null)
            {
                _context.PerformanceEvaluations.Remove(evaluation);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // =================== EMPLOYEE ===================
        public async Task<IActionResult> MyEvaluations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var evaluations = await _context.PerformanceEvaluations
                .Include(e => e.User)
                .Where(e => e.UserId == userId)
                .ToListAsync();

            return View(evaluations);
        }
    }
}
