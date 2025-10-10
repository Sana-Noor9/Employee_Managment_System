using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public string Gender { get; set; }

        public DateTime DOB { get; set; }

        public string? UserImage { get; set; }
        public string Role { get; set; } = "User";
        public override string? Email { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
         public int? Phone { get; set; }

        public DateTime JoinDate { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [ForeignKey("Designations")]
        public int DesignationId { get; set; }
        public Designation Designations { get; set; }

        // Navigation properties
        public ICollection<LeaveType> LeaveTypes { get; set; }
        public ICollection<LeaveApplication> LeaveApplications { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Salary> Salaries { get; set; }
        public ICollection<PerformanceEvaluation> PerformanceEvaluations { get; set; }
        public ICollection<LeaveBalance> LeaveBalance { get; set; }
        public List<Message>? MessagesSent { get; set; }
        public List<Message>? MessagesReceived { get; set; }
    }
}
