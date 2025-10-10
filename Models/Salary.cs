using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class Salary
    {
        [Key]
        public int SalaryId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User? User { get; set; }

        [Range(1, 12)]
        public int Month { get; set; }   // 1 = Jan, 12 = Dec
        public int Year { get; set; }    // e.g. 2025

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasicPay { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Allowances { get; set; } = 0;  // default 0

        [Column(TypeName = "decimal(18,2)")]
        public decimal Deductions { get; set; } = 0;  // default 0

        [Column(TypeName = "decimal(18,2)")]
        public decimal NetPay { get; set; } // DB me store hoga
        [Column(TypeName = "bit")]
        public bool IsRead { get; set; } = false;
    }
}
