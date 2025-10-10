using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string FromUserId { get; set; }
        public string ToUserId { get; set; }

        public User? FromUser { get; set; }
        public User? ToUser { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        [Column(TypeName = "bit")]
        public bool IsRead { get; set; } = false;

        // ✅ Fix here: change object to DateTime
        public DateTime SentAt { get; set; } = DateTime.Now;
    }
}
