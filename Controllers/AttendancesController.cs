using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeManagement.Controllers
{
    public class AttendancesController : BaseController
    {
        private readonly DailyAttendanceService _attendanceService;

        public AttendancesController(ApplicationDbContext context, DailyAttendanceService attendanceService)
            : base(context)  // BaseController ka _context use hoga
        {
            _attendanceService = attendanceService;
        }

        // ✅ Admin: all attendance
        public async Task<IActionResult> Index()
        {
            var attendances = await _context.Attendances
                .Include(a => a.User)
                .OrderByDescending(a => a.Date)
                .ToListAsync();

            return View(attendances);
        }

        // ✅ User: my attendance
        public async Task<IActionResult> MyAttendance()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var myAttendance = await _context.Attendances
                .Include(a => a.User)
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.Date)
                .ToListAsync();

            return View(myAttendance);
        }

        // ✅ Punch In
        [HttpPost]
        public async Task<IActionResult> PunchIn()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var msg = await _attendanceService.PunchInAsync(userId);
            TempData["Message"] = msg;
            return RedirectToAction(nameof(MyAttendance));
        }

        // ✅ Punch Out
        [HttpPost]
        public async Task<IActionResult> PunchOut()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var msg = await _attendanceService.PunchOutAsync(userId);
            TempData["Message"] = msg;
            return RedirectToAction(nameof(MyAttendance));
        }
    }
}
