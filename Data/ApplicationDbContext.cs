using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ✅ DbSets
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }  // ✅ fixed name
        public DbSet<LeaveBalance> LeaveBalances { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<PerformanceEvaluation> PerformanceEvaluations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // LeaveBalance → User (1-to-Many)
            modelBuilder.Entity<LeaveBalance>()
                .HasOne(lb => lb.User)
                .WithMany(u => u.LeaveBalance)
                .HasForeignKey(lb => lb.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Designation → Department (1-to-Many)
            modelBuilder.Entity<Designation>()
                .HasOne(d => d.Department)
                .WithMany()
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // User → Department (1-to-Many)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany()
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // User → Designation (1-to-Many)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Designations)
                .WithMany()
                .HasForeignKey(u => u.DesignationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.FromUser)
                .WithMany(u => u.MessagesSent)
                .HasForeignKey(m => m.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.ToUser)
                .WithMany(u => u.MessagesReceived)
                .HasForeignKey(m => m.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
