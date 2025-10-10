using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }

        [Required]
        [ForeignKey("User")]
        public required string UserId { get; set; }

        public User User { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan? InTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan? OutTime { get; set; } // Nullable

        [StringLength(50)]
        public string Status { get; set; }
        [Column(TypeName = "bit")]
        public bool IsRead { get; set; } = false;
    }
}