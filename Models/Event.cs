using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string EventType { get; set; }

        public string Color { get; set; } = "#378006";

        public string? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        public bool IsPublic { get; set; } = true;

        public List<EventAttendee>? Attendees { get; set; }
    }




}
