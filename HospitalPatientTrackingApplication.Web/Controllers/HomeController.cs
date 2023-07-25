using HospitalPatientTrackingApplication.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Diagnostics;
using System.Security.Claims;

namespace HospitalPatientTrackingApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _session;
        private readonly AppDbContext _context;

        public HomeController(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _session = httpContextAccessor;
            _context = context;
        }

        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Index()
        {
            int admin_count = await _context.Users.CountAsync(x => x.role =="admin");
            int user_count = await _context.Users.CountAsync(x => x.role =="user");
            int patient_count = await _context.Patients.CountAsync();
            int visit_count = await _context.Visits.CountAsync();

            ViewBag.UserCount = user_count;
            ViewBag.AdminCount = admin_count;
            ViewBag.PatientCount = patient_count;
            ViewBag.VisitCount = visit_count;
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Single(int? id)
        {
            var visits = await _context.Visits.OrderBy(x => x.Visit_Id).ToListAsync();
            List<Visit> temp_visits = new List<Visit>();
            foreach (var item in visits)
            {
                if (item.Patient_Id == id)
                {
                    temp_visits.Add(item);
                }
            }
            return View(temp_visits);
        }

        [AllowAnonymous]

        public async Task<IActionResult> Logout()
        {
            _session.HttpContext.Session.Remove("User_role"); // Oturum değerini ayarlayın.
            HttpContext.Session.Clear(); // Oturum verilerini temizle
            Response.Cookies.Delete("MySession");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "AccessLogin");

        }
        [AllowAnonymous]
        public async Task<IActionResult> update_user(int id)
        {
            var isUser = await _context.Users.FindAsync(id);

            return View(isUser);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> update_user(string username, string password, int id)
        {

            var isUser = await _context.Users.FirstOrDefaultAsync(x => x.user_id == id);

            if (isUser != null)
            {
                isUser.username = username;
                isUser.password = password;
                _context.Users.Update(isUser);
                await _context.SaveChangesAsync();
                TempData["status"] = "The user has been updated succesfully.";
                return RedirectToAction("Logout", "Home");
            }
            TempData["status"] = "The process failed to update.";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Authorize(Roles = "admin")]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}