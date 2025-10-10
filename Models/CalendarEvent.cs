namespace EmployeeManagement.Models
{
    public class CalendarEvent
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }
        public bool AllDay { get; set; }
    }
}
