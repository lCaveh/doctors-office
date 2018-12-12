using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DoctorsOffice.Models;

namespace DoctorsOffice.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
          return View();
      }
    }
}
