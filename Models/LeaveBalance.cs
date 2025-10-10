using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class LeaveBalance
    {
        [Key]
        public int LeaveBalanceId { get; set; }
        [ForeignKey("Users")]
        public string UserId { get; set; }
        public User User { get; set; }

        public int Year { get; set; }
        public int TotalLeaves { get; set; } = 20;
        public int UsedLeaves { get; set; }
        public int RemainingLeaves { get; set; }
    }

}

