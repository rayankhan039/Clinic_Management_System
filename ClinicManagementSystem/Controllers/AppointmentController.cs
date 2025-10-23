using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ClinicManagementSystem.Controllers
{
    
    public class AppointmentController : Controller
    {
      private readonly AppDbContext _context;
        public AppointmentController(AppDbContext context)
        {
            _context=context;
        }
        public IActionResult AddEditAppointment(int? id)
        {
         
            ViewBag.ActiveDoctors = _context.Doctors
                .Where(d => d.IsActive)
                .Select(d => new SelectListItem
                {
                    Value = d.Doctor_Id.ToString(),
                    Text = d.Name
                })
                .ToList();

         
            if (id.HasValue)
            {
                var findAppointment = _context.Appointments
              .SingleOrDefault(a => a.Appointment_Id == id.Value);

                if (findAppointment == null)
                {
                   
                    return NotFound();
                }

                return View(findAppointment);
            }

      
          
                return View(new Appointment());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEditAppointment(Appointment appoint)
        {
            ViewBag.ActiveDoctors = _context.Doctors
                  .Where(d => d.IsActive).Select(d =>
                  new SelectListItem
                  {
                      Value = d.Doctor_Id.ToString(),
                      Text = d.Name
                  }).ToList();

           


            if (appoint.Appointment_Id == 0)
            {


                appoint.Date = DateTime.Now;
                appoint.Time = DateTime.Now.TimeOfDay;
                appoint.TokenNumber = GenerateDialyToken();
                _context.Appointments.Add(appoint);
                _context.SaveChanges();

            }

            else
            {
              
            
                var existingappoint = _context.Appointments.SingleOrDefault(a => a.Appointment_Id == appoint.Appointment_Id);

                if (existingappoint == null)
                {
                    return NotFound();
                }

                existingappoint.Patient_Name = appoint.Patient_Name;
                existingappoint.Patient_Contact = appoint.Patient_Contact;
                existingappoint.Doctor_Id = appoint.Doctor_Id;
                existingappoint.Fee = appoint.Fee;

                _context.Appointments.Update(existingappoint);


                _context.SaveChanges();
             }
            
        
          

        

            return RedirectToAction("Index","Home");
        }

        public IActionResult GetAllAppointment()
        {
            var GetAll =_context.Appointments.OrderByDescending(a=>a.Appointment_Id).Include(d=>d.Doctor).ToList();

            return View(GetAll);
        }

        public IActionResult DeleteAppointment(int id)
        {
            var findAppointment = _context.Appointments.SingleOrDefault(a => a.Appointment_Id == id);

            if(findAppointment != null)
            {
                _context.Appointments.Remove(findAppointment);
                 _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        private string GenerateDialyToken()
        {
            var today = DateTime.Today;

            var todayTokens = _context.Appointments
                .Where(a => a.Date.Date == today && a.TokenNumber.StartsWith("A-"))
                .Select(a => a.TokenNumber)
                .ToList();

            if (!todayTokens.Any())
                return "A-001";

          
            int maxNumber = todayTokens
                .Select(t => int.Parse(t.Split('-')[1]))
                .Max();

            return $"A-{(maxNumber + 1).ToString("D3")}";
        }

       
    }
}
