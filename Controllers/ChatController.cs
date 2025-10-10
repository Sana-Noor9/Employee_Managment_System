using EmployeeManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    public class ChatController : BaseController
    {
        // ⚡ No need to redefine 'context' here
        public ChatController(ApplicationDbContext context) : base(context)
        {
            // BaseController ka _context use hoga
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var employess = await _context.Users
                .Where(x => x.Role == "User" && x.Id != userId)
                .ToListAsync();

            var unreadCounts = await _context.Messages
                .Where(m => m.ToUserId == userId && !m.IsRead)
                .GroupBy(m => m.FromUserId)
                .Select(g => new { UserId = g.Key, Count = g.Count() })
                .ToListAsync();

            ViewBag.UnreadCounts = unreadCounts;

            return View(employess);
        }

        public async Task<IActionResult> AdminChat()
        {
            var users = await _context.Users
                .Where(u => u.Role == "User")
                .ToListAsync();

            string adminId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var unreadCounts = await _context.Messages
                .Where(m => m.ToUserId == adminId && !m.IsRead)
                .GroupBy(m => m.FromUserId)
                .Select(g => new { UserId = g.Key, Count = g.Count() })
                .ToListAsync();

            ViewBag.UnreadCounts = unreadCounts;

            return View(users);
        }
    }
}
