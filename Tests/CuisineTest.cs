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

    public void Dispose()
    {
      // Restaurant.DeleteAll();
    }

  }
}
