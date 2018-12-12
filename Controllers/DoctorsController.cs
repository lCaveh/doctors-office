using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DoctorsOffice.Models;

namespace DoctorsOffice.Controllers
{
    public class DoctorsController : Controller
    {
      [HttpGet("/doctors")]
      public ActionResult Index()
      {
        List<Doctor> allDoctors = Doctor.GetAll();
        return View(allDoctors);
      }

      [HttpGet("/doctors/new")]
      public ActionResult New()
      {
        List<Speciality> allSpecialities = Speciality.GetAll();
        return View(allSpecialities);

      }
      [HttpPost("/doctors")]
      public ActionResult Index(string doctorName, int firstSpeciality, int secondSpeciality)
      {
        Doctor doctor = new Doctor(doctorName);
        doctor.Save();
        Speciality first = Speciality.Find(firstSpeciality);
        doctor.AddSpeciality(first);
        Speciality second = Speciality.Find(secondSpeciality);
        doctor.AddSpeciality(second);
        List<Doctor> allDoctors = Doctor.GetAll();
        return View(allDoctors);
      }
    }
}
