using System;
using MySql.Data.MySqlClient;
using AnimalShelterDatabase;
using System.Collections.Generic;

namespace AnimalShelter.Models
{
  public class DB
  {
    public static MySqlConnection Connection()
    {
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      return conn;
    }
  }
}
