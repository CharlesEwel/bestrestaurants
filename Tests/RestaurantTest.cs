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


    public void Dispose()
    {
      // Animal.DeleteAll();
    }
  }
}
