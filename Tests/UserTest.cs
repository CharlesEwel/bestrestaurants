using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;


namespace BestRestaurant.Object
{
  public class UserTest : IDisposable
  {
    public UserTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurant_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = User.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_SavesCorrectObjectToDatabase()
    {
      //Arrange
      User newUser = new User("Bob", 2);

      //Act
      newUser.Save();
      User savedUser = User.GetAll()[0];

      //Assert
      Assert.Equal(newUser, savedUser);
    }
    [Fact]
    public void Test_Find_ReturnsASpecificUserObject()
    {
      //Arrange
      User newUser = new User("Bob", 2);
      newUser.Save();

      //Act
      User foundUser = User.Find(newUser.GetId());

      //Assert
      Assert.Equal(newUser, foundUser);
    }
    [Fact]
    public void Test_GetReviews_FindsReviewsByUserId()
    {
      //Arrange
      User newUser = new User("Bob", 2);
      newUser.Save();

      Review firstReview = new Review("McDonalds", 2, newUser.GetId());

      firstReview.Save();
      Review secondReview = new Review("Dennys", 2, 2);
      secondReview.Save();
      List<Review> expectedResult = new List<Review> {firstReview};
      //Act
      List<Review> result = newUser.GetReviews();
      //Assert
      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void Test_Delete_DeletesUserAndReviewsByUserId()
    {
      //Arrange
      User firstUser = new User("Bob", 2);
      firstUser.Save();
      User secondUser = new User("Larry", 2);
      secondUser.Save();

      Review firstReview = new Review("McDonalds", 2, firstUser.GetId());
      firstReview.Save();
      Review secondReview = new Review("Chipotle", 2, secondUser.GetId());
      secondReview.Save();

      List<User> expectedUser = new List<User>{firstUser};
      List<Review> expectedResult = new List<Review> {firstReview};
      //Act
      secondUser.Delete();

      List<User> resultingUser = User.GetAll();
      List<Review> result = Review.GetAll();
      //Assert
      Assert.Equal(expectedUser, resultingUser);
      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void Test_SetDisplayPreference_AdjustsDatabaseCorrectly()
    {
      // Arrange
      User firstUser = new User("Bob", 1);
      firstUser.Save();

      //Act
      firstUser.SetDisplayPreference(2);
      User resultUser = User.Find(firstUser.GetId());

      //Assert
      Assert.Equal(firstUser.GetDisplayPreference(), resultUser.GetDisplayPreference());
    }
    [Fact]
    public void Test_SetCurrentUser_AdjustsDatabaseCorrectly()
    {
      // Arrange
      User firstUser = new User("Bob", 1);
      firstUser.Save();

      //Act
      firstUser.SetCurrentUser();
      User resultUser = User.GetCurrentUser();

      //Assert
      Assert.Equal(firstUser, resultUser);
    }


    public void Dispose()
    {
      User.DeleteAll();
      Review.DeleteAll();
    }

  }
}
