using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

public class LeaveTypesController : Controller
{
    private readonly ApplicationDbContext _context;
    public LeaveTypesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // List all leave types
    public IActionResult Index()
    {
        return View(_context.LeaveTypes.ToList());
    }

    // GET: Create Leave Type
    public IActionResult Create()
    {
        return View();
    }

    // POST: Create Leave Type
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(LeaveType leaveType)
    {
        if (ModelState.IsValid)
        {
            _context.LeaveTypes.Add(leaveType);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(leaveType);
    }
}
