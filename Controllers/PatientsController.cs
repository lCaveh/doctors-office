using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DoctorsOffice.Models;
using System;

namespace DoctorsOffice.Controllers
{
    public class PatientsController : Controller
    {
      [HttpGet("/patients")]
      public ActionResult Index()
      {
        List<Patient> allPatients = Patient.GetAll();
        return View(allPatients);
      }

      [HttpGet("/patients/new")]
      public ActionResult New()
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Speciality> specialities = Patient.GetSpecialities();
        List<Doctor> doctors = Patient.GetDoctors();
        model.Add("specialities",specialities);
        model.Add("doctors",doctors);

        return View(model);
      }

      [HttpPost("/patients")]
      public ActionResult Index(string patientName, DateTime PatientDateOfBirth)
      {
        Patient patient = new Patient(patientName, PatientDateOfBirth);
        patient.Save();
        List<Patient> allPatients = Patient.GetAll();
        return View(allPatients);
      }

      // [HttpGet("/patients/{patientId}/details")]
      // public ActionResult Show(int id)
      // {
      //   Dictionary<string, object> model = new Dictionary<string, object>();
      //   Patient selectedPatient = Patient.Find(id);
      //   List<Doctor> patientDoctor = selectedPatient.GetDoctors();
      // }
    }
}
