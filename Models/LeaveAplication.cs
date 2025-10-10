using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class LeaveApplication
    {
        [Key]
        public int LeaveApplicationId { get; set; }

        public string? UserId { get; set; }  // Assigned in controller

        public User? User { get; set; }

        [Required(ErrorMessage = "Leave type is required.")]
        public int LeaveTypeId { get; set; }

        public LeaveType? LeaveType { get; set; }

        [Required(ErrorMessage = "From date is required.")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "To date is required.")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

        public int LeaveDays { get; set; }

        [Required(ErrorMessage = "Please enter a reason.")]
        public string Reason { get; set; }

        public string Status { get; set; } = "Pending";

        public int Year { get; set; }
        [Column(TypeName = "bit")]
        public bool IsRead { get; set; } = false;
    }
}
