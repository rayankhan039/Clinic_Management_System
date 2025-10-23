using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Controllers
{
    public class DoctorController : Controller
    {
        private readonly AppDbContext _context;
        public DoctorController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult AddEditDoctor(int? id)
        {
            if (id.HasValue)
            {
                var GetDoctor =_context.Doctors.SingleOrDefault(d => d.Doctor_Id == id);
                return View(GetDoctor);
            }
            return View(new Doctor());
        }

        [HttpPost]
        public IActionResult AddEditDoctor(Doctor doc)
        {
            if (!ModelState.IsValid)
            {
                return View(doc);
            }

            else
            {



                if (doc.Doctor_Id == 0)
                {
                    _context.Doctors.Add(doc);

                }
                else
                {
                    _context.Doctors.Update(doc);

                }
                _context.SaveChanges();
            }
            return RedirectToAction("GetDoctors", "Doctor");
        }


        public IActionResult GetDoctors()
        {
            var getAll =_context.Doctors.ToList();

            return View(getAll);
        }


        public IActionResult DeleteDoctor(int id)
        {
            var get = _context.Doctors.SingleOrDefault(d => d.Doctor_Id == id);
            if(get != null)
            {
                _context.Doctors.Remove(get);
                _context.SaveChanges();

            }
            return RedirectToAction("GetDoctors", "Doctor");
        }


        [HttpPost]
        public async Task<IActionResult> ToggleAvailability(int id, bool status)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            doctor.IsActive = status;
            await _context.SaveChangesAsync();
            
            return RedirectToAction("GetDoctors","Doctor"); // or wherever you're listing doctors
        }
    }
}
