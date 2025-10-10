namespace EmployeeManagement.Models
{
    public class MessageDTO
    {
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string Body { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
