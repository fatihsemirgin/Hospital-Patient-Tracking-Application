using AutoMapper;
using HospitalPatientTrackingApplication.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.JSInterop;

namespace HospitalPatientTrackingApplication.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class PatientsController : Controller
    {
        private readonly AppDbContext _context;
        //private readonly   IMapper _mapper;
        public PatientsController(AppDbContext context)
        {
            _context = context;
            //_mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {

            var patients = await _context.Patients.OrderBy(x => x.Id).ToListAsync();
            return View(patients);
        }
        public async Task<IActionResult> Single(int id)
        {

            var patient_temp = await _context.Patients.FindAsync(id);
            return View(patient_temp);
        }
        [HttpGet]
        public async Task<IActionResult> AddPatient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient(Patient newPatient)
        {


            if (ModelState.IsValid)
            {
                if (isUnique(newPatient.id_card, newPatient.Id))
                {

                    await _context.Database.ExecuteSqlRawAsync($"call insert_patient({newPatient.id_card},'{newPatient.name_surname}','{newPatient.birth_date}');");
                    await _context.SaveChangesAsync();
                    TempData["status"] = "The patient has been added succesfully.";
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("id_card", "The value must be unique.");
            }
            return View();


        }

        public async Task<IActionResult> DeletePatient(int id)
        {
            var temp_patient = _context.Patients.Find(id);
            if (temp_patient != null)
            {
                //_context.Patients.Remove(temp_patient);
                await _context.Database.ExecuteSqlRawAsync($"call delete_patient('{id}')");
                await _context.SaveChangesAsync();
                TempData["status"] = "The deletion performed successfully.";
                TempData["delete"] = "delete";

            }
            else
            {
                TempData["status"] = "The deletion is not performed successfully.";

            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {


            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                return View(patient);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(Patient patient)
        {


            if (ModelState.IsValid)
            {

                if (isUnique(patient.id_card, patient.Id))
                {

                    await _context.Database.ExecuteSqlRawAsync($"call update_patient({patient.Id},'{patient.name_surname}',{patient.id_card},'{patient.birth_date}')");
                    await _context.SaveChangesAsync();
                    TempData["status"] = "The patient has been updated succesfully.";
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("id_card", "The id card value must be unique.");

            }
            return View();

        }
        public bool isUnique(long id_card, int id)
        {
            return !_context.Patients.AsNoTracking().ToList().Any(x => x.id_card == id_card && x.Id != id); ;
        }

    }
}
