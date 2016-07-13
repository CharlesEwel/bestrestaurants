using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BestRestaurant.Object
{
  public class Review
  {
    private int _id;
    private string _content;
    private int _restaurantId;

    public Review(string content, int restaurantId, int id = 0)
    {
      _content = content;
      _restaurantId = restaurantId;
      _id = id;
    }

    public int GetId()
    {
        return _id;
    }
    public string GetContent()
    {
        return _content;
    }
    public int GetRestaurantId()
    {
        return _restaurantId;
    }

    public void SetContent(string newContent)
    {
        _content = newContent;
        SqlConnection conn = DB.Connection();
        SqlDataReader rdr = null;
        conn.Open();

        SqlCommand cmd = new SqlCommand("UPDATE reviews SET content = @reviewContent where id = @id;", conn);

        SqlParameter contentParameter = new SqlParameter();
        contentParameter.ParameterName = "@reviewContent";
        contentParameter.Value = newContent;

        SqlParameter idParameter = new SqlParameter();
        idParameter.ParameterName = "@id";
        idParameter.Value = this.GetId();

        cmd.Parameters.Add(contentParameter);
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
    public void SetRestaurantId(int newRestaurantId)
    {
        _restaurantId = newRestaurantId;
        SqlConnection conn = DB.Connection();
        SqlDataReader rdr = null;
        conn.Open();

        SqlCommand cmd = new SqlCommand("UPDATE reviews SET restaurant_id = @restaurantId where id = @id;", conn);

        SqlParameter restaurantIdParameter = new SqlParameter();
        restaurantIdParameter.ParameterName = "@restaurantId";
        restaurantIdParameter.Value = newRestaurantId.ToString();

        SqlParameter idParameter = new SqlParameter();
        idParameter.ParameterName = "@id";
        idParameter.Value = this.GetId();

        cmd.Parameters.Add(restaurantIdParameter);
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

    public override bool Equals(System.Object otherReview)
    {
      if(!(otherReview is Review)) return false;
      else
      {
        Review newReview = (Review) otherReview;
        bool contentEquality = this.GetContent() == newReview.GetContent();
        bool restaurantEquality = this.GetRestaurantId() == newReview.GetRestaurantId();
        return(contentEquality && restaurantEquality);
      }
    }

    public static List<Review> GetAll()
    {
      List<Review> allReviews = new List<Review>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM reviews;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int reviewId = rdr.GetInt32(0);
        string reviewContent = rdr.GetString(1);

        int reviewRestaurantId = rdr.GetInt32(2);
        Review newReview = new Review(reviewContent, reviewRestaurantId, reviewId);
        allReviews.Add(newReview);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allReviews;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO reviews (content, restaurant_id) OUTPUT INSERTED.id VALUES (@reviewContent, @reviewRestaurantId);", conn);

      SqlParameter contentParameter = new SqlParameter();
      contentParameter.ParameterName = "@reviewContent";
      contentParameter.Value = this.GetContent();

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@reviewRestaurantId";
      restaurantIdParameter.Value = this.GetRestaurantId();

      cmd.Parameters.Add(contentParameter);
      cmd.Parameters.Add(restaurantIdParameter);

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
    public static Review Find(int reviewId)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM reviews WHERE id = @reviewId;", conn);

      SqlParameter reviewIdParameter = new SqlParameter();
      reviewIdParameter.ParameterName = "@reviewId";
      reviewIdParameter.Value = reviewId.ToString();

      cmd.Parameters.Add(reviewIdParameter);

      int foundReviewId = 0;
      string reviewContent = null;
      int reviewRestaurantId = 0;
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        foundReviewId = rdr.GetInt32(0);
        reviewContent = rdr.GetString(1);
        reviewRestaurantId = rdr.GetInt32(2);
      }
      Review newReview = new Review(reviewContent, reviewRestaurantId, foundReviewId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return newReview;
    }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM reviews WHERE id = @ReviewId;", conn);

      SqlParameter reviewIdParameter = new SqlParameter();
      reviewIdParameter.ParameterName = "@ReviewId";
      reviewIdParameter.Value = this.GetId();

      cmd.Parameters.Add(reviewIdParameter);
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
      SqlCommand cmd = new SqlCommand("DELETE FROM reviews;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
