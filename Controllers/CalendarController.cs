using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using EmployeeManagement.Data;

[Authorize]
public class CalendarController : Controller
{
    private readonly ApplicationDbContext _context;

    public CalendarController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserAttendance(int year, int month)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var attendance = await _context.Attendances
            .Where(a => a.UserId == userId && a.Date.Year == year && a.Date.Month == month)
          .Select(a => new
          {
              title = a.Status,
              start = a.Date.ToString("yyyy-MM-dd"),
              allDay = true,
              color = a.Status == "Present" ? "#28a745" : a.Status == "Absent" ? "#dc3545" : "#ffc107"
          })

            .ToListAsync();

        return Json(attendance);
    }
}
