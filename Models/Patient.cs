using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DoctorsOffice;

namespace DoctorsOffice.Models
{
    public class Patient
    {
      private string _name;
      private DateTime _dateOfBirth;
      private int _id;

      public Patient(string name, DateTime dateOfBirth, int id = 0)
      {
        _dateOfBirth = dateOfBirth;
        _name = name;
        _id = id;
      }

      public string GetName()
      {
        return _name;
      }

      public int GetId()
      {
        return _id;
      }

      public DateTime GetDateOfBirth()
      {
        return _dateOfBirth;
      }


      public static List<Patient> GetAll()
      {
        List<Patient> allPatients = new List<Patient> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM patients;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int id = rdr.GetInt32(0);
          string patientName = rdr.GetString(1);
          DateTime patientDateOfBith = rdr.GetDateTime(2);
          Patient newPatient = new Patient(patientName, patientDateOfBith, id);
          allPatients.Add(newPatient);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allPatients;
        }

        public static Patient Find(int id)
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM patients WHERE id = (@searchId);";
          MySqlParameter searchId = new MySqlParameter();
          searchId.ParameterName = "@searchId";
          searchId.Value = id;
          cmd.Parameters.Add(searchId);
          var rdr = cmd.ExecuteReader() as MySqlDataReader;

          int patientId = 0;
          string patientName = "";
          DateTime patientDateOfBith = new DateTime();
          while(rdr.Read())
          {
            patientId = rdr.GetInt32(0);
            patientName = rdr.GetString(1);
            patientDateOfBith = rdr.GetDateTime(2);
          }
          Patient newPatient = new Patient(patientName, patientDateOfBith, patientId);
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
          return newPatient;
        }

        public void Save()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO patients (name, date_of_birth) VALUES (@name,@dateOfBirth);";
          MySqlParameter name = new MySqlParameter();
          name.ParameterName = "@name";
          name.Value = this._name;
          cmd.Parameters.Add(name);
          MySqlParameter dateOfBirth = new MySqlParameter();
          dateOfBirth.ParameterName = "@dateOfBirth";
          dateOfBirth.Value = this._dateOfBirth;
          cmd.Parameters.Add(dateOfBirth);
          cmd.ExecuteNonQuery();
          _id = (int) cmd.LastInsertedId;
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
        }

        public override bool Equals(System.Object otherPatient)
        {
          if (!(otherPatient is Patient))
          {
            return false;
          }
          else
          {
             Patient newPatient = (Patient) otherPatient;
             bool idEquality = this.GetId() == newPatient.GetId();
             bool nameEquality = this.GetName() == newPatient.GetName();
             bool dateOfBirthEquality = this.GetDateOfBirth() == newPatient.GetDateOfBirth();
             return (idEquality && nameEquality && dateOfBirthEquality);
           }
        }

        // public static List<Speciality> GetSpecialities()
        // {
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"SELECT specialities.* FROM patients
        //         JOIN specialists ON (patients.id = specialists.patient_id)
        //         JOIN specialities ON (specialists.specialitie_id = specialities.id)
        //         WHERE patients.id = @PatientId;";
        //     MySqlParameter patientIdParameter = new MySqlParameter();
        //     patientIdParameter.ParameterName = "@PatientId";
        //     patientIdParameter.Value = _id;
        //     cmd.Parameters.Add(patientIdParameter);
        //     MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        //     List<Speciality> specialities = new List<Speciality>{};
        //     while(rdr.Read())
        //     {
        //       int specialitieId = rdr.GetInt32(0);
        //       string specialitieName = rdr.GetString(1);
        //       Speciality newSpecialities = new Speciality(specialitieName, specialitieId);
        //       specialities.Add(newSpecialities);
        //     }
        //     conn.Close();
        //     if (conn != null)
        //     {
        //       conn.Dispose();
        //     }
        //     return specialities;
        // }
        //
        // public static List<Doctor> GetDoctors()
        // {
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"SELECT doctors.* FROM patients
        //         JOIN specialists ON (patients.id = specialists.patient_id)
        //         JOIN doctors ON (specialists.doctors_id = doctors.id)
        //         WHERE patients.id = @PatientId;";
        //     MySqlParameter patientIdParameter = new MySqlParameter();
        //     patientIdParameter.ParameterName = "@PatientId";
        //     patientIdParameter.Value = _id;
        //     cmd.Parameters.Add(patientIdParameter);
        //     MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        //     List<Doctor> doctors = new List<Doctor>{};
        //     while(rdr.Read())
        //     {
        //       int doctorsId = rdr.GetInt32(0);
        //       string doctorsName = rdr.GetString(1);
        //       DateTime dateOfBirth = rdr.GetDateTime(2);
        //       Speciality newSpecialities = new Speciality(doctorsName, doctorsDateOfBirth, doctorsId);
        //       doctors.Add(newSpecialities);
        //     }
        //     conn.Close();
        //     if (conn != null)
        //     {
        //       conn.Dispose();
        //     }
        //     return doctors;
        // }
      }
    }
