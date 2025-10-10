using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models.ViewModels
{
    public class PerformanceReportViewModel
    {
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User? User { get; set; }

        public string Employee { get; set; }
        public int TotalEvaluations { get; set; }
        public double AverageScore { get; set; }
        public DateTime? LastEvaluationDate { get; set; }
    }
}
