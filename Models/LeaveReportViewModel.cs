namespace EmployeeManagement.Models.ViewModels
{
    public class LeaveReportViewModel
    {
        public string UserId { get; set; }         // From User table
        public string Employee { get; set; }    // User.FullName
        public int TotalLeaves { get; set; }
        public int Approved { get; set; }
        public int Rejected { get; set; }
        public int Pending { get; set; }
    }
}