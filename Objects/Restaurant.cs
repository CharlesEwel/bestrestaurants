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
    private int _averageRating;

    public Restaurant(string name, int cuisineId, int id = 0, int averageRating = -1)
    {
      _name = name;
      _cuisineId = cuisineId;
      _id = id;
      _averageRating = averageRating;
    }

    public int GetId()
    {
        return _id;
    }
    public string GetName()
    {
        return _name;
    }
    public int GetCuisineId()
    {
        return _cuisineId;
    }

    public int GetAverageRating()
    {
      return _averageRating;
    }

    public void SetName(string newName)
    {
      _name = newName;
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE restaurants SET name = @restaurantName where id = @id;", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@restaurantName";
      nameParameter.Value = newName;

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@id";
      idParameter.Value = this.GetId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

    }
    public void SetCuisineId(int newCuisineId)
    {
      _cuisineId = newCuisineId;
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE restaurants SET cuisine_id = @cuisineId where id = @id;", conn);

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@cuisineId";
      cuisineIdParameter.Value = newCuisineId.ToString();

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@id";
      idParameter.Value = this.GetId();

      cmd.Parameters.Add(cuisineIdParameter);
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void SetAverageRating(int newAverageRating)
    {
      _averageRating = newAverageRating;
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE restaurants SET average_rating = @averageRating where id = @id;", conn);

      SqlParameter averageRatingParameter = new SqlParameter();
      averageRatingParameter.ParameterName = "@averageRating";
      averageRatingParameter.Value = newAverageRating.ToString();

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@id";
      idParameter.Value = this.GetId();

      cmd.Parameters.Add(averageRatingParameter);
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if(!(otherRestaurant is Restaurant)) return false;
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool nameEquality = this.GetName() == newRestaurant.GetName();
        bool cuisineEquality = this.GetCuisineId() == newRestaurant.GetCuisineId();
        return(nameEquality && cuisineEquality);
      }
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

    public List<Review> GetReviews()
    {
      List<Review> allReviewsMatchingRestaurant = new List<Review>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();


      SqlCommand cmd = new SqlCommand("SELECT * FROM reviews WHERE restaurant_id = @restaurantId;", conn);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@restaurantId";
      restaurantIdParameter.Value = this.GetId().ToString();



      cmd.Parameters.Add(restaurantIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int reviewId = rdr.GetInt32(0);
        string reviewName = rdr.GetString(1);
        int reviewRestaurantId = rdr.GetInt32(2);
        int reviewUserId = rdr.GetInt32(3);
        int reviewRating = rdr.GetInt32(4);
        Review newReview = new Review(reviewName, reviewRestaurantId, reviewUserId, reviewRating, reviewId);
        allReviewsMatchingRestaurant.Add(newReview);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allReviewsMatchingRestaurant;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, cuisine_id, average_rating) OUTPUT INSERTED.id VALUES (@restaurantName, @restaurantCuisineId, @averageRating);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@restaurantName";
      nameParameter.Value = this.GetName();

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@restaurantCuisineId";
      cuisineIdParameter.Value = this.GetCuisineId();

      SqlParameter averageRatingParameter = new SqlParameter();
      averageRatingParameter.ParameterName = "@averageRating";
      averageRatingParameter.Value = this.GetCuisineId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(cuisineIdParameter);
      cmd.Parameters.Add(averageRatingParameter);

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
    public static Restaurant Find(int restaurantId)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @restaurantId;", conn);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@restaurantId";
      restaurantIdParameter.Value = restaurantId.ToString();

      cmd.Parameters.Add(restaurantIdParameter);

      int foundRestaurantId = 0;
      string restaurantName = null;
      int restaurantCuisineId = 0;
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        foundRestaurantId = rdr.GetInt32(0);
        restaurantName = rdr.GetString(1);
        restaurantCuisineId = rdr.GetInt32(2);
      }
      Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCuisineId, foundRestaurantId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return newRestaurant;
    }
    public void Delete()
    {
      List<Review> reviewsToDelete = this.GetReviews();
      foreach (Review item in reviewsToDelete)
      {
        item.Delete();
      }
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants WHERE id = @RestaurantId;", conn);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value = this.GetId();

      cmd.Parameters.Add(restaurantIdParameter);
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
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
