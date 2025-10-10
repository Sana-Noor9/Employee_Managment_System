namespace EmployeeManagement.Models
{
    public class Designation
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<User> User { get; set; }
    }
}
