using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

public class DailyAttendanceService
{
    private readonly ApplicationDbContext _context;

    public DailyAttendanceService(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ Daily seeding: create Absent rows at 8 AM
    public async Task SeedDailyAttendanceAsync()
    {
        var today = DateTime.Today;
        var users = await _context.Users.ToListAsync();

        foreach (var user in users)
        {
            bool exists = await _context.Attendances
                .AnyAsync(a => a.UserId == user.Id && a.Date == today);

            if (!exists)
            {
                _context.Attendances.Add(new Attendance
                {
                    UserId = user.Id,
                    Date = today,
                    Status = "Absent",
                    InTime = null,
                    OutTime = null
                });
            }
        }
        await _context.SaveChangesAsync();
    }

    // ✅ Punch In
    public async Task<string> PunchInAsync(string userId)
    {
        var today = DateTime.Today;

        var attendance = await _context.Attendances
            .FirstOrDefaultAsync(a => a.UserId == userId && a.Date == today);

        if (attendance == null)
        {
            attendance = new Attendance
            {
                UserId = userId,
                Date = today,
                Status = "Absent"
            };
            _context.Attendances.Add(attendance);
        }

        if (attendance.InTime != null)
            return "You have already punched in today.";

        attendance.InTime = DateTime.Now.TimeOfDay;

        // ✅ Check Punch In Time
        var punchInHour = DateTime.Now.Hour;
        var punchInMinute = DateTime.Now.Minute;

        if (punchInHour > 8 || (punchInHour == 8 && punchInMinute > 0))
        {
            attendance.Status = "Late";   // after 8:00
        }
        else
        {
            attendance.Status = "Present"; // on/before 8:00
        }

        await _context.SaveChangesAsync();
        return "Punched in successfully.";
    }


    // ✅ Punch Out
    public async Task<string> PunchOutAsync(string userId)
    {
        var today = DateTime.Today;

        var attendance = await _context.Attendances
            .FirstOrDefaultAsync(a => a.UserId == userId && a.Date == today);

        if (attendance == null || attendance.InTime == null)
            return "You haven't punched in yet.";

        if (attendance.OutTime != null)
            return "You have already punched out.";

        attendance.OutTime = DateTime.Now.TimeOfDay;

        await _context.SaveChangesAsync();
        return "Punched out successfully.";
    }
}
