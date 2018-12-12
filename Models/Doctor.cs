using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DoctorsOffice;

namespace DoctorsOffice.Models
{
    public class Doctor
    {
      private string _name;
      private int _id;

      public Doctor(string name, int id = 0)
      {
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
      public static List<Doctor> GetAll()
      {
        List<Doctor> allDoctors = new List<Doctor> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM doctors;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int id = rdr.GetInt32(0);
          string doctorName = rdr.GetString(1);
          Doctor newDoctor = new Doctor(doctorName, id);
          allDoctors.Add(newDoctor);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allDoctors;
        }

        public static Doctor Find(int id)
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM doctors WHERE id = (@searchId);";
          MySqlParameter searchId = new MySqlParameter();
          searchId.ParameterName = "@searchId";
          searchId.Value = id;
          cmd.Parameters.Add(searchId);
          var rdr = cmd.ExecuteReader() as MySqlDataReader;

          int doctorId = 0;
          string doctorName = "";
          while(rdr.Read())
          {
            doctorId = rdr.GetInt32(0);
            doctorName = rdr.GetString(1);
          }
          Doctor newDoctor = new Doctor(doctorName, doctorId);
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
          return newDoctor;
        }

        public void Save()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO doctors (name) VALUES (@name);";
          MySqlParameter name = new MySqlParameter();
          name.ParameterName = "@name";
          name.Value = this._name;
          cmd.Parameters.Add(name);
          cmd.ExecuteNonQuery();
          _id = (int) cmd.LastInsertedId;
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
        }

        public void AddSpeciality(Speciality newSpeciality)
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO specialists (speciality_id, doctor_id) VALUES (@SpecialityId, @DoctorId);";
          MySqlParameter speciality_id = new MySqlParameter();
          speciality_id.ParameterName = "@SpecialityId";
          speciality_id.Value = newSpeciality.GetId();
          cmd.Parameters.Add(speciality_id);
          MySqlParameter doctor_id = new MySqlParameter();
          doctor_id.ParameterName = "@DoctorId";
          doctor_id.Value = _id;
          cmd.Parameters.Add(doctor_id);
          cmd.ExecuteNonQuery();
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
        }

        public List<Speciality> GetSpecialites()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT specialities.* FROM doctors
            JOIN specialities_doctors ON (doctors.id = specialities_doctors.doctor_id)
            JOIN specialities ON (specialities_doctors.speciality_id = specialities.id)
            WHERE doctors.id = @DoctorId;";
          MySqlParameter doctorIdParameter = new MySqlParameter();
          doctorIdParameter.ParameterName = "@DoctorId";
          doctorIdParameter.Value = _id;
          cmd.Parameters.Add(doctorIdParameter);
          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          List<Speciality> specialities = new List<Speciality> {};
          while(rdr.Read())
          {
            int specialityId = rdr.GetInt32(0);
            string specialityName = rdr.GetString(1);
            Speciality newSpeciality = new Speciality(specialityName, specialityId);
            specialities.Add(newSpeciality);
          }
          conn.Close();
          if (conn !=null)
          {
            conn.Dispose();
          }
          return specialities;
        }

      }
    }
