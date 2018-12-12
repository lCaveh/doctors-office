using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DoctorsOffice;

namespace DoctorsOffice.Models
{
  public class Speciality
  {
    private string _name;
    private int _id;

    public Speciality(string name, int id = 0)
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
    public static List<Speciality> GetAll()
    {
      List<Speciality> allSpecialities = new List<Speciality> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialities;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string specialityName = rdr.GetString(1);
        Speciality newSpeciality = new Speciality(specialityName, id);
        allSpecialities.Add(newSpeciality);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allSpecialities;
    }

    public static Speciality Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialities WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int specialityId = 0;
      string specialityName = "";
      while(rdr.Read())
      {
        specialityId = rdr.GetInt32(0);
        specialityName = rdr.GetString(1);
      }
      Speciality newSpeciality = new Speciality(specialityName, specialityId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newSpeciality;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialities (name) VALUES (@name);";
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

    public void AddDoctor(Doctor newDoctor)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialists (doctor_id, speciality_id) VALUES (@DoctorId, @SpecialityId);";
      MySqlParameter doctor_id = new MySqlParameter();
      doctor_id.ParameterName = "@DoctorId";
      doctor_id.Value = newDoctor.GetId();
      cmd.Parameters.Add(doctor_id);
      MySqlParameter speciality_id = new MySqlParameter();
      speciality_id.ParameterName = "@SpecialityId";
      speciality_id.Value = _id;
      cmd.Parameters.Add(speciality_id);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
