using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class LeaveApplicationsController : Controller
{
    private readonly ApplicationDbContext _context;

    public LeaveApplicationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // =================== EMPLOYEE ===================

    // Employee - View My Applications
    public async Task<IActionResult> Index()
    {
        //if (!User.Identity.IsAuthenticated || !User.IsInRole("User"))
        //    return RedirectToAction("Login", "Account");

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var leaves = await _context.LeaveApplications
            .Include(l => l.User)
            .Where(l => l.UserId == userId)
            .OrderByDescending(l => l.FromDate)
            .ToListAsync();

        var balance = await _context.LeaveBalances
            .FirstOrDefaultAsync(b => b.UserId == userId && b.Year == DateTime.Now.Year);

        ViewBag.LeaveBalance = balance;

        return View(leaves);
    }

    // Employee - Apply Leave (GET)
    public IActionResult Create()
    {
        //if (!User.IsInRole("User"))
        //    return RedirectToAction("Login", "Account");

        TempData["UserId"] = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value; // ✅ correct id
  

        ViewBag.LeaveTypes = _context.LeaveTypes.ToList(); // 👈 Dropdown fill
        return View();
    }

    // Employee - Apply Leave (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveApplication leaveApp)
    {
        ModelState.Remove("Year");
        var leaveTypeId = leaveApp.LeaveTypeId;

        //var userId = HttpContext.Session.GetString("UserId"); // ✅ correct id
        //if (!User.Identity.IsAuthenticated)
        //    return RedirectToAction("Login", "Account");

        leaveApp.UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (ModelState.IsValid)
        {
            leaveApp.Status = "Pending";
            leaveApp.Year = DateTime.Now.Year;

            var balance = await _context.LeaveBalances
                .FirstOrDefaultAsync(b => b.UserId == leaveApp.UserId && b.Year == leaveApp.Year);

            if (balance == null)
            {
                balance = new LeaveBalance
                {
                    UserId = leaveApp.UserId,
                    Year = leaveApp.Year,
                    TotalLeaves = 20,
                    UsedLeaves = 0,
                    RemainingLeaves = 20
                };
                _context.LeaveBalances.Add(balance);
            }

            int requestedDays = (leaveApp.ToDate - leaveApp.FromDate).Days + 1;
            if (requestedDays <= 0)
            {
                TempData["Error"] = "Invalid leave period!";
                ViewBag.LeaveTypes = _context.LeaveTypes.ToList();
                return View(leaveApp);
            }

            if (requestedDays > balance.RemainingLeaves)
            {
                TempData["Error"] = $"You only have {balance.RemainingLeaves} leaves left!";
                ViewBag.LeaveTypes = _context.LeaveTypes.ToList();
                return View(leaveApp);
            }

            leaveApp.LeaveDays = requestedDays;
            leaveApp.LeaveTypeId = leaveTypeId;

            _context.LeaveApplications.Add(leaveApp);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Leave applied successfully!";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.LeaveTypes = _context.LeaveTypes.ToList();
        return View(leaveApp);
    }

    // =================== ADMIN ===================

    // Admin - View All Leave Applications
    public async Task<IActionResult> AllApplications()
    {
        //if (HttpContext.Session.GetString("role") != "Admin")
        //    return RedirectToAction("Login", "Account");

        var leaves = await _context.LeaveApplications
            .Include(l => l.User)
            .OrderByDescending(l => l.FromDate)
            .ToListAsync();

        return View(leaves);
    }

    
    // Admin - Approve Leave (JSON)
    [HttpPost]
    public async Task<JsonResult> ApproveLeave(int? id)
    {
        if (id == null)
            return Json(new { success = false, message = "Leave ID not provided" });

        //if (HttpContext.Session.GetString("role") != "Admin")
        //    return Json(new { success = false, message = "Unauthorized" });

        var leaveApp = await _context.LeaveApplications.FindAsync(id);
        if (leaveApp == null)
            return Json(new { success = false, message = "Leave application not found" });

        if (leaveApp.Status != "Pending")
            return Json(new { success = false, message = $"Cannot approve leave. Current status: {leaveApp.Status}" });

        leaveApp.Status = "Approved";

        var balance = await _context.LeaveBalances
            .FirstOrDefaultAsync(b => b.UserId == leaveApp.UserId && b.Year == leaveApp.Year);

        if (balance != null)
        {
            balance.UsedLeaves += leaveApp.LeaveDays;
            balance.RemainingLeaves = balance.TotalLeaves - balance.UsedLeaves;
        }
        else
        {
            balance = new LeaveBalance
            {
                UserId = leaveApp.UserId,
                Year = leaveApp.Year,
                TotalLeaves = 20,
                UsedLeaves = leaveApp.LeaveDays,
                RemainingLeaves = 20 - leaveApp.LeaveDays
            };
            _context.LeaveBalances.Add(balance);
        }

        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Leave approved successfully" });
    }

    // Admin - Reject Leave (JSON)
    [HttpPost]
    public async Task<JsonResult> RejectLeave(int? id)
    {
        if (id == null)
            return Json(new { success = false, message = "Leave ID not provided" });

        //if (HttpContext.Session.GetString("role") != "Admin")
        //    return Json(new { success = false, message = "Unauthorized" });

        var leaveApp = await _context.LeaveApplications.FindAsync(id);
        if (leaveApp == null)
            return Json(new { success = false, message = "Leave application not found" });

        if (leaveApp.Status != "Pending")
            return Json(new { success = false, message = $"Cannot reject leave. Current status: {leaveApp.Status}" });

        leaveApp.Status = "Rejected";
        await _context.SaveChangesAsync();

        return Json(new { success = true, message = "Leave rejected successfully" });
    }

}

