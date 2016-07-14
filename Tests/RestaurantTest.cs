using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;


namespace BestRestaurant.Object
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurant_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_SavesSomethingToDatabase()
    {
      //Arrange
      Restaurant newRestaurant = new Restaurant("McDonalds", 1);

      //Act
      newRestaurant.Save();
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.Equal(1, result);
    }
    [Fact]
    public void Test_Equals_ReturnsTrueIfNameAndCuisineAreIdentical()
    {
      //Arrange
      Restaurant firstRestaurant = new Restaurant("McDonalds", 1);
      Restaurant secondRestaurant = new Restaurant("McDonalds", 1);

      //Assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }
    [Fact]
    public void Test_Save_SavesCorrectObjectToDatabase()
    {
      //Arrange
      Restaurant newRestaurant = new Restaurant("McDonalds", 1);

      //Act
      newRestaurant.Save();
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      //Assert
      Assert.Equal(newRestaurant, savedRestaurant);
    }
    [Fact]
    public void Test_Find_ReturnsASpecificRestaurantObject()
    {
      //Arrange
      Restaurant newRestaurant = new Restaurant("McDonalds", 1);
      newRestaurant.Save();

      //Act
      Restaurant foundRestaurant = Restaurant.Find(newRestaurant.GetId());

      //Assert
      Assert.Equal(newRestaurant, foundRestaurant);
    }
    [Fact]
    public void Test_DeleteOne_DeletesASpecificRestaurantObject()
    {
      //Arrange
      Restaurant firstRestaurant = new Restaurant("McDonalds", 1);
      firstRestaurant.Save();
      Restaurant secondRestaurant = new Restaurant("Dennys", 2);
      secondRestaurant.Save();

      //Act
      secondRestaurant.Delete();
      List<Restaurant> expectedRestaurant = new List<Restaurant> {firstRestaurant};
      List<Restaurant> testRestaurant= Restaurant.GetAll();

      //Assert
      Assert.Equal(expectedRestaurant, testRestaurant);
    }
    [Fact]
    public void Test_SetNameAndId_AdjustsDatabaseCorrectly()
    {
      // Arrange
      Restaurant firstRestaurant = new Restaurant("McDonalds", 1);
      firstRestaurant.Save();

      //Act
      firstRestaurant.SetName("Chipotle");
      firstRestaurant.SetCuisineId(2);
      Restaurant resultRestaurant = Restaurant.Find(firstRestaurant.GetId());

      //Assert
      Assert.Equal(firstRestaurant.GetName(), resultRestaurant.GetName());
      Assert.Equal(firstRestaurant.GetCuisineId(), resultRestaurant.GetCuisineId());
    }
    [Fact]
    public void Test_GetReviews_FindsReviewsByRestaurantId()
    {
      //Arrange
      Restaurant newRestaurant = new Restaurant("Fast Food", 1);
      newRestaurant.Save();

      Review firstReview = new Review("McDonalds", newRestaurant.GetId(), 2);

      firstReview.Save();
      Review secondReview = new Review("Dennys", 2, 2);
      secondReview.Save();
      List<Review> expectedResult = new List<Review> {firstReview};
      //Act
      List<Review> result = newRestaurant.GetReviews();
      //Assert
      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void Test_CalculateAverageRating_CorrectlySavesAveragesRatings()
    {
      //Arrange
      Restaurant newRestaurant = new Restaurant("McDonalds", 1);
      newRestaurant.Save();

      Review firstReview = new Review("Yummy", newRestaurant.GetId(), 2, 4);
      firstReview.Save();
      Review secondReview = new Review("Ok", newRestaurant.GetId(), 1, 3);
      secondReview.Save();

      //Act
      double result = newRestaurant.CalculateAverageRating();
      newRestaurant.SetAverageRating(result);
      double resultToCheck = newRestaurant.GetAverageRating();
      //Assert
      Assert.Equal(3.5, resultToCheck);
    }
    public void Dispose()
    {
      Restaurant.DeleteAll();
      Review.DeleteAll();
    }
  }
}
