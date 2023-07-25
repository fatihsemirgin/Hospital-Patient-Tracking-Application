using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using HospitalPatientTrackingApplication.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HospitalPatientTrackingApplication.Web.Controllers
{
    public class AccessLogin : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _session;

        public AccessLogin(IHttpContextAccessor session, AppDbContext context)
        {
            //_session = httpContextAccessor;
            _context = context;
            _session = session;
        }


        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {

                if (claimUser.IsInRole("admin"))
                    return RedirectToAction("Index", "Home");
                else if (claimUser.IsInRole("user"))
                {
                    string nameIdentifier = claimUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var isUser = await _context.Users.FirstOrDefaultAsync(x => x.username == nameIdentifier);
                    var patient_temp = await _context.Patients.FirstOrDefaultAsync(x => x.id_card == isUser.id_card);
                    if (patient_temp != null)
                    {
                        return RedirectToAction("Single", "Home", new { id = patient_temp.Id });
                    }
                    else
                        return RedirectToAction("Single", "Home");

                }
            }

            return View();
        }
        [HttpPost]


        public async Task<IActionResult> Login(string username, string password)
        {

            var isUser = await _context.Users.FirstOrDefaultAsync(x => x.username == username && x.password == password);

            if (isUser != null)
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, username),
                    new Claim(ClaimTypes.Role, isUser.role)

                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = false,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity)
                    , properties);


                _session.HttpContext.Session.SetInt32("user_id", isUser.user_id);
                _session.HttpContext.Session.SetString("user_name", isUser?.username);
                if (isUser.role.ToLower() == "admin")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (isUser.role.ToLower() == "user")
                {
                    var patient_temp = await _context.Patients.FirstOrDefaultAsync(x => x.id_card == isUser.id_card);
                    if (patient_temp != null)
                    {
                        _session.HttpContext.Session.SetInt32("patient_id", patient_temp.Id);
                        return RedirectToAction("Single", "Home", new { id = patient_temp.Id });
                    }else
                        return RedirectToAction("Single", "Home");

                }
                else
                {
                    TempData["alert"] = "Your role is not eligible for access!";

                }

            }
            if (TempData["alert"]==null)
                TempData["alert"] = "Your username or password is wrong. Please try again!";
            return View();

        }
    }
}
