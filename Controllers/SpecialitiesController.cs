using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DoctorsOffice.Models;

namespace DoctorsOffice.Controllers
{
    public class SpecialitiesController : Controller
    {
      [HttpGet("/specialities")]
      public ActionResult Index()
      {
        List<Speciality> allSpecialities = Speciality.GetAll();
        return View(allSpecialities);
      }
      [HttpGet("/specialities/new")]
      public ActionResult New()
      {
        return View();
      }
      [HttpPost("/specialities")]
      public ActionResult Index(string specialityName)
      {
        Speciality speciality = new Speciality(specialityName);
        speciality.Save();
        List<Speciality> allSpecialities = Speciality.GetAll();
        return View(allSpecialities);
      }
    }
}
