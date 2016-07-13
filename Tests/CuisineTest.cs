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
      System.Console.WriteLine("test id=" + newCuisine.GetId());
      Restaurant firstRestaurant = new Restaurant("McDonalds", newCuisine.GetId());
      System.Console.WriteLine("McD cuisine ID=" + firstRestaurant.GetCuisineId());
      firstRestaurant.Save();
      Restaurant secondRestaurant = new Restaurant("Dennys", 2);
      secondRestaurant.Save();
      List<Restaurant> expectedResult = new List<Restaurant> {firstRestaurant};
      //Act
      List<Restaurant> result = newCuisine.GetRestaurants();
      //Assert
      Assert.Equal(expectedResult, result);
    }

    public void Dispose()
    {
      Cuisine.DeleteAll();
      Restaurant.DeleteAll();
    }

  }
}