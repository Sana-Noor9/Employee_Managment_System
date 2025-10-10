using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class DesignationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DesignationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================== Index ==================
        public async Task<IActionResult> Index()
        {
            var designations = _context.Designations
                .Include(d => d.Department);   // Include Department Name
            return View(await designations.ToListAsync());
        }

        // ================== Details ==================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var designation = await _context.Designations
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.DesignationId == id);

            if (designation == null) return NotFound();

            return View(designation);
        }

        // ================== Create (GET) ==================
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // ================== Create (POST) ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Designation designation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(designation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", designation.DepartmentId);
            return View(designation);
        }

        // ================== Edit (GET) ==================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var designation = await _context.Designations.FindAsync(id);
            if (designation == null) return NotFound();

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", designation.DepartmentId);
            return View(designation);
        }

        // ================== Edit (POST) ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Designation designation)
        {
            if (id != designation.DesignationId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(designation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesignationExists(designation.DesignationId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", designation.DepartmentId);
            return View(designation);
        }

        // ================== Delete (GET) ==================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var designation = await _context.Designations
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.DesignationId == id);

            if (designation == null) return NotFound();

            return View(designation);
        }

        // ================== Delete (POST) ==================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var designation = await _context.Designations.FindAsync(id);
            if (designation != null)
            {
                _context.Designations.Remove(designation);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // ================== Exists Check ==================
        private bool DesignationExists(int id)
        {
            return _context.Designations.Any(e => e.DesignationId == id);
        }
    }
}
