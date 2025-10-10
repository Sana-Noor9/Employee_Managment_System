using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Rotativa.AspNetCore.Options;
using System.Drawing.Printing;
using DinkToPdf;
using DinkToPdf.Contracts;
using QuestPDF.Helpers;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
namespace EmployeeManagement.Controllers
{
    //[Authorize] // ensure only logged in users can access
    public class SalariesController : BaseController
    {
        private readonly EmailSettings emailSettings;
        private readonly UserManager<User> _userManager;
        private readonly IConverter _converter;


        public SalariesController(
            ApplicationDbContext context,
            IOptions<EmailSettings> emailSettings,
            UserManager<User> userManager,
            IConverter converter
        ) : base(context)   // 👈 yahan se BaseController ko context bhej diya
        {
            this.emailSettings = emailSettings.Value;
            _userManager = userManager;
            this._converter = converter;
        }



        private async Task SendingEmail(string email, Salary salary)
        {
            var user = await _userManager.FindByIdAsync(salary.UserId);
            if (user == null)
                throw new Exception("User not found");

            string monthName = new DateTime(salary.Year, salary.Month, 1).ToString("MMMM");
            string subject = $"Salary Notification for {monthName} {salary.Year}";

            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Templates", "email-template.html");
            string body = await System.IO.File.ReadAllTextAsync(templatePath);

            // Replace placeholders in HTML
            body = body.Replace("{{UserName}}", user.FullName)
                       .Replace("{{Month}}", monthName)
                       .Replace("{{Year}}", salary.Year.ToString())
                       .Replace("{{BasicPay}}", salary.BasicPay.ToString("C"))
                       .Replace("{{Allowances}}", salary.Allowances.ToString("C"))
                       .Replace("{{Deductions}}", salary.Deductions.ToString("C"))
                       .Replace("{{NetPay}}", salary.NetPay.ToString("C"))
                       .Replace("{{Date}}", DateTime.Now.ToString("dd MMMM yyyy"));

            var smtpClient = new SmtpClient(emailSettings.Host)
            {
                Port = emailSettings.Port,
                Credentials = new NetworkCredential(emailSettings.UserName, emailSettings.Password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings.UserName, "HR Department"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }



        public IActionResult Test()
        {
            return View();
        }

        // ================= ADMIN PANEL =================
        // GET: Salaries (Admin sab salaries dekh sakta hai)
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var salaries = _context.Salaries
                                   .Include(s => s.User)
                                   .OrderByDescending(s => s.Year)
                                   .ThenByDescending(s => s.Month);
            return View(await salaries.ToListAsync());
        }

        // GET: Salaries/Details/5
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var salary = await _context.Salaries
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SalaryId == id);

            if (salary == null) return NotFound();

            return View(salary);
        }

        // GET: Salaries/Create
        //[Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName");
            return View();
        }

        // POST: Salaries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Salary salary)
        {
            //string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Templates", "email-template.html");
            //return Json(templatePath);
            //return Json(salary);
            if (ModelState.IsValid)
            {
                // NetPay auto calculate
                salary.NetPay = salary.BasicPay + salary.Allowances - salary.Deductions;

                _context.Add(salary);
                await _context.SaveChangesAsync();
                var user = await _userManager.FindByIdAsync(salary.UserId);
                if (user == null)
                    return NotFound("User not found");

                var email = user.Email;
                await SendingEmail(email, salary);

                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", salary.UserId);
            return View(salary);
        }

        // GET: Salaries/Edit/5
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null) return NotFound();

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", salary.UserId);
            return View(salary);
        }

        // POST: Salaries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Salary salary)
        {
            if (id != salary.SalaryId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // NetPay recalculate
                    salary.NetPay = salary.BasicPay + salary.Allowances - salary.Deductions;

                    _context.Update(salary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaryExists(salary.SalaryId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", salary.UserId);
            return View(salary);
        }

        // GET: Salaries/Delete/5
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var salary = await _context.Salaries
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SalaryId == id);

            if (salary == null) return NotFound();

            return View(salary);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if (salary != null)
            {
                _context.Salaries.Remove(salary);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SalaryExists(int id)
        {
            return _context.Salaries.Any(e => e.SalaryId == id);
        }

        // ================= EMPLOYEE PANEL =================
        // Employee apni salaries dekh sake
        [Authorize]
        public async Task<IActionResult> MySalaries()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var mySalaries = await _context.Salaries
                .Include(s => s.User)
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.Year)
                .ThenByDescending(s => s.Month)
                .ToListAsync();

            // ====== Mark salaries as read ======
            var unread = mySalaries.Where(s => !s.IsRead).ToList();
            if (unread.Any())
            {
                foreach (var s in unread)
                {
                    s.IsRead = true;
                }
                await _context.SaveChangesAsync();
            }

            return View(mySalaries);
        }



        // Employee apna salary slip PDF download kare
        //[Authorize]
        [Authorize]
        public async Task<IActionResult> SalarySlip(int id)
        {
            var salary = await _context.Salaries
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.SalaryId == id);

            if (salary == null)
                return NotFound();

            // 👇 Mark as Read
            if (!salary.IsRead)
            {
                salary.IsRead = true;
                _context.Update(salary);
                await _context.SaveChangesAsync();
            }

            return new ViewAsPdf("SalarySlips", salary)
            {
                FileName = $"SalarySlip_{salary.User.FullName}_{salary.Month}_{salary.Year}.pdf"
            };
        }

        public async Task<IActionResult> GeneratePdf(int id)
        {
            // ✅ License set karna zaroori hai
            QuestPDF.Settings.License = LicenseType.Community;

            var salary = await _context.Salaries
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.SalaryId == id);

            if (salary == null)
                return NotFound();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);

                    page.Header()
                        .Text("Salary Slip")
                        .FontSize(20)
                        .Bold();

                    page.Content().PaddingVertical(10).Text(text =>
                    {
                        text.Line($"Name: {salary.User.FullName}");
                        text.Line($"Month: {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(salary.Month)} {salary.Year}");
                        text.Line($"Basic Pay: {salary.BasicPay:C}");
                        text.Line($"Allowances: {salary.Allowances:C}");
                        text.Line($"Deductions: {salary.Deductions:C}");
                        text.Line($"Net Pay: {salary.NetPay:C}");
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Generated on " + DateTime.Now.ToString("dd MMM yyyy"));
                });
            }).GeneratePdf();

            string fileName = $"SalarySlip_{salary.User.FullName}_{salary.Month}_{salary.Year}.pdf";

            return File(pdf, "application/pdf", fileName);
        }

        //public async Task<IActionResult> GenertPdf()
        //{
        //    var salary = await _context.Salaries
        //        .Include(s => s.User)
        //        .FirstOrDefaultAsync(s => s.SalaryId == id);

        //    if (salary == null)
        //        return NotFound();
        //    return View();
        //}


    }
}
