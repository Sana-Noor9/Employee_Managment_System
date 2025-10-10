using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class PerformanceEvaluation
    {
        [Key]
        public int EvaluationId { get; set; }

        [Required(ErrorMessage = "Employee selection is required.")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; } // ✅ Nullable, takay only UserId kaam kare

        [Required(ErrorMessage = "Evaluation Date is required.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        [Range(1, 10, ErrorMessage = "Score must be between 1 and 10.")]
        public int Score { get; set; }

        [StringLength(500)]
        public string? Comments { get; set; }
        [Column(TypeName = "bit")]
        public bool IsRead { get; set; } = false;
        public DateTime? EvaluationDate { get; internal set; }
    }
}
