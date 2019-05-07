using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AnimalShelter.Models
{
  public class HellBeast
  {
    public string Name {get; set;}
    public string Origin {get; set;}
    public string Location {get; set;}
    public int Id {get; set;}

    public HellBeast (string name, string origin, string location, int id = 0) {
      Name = name;
      Origin = origin;
      Location = location;
      Id = id;
    }

    public static List<HellBeast> GetAll()
    {
      List<HellBeast> allHellBeasts = new List<HellBeast> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM hellbeast;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string origin = rdr.GetString(2);
        string location = rdr.GetString(3);

        HellBeast newHellBeast = new HellBeast(name, origin, location, id);
        allHellBeasts.Add(newHellBeast);
      }
      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
      return allHellBeasts;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM hellbeast";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherHellBeast)
    {
      if (!(otherHellBeast is HellBeast))
      {
        return false;
      }
      else
      {
        HellBeast newHellBeast = (HellBeast) otherHellBeast;
        bool idEquality = (this.Id == newHellBeast.Id);
        bool nameEquality = (this.Name == newHellBeast.Name);
        bool originEquality = (this.Origin == newHellBeast.Origin);
        bool locationEquality = (this.Location == newHellBeast.Location);
        return (idEquality && nameEquality && originEquality && locationEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `hellbeast` (`name`, `origin`, `location`) VALUES (@HellBeastName, @HellBeastOrigin, @HellBeastLocation);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@HellBeastName";
      name.Value = this.Name;

      MySqlParameter origin = new MySqlParameter();
      origin.ParameterName = "@HellBeastOrigin";
      origin.Value = this.Origin;

      MySqlParameter location = new MySqlParameter();
      location.ParameterName = "@HellBeastLocation";
      location.Value = this.Location;

      cmd.Parameters.Add(name);
      cmd.Parameters.Add(origin);
      cmd.Parameters.Add(location);
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      // more logic will go here in a moment

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static HellBeast Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `hellbeast` WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      string hellbeastName = "";
      string hellbeastOrigin = "";
      string hellbeastLocation = "";
      int hellbeastId = 0;

      while (rdr.Read())
      {
        hellbeastName = rdr.GetString(1);
        hellbeastOrigin = rdr.GetString(2);
        hellbeastLocation = rdr.GetString(3);
        hellbeastId = rdr.GetInt32(0);
      }

      HellBeast foundHellBeast = new HellBeast(hellbeastName, hellbeastOrigin, hellbeastLocation, hellbeastId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundHellBeast;
    }
  }
}
