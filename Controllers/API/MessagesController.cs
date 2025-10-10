using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{fromId}/{toId}")]
        public async Task<IActionResult> GetMessages(string fromId, string toId)
        {
            var messages = await _context.Messages
                .Include(x => x.FromUser)
                .Include(x => x.ToUser)
                .Where(x => (x.FromUserId == fromId && x.ToUserId == toId) ||
                            (x.FromUserId == toId && x.ToUserId == fromId))
                .Select(m => new {
                    m.Id,
                    m.Body,
                    m.CreatedAt,
                    FromUserId = m.FromUserId,
                    ToUserId = m.ToUserId,
                    FromUserName = m.FromUser.FullName,
                    ToUserName = m.ToUser.FullName
                })
                .ToListAsync();

            return Ok(messages);
        }

    }
}
