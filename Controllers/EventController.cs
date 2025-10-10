using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Data;

namespace EmployeeManagement.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events
                .Where(e => e.IsActive)
                .Select(e => new
                {
                    id = e.Id,
                    title = e.Title,
                    description = e.Description,
                    start = e.StartDate.ToString("yyyy-MM-dd"),
                    end = e.EndDate.ToString("yyyy-MM-dd"),
                    color = e.Color,
                    eventType = e.EventType
                })
                .ToListAsync();

            return Json(events);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEvent([FromBody] Event model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Set created by (current user)
                    model.CreatedBy = User.Identity.Name;
                    model.CreatedDate = DateTime.Now;
                    model.IsActive = true;

                    _context.Events.Add(model);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Event saved successfully!" });
                }

                return Json(new { success = false, message = "Invalid data" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEvent(int id)
        {
            var eventItem = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);

            if (eventItem == null)
                return NotFound();

            return Json(eventItem);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null)
                return NotFound();

            eventItem.IsActive = false;
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Event deleted successfully!" });
        }
        [HttpGet]
        public async Task<IActionResult> EmployeeEvents()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeEvents()
        {
            var events = await _context.Events
                .Where(e => e.IsActive && e.IsPublic)
                .Select(e => new
                {
                    id = e.Id,
                    title = e.Title,
                    description = e.Description,
                    start = e.StartDate.ToString("yyyy-MM-dd"),
                    end = e.EndDate.ToString("yyyy-MM-dd"),
                    color = e.Color,
                    eventType = e.EventType
                })
                .ToListAsync();

            return Json(events);
        }
    
        public async Task<IActionResult> AdminEvents()
        {
            return View();
        }
    }
}