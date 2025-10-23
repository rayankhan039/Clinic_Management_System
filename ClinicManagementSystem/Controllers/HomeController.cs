using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;
    public HomeController(ILogger<HomeController> logger,AppDbContext context)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        var appointments = _context.Appointments
            .Where(a => a.Date >= today && a.Date < tomorrow)
            .Include(a => a.Doctor)
            .OrderByDescending(a => a.Appointment_Id)
            .ToList();

        //Count No of active Doctors at that time
        ViewBag.Doctors =_context.Doctors.Where(d => d.IsActive == true).Count();

        //Count No of Appoinments of that day
        ViewBag.Appointments = _context.Appointments.Where(a => a.Date == DateTime.Today).Count();

        //Count Total No of Doctors in Clinic
        ViewBag.TotalDoc = _context.Doctors.Count();

        return View(appointments);
    }

 

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
