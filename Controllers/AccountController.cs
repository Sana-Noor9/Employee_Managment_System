using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeManagement.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly DailyAttendanceService _attendanceService;

        public AccountController(
            UserManager<User> userManager,
            ApplicationDbContext context,
            IWebHostEnvironment env,
            DailyAttendanceService attendanceService,SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager) // inject service here
        {
            _userManager = userManager;
            _db = context;
            _env = env;
            _attendanceService = attendanceService;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                var row = _db.Users.Include(u => u.Department).Include(u => u.Designations).ToList();
                return View(row);
            }

            return RedirectToAction("Login"); // Or wherever your login action is
        }

      public IActionResult Register()
{
    ViewBag.Departments = new SelectList(_db.Departments.ToList(), "DepartmentId", "DepartmentName");
    ViewBag.Designations = new SelectList(_db.Designations.ToList(), "DesignationId", "DesignationName");
    return View();
}

        [HttpPost]
        public async Task<IActionResult> Register(IFormFile UserImage, User user, string password)
        {
            // --- IMAGE UPLOAD ---
            if (UserImage != null && UserImage.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(UserImage.FileName);

                string folderPath = Path.Combine(_env.WebRootPath, "users");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string filePath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await UserImage.CopyToAsync(stream);
                }

                user.UserImage = fileName; // filename DB me save hoga
            }

            if (string.IsNullOrEmpty(user.UserName))
                user.UserName = user.Email;

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                string role = user.Role ?? "User";
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));

                await _userManager.AddToRoleAsync(user, role);

                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(user);
        }
        public IActionResult Login()
        {
            return View();
        }
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
{
    if (!ModelState.IsValid)
        return View(model);

    var user = await _userManager.FindByEmailAsync(model.Email);
    if (user != null)
    {
        var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

        if (result.Succeeded)
{
    // Save session info
    HttpContext.Session.SetString("UserId", user.Id);
    HttpContext.Session.SetString("name", user.UserName);

    // Get role
    var roles = await _userManager.GetRolesAsync(user);
    var role = roles.FirstOrDefault() ?? "User";
    HttpContext.Session.SetString("role", role);

    // Set ViewBag for layout (user image, name, email)
    ViewBag.UserImage = string.IsNullOrEmpty(user.UserImage) 
        ? "/assets/img/avtar/02.jpg"  // default image
        : "/users/" + user.UserImage;
                    if (!string.IsNullOrEmpty(user.UserImage))
                    {
                        HttpContext.Session.SetString("userImage", user.UserImage);
                    }
                    //else
                    //{
                    //    HttpContext.Session.SetString("userImage","");

                    //}
                    
    ViewBag.Name = user.FullName;
    ViewBag.Email = user.Email;

    // Attendance Punch In
    var today = DateTime.Today;
    var attendance = await _db.Attendances
        .FirstOrDefaultAsync(a => a.UserId == user.Id && a.Date == today);

    if (attendance == null)
    {
        attendance = new Attendance
        {
            UserId = user.Id,
            Date = today,
            InTime = DateTime.Now.TimeOfDay,
            Status = DateTime.Now.TimeOfDay <= new TimeSpan(8, 0, 0) ? "Present" : "Late"
        };
        _db.Attendances.Add(attendance);
        await _db.SaveChangesAsync();
    }

    // Redirect based on role
    if (role == "Admin" || role == "HR")
        return RedirectToAction("AdminHRDashboard");
    else
        return RedirectToAction("UserDashborad");
}

    }

    ModelState.AddModelError("", "Invalid login attempt");
    return View(model);
}


        //ogin

        //edit login
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.Users
                .Include(u => u.Department)
                .Include(u => u.Designations)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return NotFound();

            // Dropdowns ke liye ViewBag
            ViewBag.Departments = new SelectList(_db.Departments.ToList(), "DepartmentId", "DepartmentName", user.DepartmentId);
            ViewBag.Designations = new SelectList(_db.Designations.ToList(), "DesignationId", "DesignationName", user.DesignationId);

            return View(user); // Edit.cshtml ko bhejenge
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User model, IFormFile UserImage)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            // ✅ unnecessary validations remove
            ModelState.Remove("Department");
            ModelState.Remove("Designations");
            ModelState.Remove("LeaveTypes");
            ModelState.Remove("Attendances");
            ModelState.Remove("Salaries");
            ModelState.Remove("PerformanceEvaluations");
            ModelState.Remove("LeaveApplications");
            ModelState.Remove("LeaveBalance");
            ModelState.Remove("UserImage");

            if (ModelState.IsValid)
            {
                // ✅ Basic fields update
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.Email;   // Identity ke liye required
                user.Phone = model.Phone;      // Ab string hai
                user.Gender = model.Gender;
                user.DOB = model.DOB;
                user.Address = model.Address;
                user.City = model.City;
                user.DepartmentId = model.DepartmentId;
                user.DesignationId = model.DesignationId;

                // ✅ Image upload (agar nayi di gayi hai)
                if (UserImage != null && UserImage.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(UserImage.FileName);
                    string folderPath = Path.Combine(_env.WebRootPath, "users");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string filePath = Path.Combine(folderPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await UserImage.CopyToAsync(stream);
                    }

                    user.UserImage = fileName;  // nayi image set
                }
                // agar UserImage null hai to purani image hi rahegi ✅

                // ✅ Update user in DB
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index"); // Edit ke baad wapas list me
                }

                // Errors agar aaye to modelstate me dikhayenge
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            // agar model valid nahi hai ya update fail hua to dropdowns wapas load karna zaroori hai
            ViewBag.Departments = new SelectList(_db.Departments.ToList(), "DepartmentId", "DepartmentName", model.DepartmentId);
            ViewBag.Designations = new SelectList(_db.Designations.ToList(), "DesignationId", "DesignationName", model.DesignationId);

            return View(model);
        }
        //edit login

        //del user
        // ✅ Direct Delete without confirmation page
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                // ✅ Deleted successfully → wapas Index par bhejo
                return RedirectToAction("Index");
            }

            // ❌ Agar delete fail ho jaye to error messages add karo
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            // Fir bhi wapas Index bhejenge
            return RedirectToAction("Index");
        }

        //del user


        //ogin


        [Authorize(Roles = "Admin,HR")]
public IActionResult AdminHRDashboard()
{
    if (HttpContext.Session.GetString("name") != null)
    {
        ViewBag.Role = HttpContext.Session.GetString("role"); // Admin or HR
        ViewBag.Name = HttpContext.Session.GetString("name");
        return View();
    }

    return RedirectToAction("Login");
}

        public IActionResult UserDashborad()
        {

            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");


            }
            return View();
        }




        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(oldPassword) ||
                string.IsNullOrWhiteSpace(newPassword) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                ModelState.AddModelError("", "All fields are required.");
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "New password & confirm password do not match.");
                return View();
            }

            // Get current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Use Identity API to verify + change password
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user); // refresh session
                TempData["Message"] = "Password changed successfully!";
                return RedirectToAction("UserDashborad"); // redirect back to view
            }

            // Show Identity errors
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        

    }
}
