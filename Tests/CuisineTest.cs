using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;


namespace BestRestaurant.Object
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurant_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Cuisine.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_SavesCorrectObjectToDatabase()
    {
      //Arrange
      Cuisine newCuisine = new Cuisine("Fast Food");

      //Act
      newCuisine.Save();
      Cuisine savedCuisine = Cuisine.GetAll()[0];

      //Assert
      Assert.Equal(newCuisine, savedCuisine);
    }
    [Fact]
    public void Test_Find_ReturnsASpecificCuisineObject()
    {
      //Arrange
      Cuisine newCuisine = new Cuisine("Fast Food");
      newCuisine.Save();

      //Act
      Cuisine foundCuisine = Cuisine.Find(newCuisine.GetId());

      //Assert
      Assert.Equal(newCuisine, foundCuisine);
    }
    [Fact]
    public void Test_GetRestaurants_FindsRestaurantsByCuisineId()
    {
      //Arrange
      Cuisine newCuisine = new Cuisine("Fast Food");
      newCuisine.Save();

      Restaurant firstRestaurant = new Restaurant("McDonalds", newCuisine.GetId());

      firstRestaurant.Save();
      Restaurant secondRestaurant = new Restaurant("Dennys", 2);
      secondRestaurant.Save();
      List<Restaurant> expectedResult = new List<Restaurant> {firstRestaurant};
      //Act
      List<Restaurant> result = newCuisine.GetRestaurants();
      //Assert
      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void Test_Delete_DeletesCuisineAndRestaurantsByCuisineId()
    {
      //Arrange
      Cuisine firstCuisine = new Cuisine("Fast Food");
      firstCuisine.Save();
      Cuisine secondCuisine = new Cuisine("Mexican");
      secondCuisine.Save();

      Restaurant firstRestaurant = new Restaurant("McDonalds", firstCuisine.GetId());
      firstRestaurant.Save();
      Restaurant secondRestaurant = new Restaurant("Chipotle", secondCuisine.GetId());
      secondRestaurant.Save();

      List<Cuisine> expectedCuisine = new List<Cuisine>{firstCuisine};
      List<Restaurant> expectedResult = new List<Restaurant> {firstRestaurant};
      //Act
      secondCuisine.Delete();

      List<Cuisine> resultingCuisine = Cuisine.GetAll();
      List<Restaurant> result = Restaurant.GetAll();
      //Assert
      Assert.Equal(expectedCuisine, resultingCuisine);
      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void Test_Delete_DeletesCuisineAndRestaurantsAndReviewsByCuisineId()
    {
      //Arrange
      Cuisine firstCuisine = new Cuisine("Fast Food");
      firstCuisine.Save();
      Cuisine secondCuisine = new Cuisine("Mexican");
      secondCuisine.Save();

      Restaurant firstRestaurant = new Restaurant("McDonalds", firstCuisine.GetId());
      firstRestaurant.Save();
      Restaurant secondRestaurant = new Restaurant("Chipotle", secondCuisine.GetId());
      secondRestaurant.Save();

      Review firstReview = new Review("Test", firstRestaurant.GetId());
      firstReview.Save();
      Review secondReview = new Review("Test2", secondRestaurant.GetId());
      secondReview.Save();

      List<Cuisine> expectedCuisine = new List<Cuisine>{firstCuisine};
      List<Restaurant> expectedRestaurant = new List<Restaurant> {firstRestaurant};
      List<Review> expectedReview = new List<Review> {firstReview};

      //Act
      secondCuisine.Delete();

      List<Cuisine> resultingCuisine = Cuisine.GetAll();
      List<Restaurant> resultingRestaurant = Restaurant.GetAll();
      List<Review> resultingReview = Review.GetAll();
      //Assert
      Assert.Equal(expectedCuisine, resultingCuisine);
      Assert.Equal(expectedRestaurant, resultingRestaurant);
      Assert.Equal(expectedReview, resultingReview);
    }

    public void Dispose()
    {
      Cuisine.DeleteAll();
      Restaurant.DeleteAll();
      Review.DeleteAll();
    }

  }
}
