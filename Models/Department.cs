namespace EmployeeManagement.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public ICollection<Designation> Designations { get; set; }
        public ICollection<User> User { get; set; }
    }
}
