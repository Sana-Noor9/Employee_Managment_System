using EmployeeManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace EmployeeManagement.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;

        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                // Unread leave notifications
                ViewBag.LeaveUnread = _context.LeaveApplications
                    .Count(l => l.UserId == userId && !l.IsRead);

                // Unread salary notifications
                ViewBag.SalaryUnread = _context.Salaries
                    .Count(s => s.UserId == userId && !s.IsRead);

                // Unread chat messages
                ViewBag.ChatUnread = _context.Messages
          .Count(c => c.ToUserId == userId && !c.IsRead);
                // Unread performance evaluations (example: pending evaluations)
                ViewBag.EvaluationUnread = _context.PerformanceEvaluations
                    .Count(e => e.UserId == userId && !e.IsRead);

                // Unread attendance notifications (example: pending approvals)
                ViewBag.AttendanceUnread = _context.Attendances
                    .Count(a => a.UserId == userId && !a.IsRead);
            }

            base.OnActionExecuting(context);
        }
    }
}
