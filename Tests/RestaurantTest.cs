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
    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
  }
}
