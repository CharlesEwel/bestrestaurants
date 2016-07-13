using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BestRestaurant.Object
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private int _cuisineId;

    public Restaurant(string name, int cuisineId, int id = 0)
    {
      _name = name;
      _cuisineId = cuisineId;
      _id = id;
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);

        int restaurantCuisineId = rdr.GetInt32(2);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCuisineId, restaurantId);
        allRestaurants.Add(newRestaurant);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allRestaurants;
    }
  }
}
