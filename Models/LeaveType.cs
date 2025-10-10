namespace EmployeeManagement.Models
{
    public class LeaveType
    {
        public int LeaveTypeId { get; set; }
        public string Name { get; set; }   // Sick, Casual, Annual
        public int MaxDays { get; set; }   // e.g. Sick = 10, Casual = 5
    }
}
