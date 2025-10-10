using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

    public ReportsController(ApplicationDbContext context)
        {
            _context = context;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        // ==================== LEAVE REPORT ====================
        public async Task<IActionResult> LeaveReport()
        {
            var previousMonth = DateTime.Now.AddMonths(-1);
            var month = previousMonth.Month;
            var year = previousMonth.Year;

            var report = await _context.LeaveApplications
                .Include(l => l.User)
                .Where(l => l.FromDate.Month == month && l.FromDate.Year == year)
                .GroupBy(l => l.User)
                .Select(g => new LeaveReportViewModel
                {
                    UserId = g.Key.Id,
                    Employee = g.Key.FullName,
                    TotalLeaves = g.Count(),
                    Approved = g.Count(x => x.Status == "Approved"),
                    Rejected = g.Count(x => x.Status == "Rejected"),
                    Pending = g.Count(x => x.Status == "Pending")
                })
                .ToListAsync();

            ViewBag.Month = previousMonth.ToString("MMMM yyyy");
            return View(report);
        }

        public async Task<IActionResult> LeaveReportPdf()
        {
            var previousMonth = DateTime.Now.AddMonths(-1);
            var month = previousMonth.Month;
            var year = previousMonth.Year;

            var report = await _context.LeaveApplications
                .Include(l => l.User)
                .Where(l => l.FromDate.Month == month && l.FromDate.Year == year)
                .GroupBy(l => l.User)
                .Select(g => new LeaveReportViewModel
                {
                    UserId = g.Key.Id,
                    Employee = g.Key.FullName,
                    TotalLeaves = g.Count(),
                    Approved = g.Count(x => x.Status == "Approved"),
                    Rejected = g.Count(x => x.Status == "Rejected"),
                    Pending = g.Count(x => x.Status == "Pending")
                })
                .ToListAsync();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Header().Text($"Leave Report - {previousMonth:MMMM yyyy}").FontSize(18).Bold().AlignCenter();
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("User ID").Bold();
                            header.Cell().Text("Employee").Bold();
                            header.Cell().Text("Total Leaves").Bold();
                            header.Cell().Text("Approved").Bold();
                            header.Cell().Text("Rejected").Bold();
                            header.Cell().Text("Pending").Bold();
                        });

                        foreach (var row in report)
                        {
                            table.Cell().Text(row.UserId.ToString());
                            table.Cell().Text(row.Employee);
                            table.Cell().Text(row.TotalLeaves.ToString());
                            table.Cell().Text(row.Approved.ToString());
                            table.Cell().Text(row.Rejected.ToString());
                            table.Cell().Text(row.Pending.ToString());
                        }
                    });
                    page.Footer().AlignRight().Text($"Generated on: {DateTime.Now:dd MMM yyyy}").FontSize(10);
                });
            }).GeneratePdf();

            return File(pdf, "application/pdf", $"LeaveReport_{previousMonth:MMMM_yyyy}.pdf");
        }

        // ==================== ATTENDANCE REPORT ====================
        public async Task<IActionResult> AttendanceReport()
        {
            var previousMonth = DateTime.Now.AddMonths(-1);
            var month = previousMonth.Month;
            var year = previousMonth.Year;

            var report = await _context.Attendances
                .Include(a => a.User)
                .Where(a => a.Date.Month == month && a.Date.Year == year)
                .GroupBy(a => a.User)
                .Select(g => new AttendanceReportViewModel
                {
                    UserId = g.Key.Id,
                    Employee = g.Key.FullName,
                    TotalDays = g.Count(),
                    Present = g.Count(x => x.Status == "Present"),
                    Absent = g.Count(x => x.Status == "Absent"),
                    Late = g.Count(x => x.Status == "Late")
                })
                .ToListAsync();

            ViewBag.Month = previousMonth.ToString("MMMM yyyy");
            return View(report);
        }

        public async Task<IActionResult> AttendanceReportPdf()
        {
            var previousMonth = DateTime.Now.AddMonths(-1);
            var month = previousMonth.Month;
            var year = previousMonth.Year;

            var report = await _context.Attendances
                .Include(a => a.User)
                .Where(a => a.Date.Month == month && a.Date.Year == year)
                .GroupBy(a => a.User)
                .Select(g => new AttendanceReportViewModel
                {
                    UserId = g.Key.Id,
                    Employee = g.Key.FullName,
                    TotalDays = g.Count(),
                    Present = g.Count(x => x.Status == "Present"),
                    Absent = g.Count(x => x.Status == "Absent"),
                    Late = g.Count(x => x.Status == "Late")
                })
                .ToListAsync();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Header().Text($"Attendance Report - {previousMonth:MMMM yyyy}").FontSize(18).Bold().AlignCenter();
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("User ID").Bold();
                            header.Cell().Text("Employee").Bold();
                            header.Cell().Text("Total Days").Bold();
                            header.Cell().Text("Present").Bold();
                            header.Cell().Text("Absent").Bold();
                            header.Cell().Text("Late").Bold();
                        });

                        foreach (var row in report)
                        {
                            table.Cell().Text(row.UserId.ToString());
                            table.Cell().Text(row.Employee);
                            table.Cell().Text(row.TotalDays.ToString());
                            table.Cell().Text(row.Present.ToString());
                            table.Cell().Text(row.Absent.ToString());
                            table.Cell().Text(row.Late.ToString());
                        }
                    });
                    page.Footer().AlignRight().Text($"Generated on: {DateTime.Now:dd MMM yyyy}").FontSize(10);
                });
            }).GeneratePdf();

            return File(pdf, "application/pdf", $"AttendanceReport_{previousMonth:MMMM_yyyy}.pdf");
        }

        // ==================== PERFORMANCE REPORT ====================
        public async Task<IActionResult> PerformanceReport()
        {
            var report = await _context.PerformanceEvaluations
                .Include(p => p.User)
                .GroupBy(p => p.User)
                .Select(g => new PerformanceReportViewModel
                {
                    UserId = g.Key.Id,
                    Employee = g.Key.FullName,
                    TotalEvaluations = g.Count(),
                    AverageScore = g.Average(x => x.Score),
                    LastEvaluationDate = g.Max(x => x.EvaluationDate)
                })
                .ToListAsync();

            return View(report);
        }

        public async Task<IActionResult> PerformanceReportPdf()
        {
            var report = await _context.PerformanceEvaluations
                .Include(p => p.User)
                .GroupBy(p => p.User)
                .Select(g => new PerformanceReportViewModel
                {
                    UserId = g.Key.Id,
                    Employee = g.Key.FullName,
                    TotalEvaluations = g.Count(),
                    AverageScore = g.Average(x => x.Score),
                    LastEvaluationDate = g.Max(x => x.EvaluationDate)
                })
                .ToListAsync();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Header().Text("Performance Report").FontSize(18).Bold().AlignCenter();
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(3);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("User ID").Bold();
                            header.Cell().Text("Employee").Bold();
                            header.Cell().Text("Total Evaluations").Bold();
                            header.Cell().Text("Average Score").Bold();
                            header.Cell().Text("Last Evaluation Date").Bold();
                        });

                        foreach (var row in report)
                        {
                            table.Cell().Text(row.UserId.ToString());
                            table.Cell().Text(row.Employee);
                            table.Cell().Text(row.TotalEvaluations.ToString());
                            table.Cell().Text(row.AverageScore.ToString("F2"));

                            // ✅ Safe date formatting
                            table.Cell().Text(row.LastEvaluationDate.HasValue
                                ? row.LastEvaluationDate.Value.ToString("dd MMM yyyy")
                                : "-");
                        }
                    });
                    page.Footer().AlignRight().Text($"Generated on: {DateTime.Now:dd MMM yyyy}").FontSize(10);
                });
            }).GeneratePdf();

            return File(pdf, "application/pdf", "PerformanceReport.pdf");
        }
    }


}
