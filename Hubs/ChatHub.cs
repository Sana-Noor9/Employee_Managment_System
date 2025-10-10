using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagement.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext context;
        public ChatHub(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task SendMessage(MessageDTO messageDTO)
        {
            try
            {
                var message = new Message
                {
                    FromUserId = messageDTO.FromUserId,
                    ToUserId = messageDTO.ToUserId,
                    Body = messageDTO.Body,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                await context.Messages.AddAsync(message);
                await context.SaveChangesAsync();
                
                await Clients.All.SendAsync("ReceiveMessage", "You", new { body = messageDTO.Body});
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in SendMessage: " + ex.Message);
                throw;
            }
        }
    }
}
