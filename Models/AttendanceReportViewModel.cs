using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models.ViewModels
{
    public class AttendanceReportViewModel
    {
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User? User { get; set; }

        public string Employee { get; set; }
        public int TotalDays { get; set; }
        public int Present { get; set; }
        public int Absent { get; set; }
        public int Late { get; set; }
    }
}
