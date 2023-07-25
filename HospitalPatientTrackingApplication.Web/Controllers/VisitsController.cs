using AutoMapper;
using HospitalPatientTrackingApplication.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HospitalPatientTrackingApplication.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class VisitsController : Controller
    {
        private readonly AppDbContext _context;
        //private readonly IMapper _mapper;
        public VisitsController(AppDbContext context)
        {
            _context = context;
            //_mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {

            //if (HttpContext.Session.GetString("User_role") == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //var join_list = from p_temp in _context.Patients
            //                join v_temp in _context.Visits
            //                on p_temp.Id equals v_temp.Patient_Id
            //                select new
            //                {
            //                    P_temp = p_temp,
            //                   V_temp = v_temp
            //                };
            //foreach (var result in join_list)
            //{
            //    Console.WriteLine($"Order ID: {result.P_temp.Id}");
            //}
            MyViewModel myViewModel = new MyViewModel();
            myViewModel.temp_visits = _context.Visits.OrderBy(x => x.Visit_Id).ToList();
            myViewModel.temp_patient = _context.Patients.ToList();

            return View(myViewModel);
        }
        public async Task<IActionResult> Single(int id)
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
        [HttpGet]
        public async Task<IActionResult> Add()
        {

            //if (HttpContext.Session.GetString("User_role") == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            ViewBag.Patients = new SelectList(_context.Patients, "Id", "name_surname");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Visit newVisit)
        {

            //if (HttpContext.Session.GetString("User_role") == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            if (ModelState.IsValid)
            {
                //_context.Visits.Add(newVisit);
                await _context.Database.ExecuteSqlRawAsync($"call insert_visit({newVisit.Patient_Id},'{newVisit.Visit_Date}','{newVisit.Doctor_Name}','{newVisit.Complaint}','{newVisit.Treatment}')");
                await _context.SaveChangesAsync();
                TempData["status"] = "The visit has been added succesfully.";

                return RedirectToAction("Index");
            }
            ViewBag.Patients = new SelectList(_context.Patients, "Id", "name_surname");

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {

            //if (HttpContext.Session.GetString("User_role") == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            var visit = _context.Visits.Find(id);
            if (visit != null)
            {
                //_context.Visits.Remove(visit);
                await _context.Database.ExecuteSqlRawAsync($"call delete_visit({id})");
                await _context.SaveChangesAsync();
                TempData["status"] = "The deletion performed successfully.";
            }

            return RedirectToAction("Index");


        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            //if (HttpContext.Session.GetString("User_role") == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            var visit = await _context.Visits.FindAsync(id);
            ViewBag.Patients = new SelectList(_context.Patients, "Id", "name_surname");

            if (visit != null)
            {

                return View(visit);
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Update(Visit newVisit)
        {

            //if (HttpContext.Session.GetString("User_role") == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //_context.Visits.Update(newVisit);
            if (ModelState.IsValid)
            {
                await _context.Database.ExecuteSqlRawAsync($"call update_visit({newVisit.Visit_Id},{newVisit.Patient_Id},'{newVisit.Visit_Date}'," +
                                $"'{newVisit.Doctor_Name}','{newVisit.Complaint}','{newVisit.Treatment}')");
                await _context.SaveChangesAsync();
                TempData["status"] = "The visit has been updated succesfully.";
                return RedirectToAction("Index");

            }
            ViewBag.Patients = new SelectList(_context.Patients, "Id", "name_surname");

            return View();

        }

    }
}
