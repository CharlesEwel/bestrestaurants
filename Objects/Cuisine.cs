using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BestRestaurant.Object
{
  public class Cuisine
  {
    private int _id;
    private string _name;

    public Cuisine(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public int GetId()
    {
        return _id;
    }
    public string GetName()
    {
        return _name;
    }

    public void SetName(string newName)
    {
        _name = newName;
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if(!(otherCuisine is Cuisine)) return false;
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool nameEquality = this.GetName() == newCuisine.GetName();
        return(nameEquality);
      }
    }

    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisines = new List<Cuisine>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisine;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int cuisineId = rdr.GetInt32(0);
        string cuisineName = rdr.GetString(1);
        Cuisine newCuisine = new Cuisine(cuisineName, cuisineId);
        allCuisines.Add(newCuisine);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allCuisines;
    }

    public List<Restaurant> GetRestaurants()
    {
      List<Restaurant> allRestaurantsMatchingCuisine = new List<Restaurant>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();


      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE cuisine_id = @cuisineId;", conn);

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@cuisineId";
      cuisineIdParameter.Value = this.GetId().ToString();



      cmd.Parameters.Add(cuisineIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        int restaurantCuisineId = rdr.GetInt32(2);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCuisineId, restaurantId);
        allRestaurantsMatchingCuisine.Add(newRestaurant);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allRestaurantsMatchingCuisine;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO cuisine (name) OUTPUT INSERTED.id VALUES (@cuisineName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@cuisineName";
      nameParameter.Value = this.GetName();

      cmd.Parameters.Add(nameParameter);


      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
    public static Cuisine Find(int cuisineId)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisine WHERE id = @cuisineId;", conn);

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@cuisineId";
      cuisineIdParameter.Value = cuisineId.ToString();

      cmd.Parameters.Add(cuisineIdParameter);

      int foundCuisineId = 0;
      string cuisineName = null;
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        foundCuisineId = rdr.GetInt32(0);
        cuisineName = rdr.GetString(1);
      }
      Cuisine newCuisine = new Cuisine(cuisineName, foundCuisineId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return newCuisine;
    }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM cuisine WHERE id = @CuisineId; DELETE FROM restaurants WHERE cuisine_id = @CuisineId;", conn);

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@CuisineId";
      cuisineIdParameter.Value = this.GetId();

      cmd.Parameters.Add(cuisineIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM cuisine;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
