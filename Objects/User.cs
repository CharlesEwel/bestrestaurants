using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BestRestaurant.Object
{
  public class User
  {
    private int _id;
    private string _name;
    private int _displayPreference;

    public User(string name, int displayPreference, int id = 0)
    {
      _name = name;
      _displayPreference = displayPreference;
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
    public int GetDisplayPreference()
    {
        return _displayPreference;
    }

    public void SetName(string newName)
    {
        _name = newName;
    }
    public void SetDisplayPreference(int displayPreference)
    {
        _displayPreference = displayPreference;
        SqlConnection conn = DB.Connection();
        SqlDataReader rdr = null;
        conn.Open();

        SqlCommand cmd = new SqlCommand("UPDATE users SET display_preferences = @displayPreference where id = @id;", conn);

        SqlParameter displayParameter = new SqlParameter();
        displayParameter.ParameterName = "@displayPreference";
        displayParameter.Value = displayPreference;

        SqlParameter idParameter = new SqlParameter();
        idParameter.ParameterName = "@id";
        idParameter.Value = this.GetId();

        cmd.Parameters.Add(displayParameter);
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

    public override bool Equals(System.Object otherUser)
    {
      if(!(otherUser is User)) return false;
      else
      {
        User newUser = (User) otherUser;
        bool nameEquality = this.GetName() == newUser.GetName();
        bool displayEquality = this.GetDisplayPreference() == newUser.GetDisplayPreference();
        return(nameEquality && displayEquality);
      }
    }

    public static List<User> GetAll()
    {
      List<User> allUsers = new List<User>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM users;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int userId = rdr.GetInt32(0);
        string userName = rdr.GetString(1);
        int displayPreference = rdr.GetInt32(2);
        User newUser = new User(userName, displayPreference, userId);
        allUsers.Add(newUser);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allUsers;
    }

    public List<Review> GetReviews()
    {
      List<Review> allReviewsMatchingUser = new List<Review>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();


      SqlCommand cmd = new SqlCommand("SELECT * FROM reviews WHERE user_id = @userId;", conn);

      SqlParameter userIdParameter = new SqlParameter();
      userIdParameter.ParameterName = "@userId";
      userIdParameter.Value = this.GetId().ToString();



      cmd.Parameters.Add(userIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int reviewId = rdr.GetInt32(0);
        string reviewContent = rdr.GetString(1);
        int restaurantId = rdr.GetInt32(2);
        int reviewUserId = rdr.GetInt32(3);
        int reviewRating = rdr.GetInt32(4);
        Review newReview = new Review(reviewContent, restaurantId, reviewUserId, reviewRating, reviewId);
        allReviewsMatchingUser.Add(newReview);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allReviewsMatchingUser;
    }

    public static bool IsUserNameTaken(string username)
    {
      List<User> takenUsernames = User.GetAll();
      bool IsTaken = false;
      foreach(User takenUsername in takenUsernames)
      {
        if(takenUsername.GetName() == username)
        {
          IsTaken = true;
        }
      }
      return IsTaken;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO users (username, display_preferences) OUTPUT INSERTED.id VALUES (@userName, @userDisplayPreference);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@userName";
      nameParameter.Value = this.GetName();

      SqlParameter displayParameter = new SqlParameter();
      displayParameter.ParameterName = "@userDisplayPreference";
      displayParameter.Value = this.GetDisplayPreference();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(displayParameter);


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
    public static User Find(int userId)
    {

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE id = @userId;", conn);

      SqlParameter userIdParameter = new SqlParameter();
      userIdParameter.ParameterName = "@userId";
      userIdParameter.Value = userId.ToString();

      cmd.Parameters.Add(userIdParameter);

      int foundUserId = 0;
      string userName = null;
      int userDisplayPreference = 0;
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        foundUserId = rdr.GetInt32(0);
        userName = rdr.GetString(1);
        userDisplayPreference = rdr.GetInt32(2);
      }
      User newUser = new User(userName, userDisplayPreference, foundUserId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return newUser;
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

      SqlCommand cmd = new SqlCommand("DELETE FROM users WHERE id = @UserId;", conn);

      SqlParameter userIdParameter = new SqlParameter();
      userIdParameter.ParameterName = "@UserId";
      userIdParameter.Value = this.GetId();

      cmd.Parameters.Add(userIdParameter);
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
      SqlCommand cmd = new SqlCommand("DELETE FROM users;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
