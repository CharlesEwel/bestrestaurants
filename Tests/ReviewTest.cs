using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;


namespace BestRestaurant.Object
{
  public class ReviewTest : IDisposable
  {
    public ReviewTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurant_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Review.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_SavesSomethingToDatabase()
    {
      //Arrange
      Review newReview = new Review("McDonalds", 1, 2);

      //Act
      newReview.Save();
      int result = Review.GetAll().Count;

      //Assert
      Assert.Equal(1, result);
    }
    [Fact]
    public void Test_Equals_ReturnsTrueIfContentAndRestaurantAreIdentical()
    {
      //Arrange
      Review firstReview = new Review("McDonalds", 1, 2);
      Review secondReview = new Review("McDonalds", 1, 2);

      //Assert
      Assert.Equal(firstReview, secondReview);
    }
    [Fact]
    public void Test_Save_SavesCorrectObjectToDatabase()
    {
      //Arrange
      Review newReview = new Review("McDonalds", 1, 2);

      //Act
      newReview.Save();
      Review savedReview = Review.GetAll()[0];

      //Assert
      Assert.Equal(newReview, savedReview);
    }
    [Fact]
    public void Test_Find_ReturnsASpecificReviewObject()
    {
      //Arrange
      Review newReview = new Review("McDonalds", 1, 2);
      newReview.Save();

      //Act
      Review foundReview = Review.Find(newReview.GetId());

      //Assert
      Assert.Equal(newReview, foundReview);
    }
    [Fact]
    public void Test_DeleteOne_DeletesASpecificReviewObject()
    {
      //Arrange
      Review firstReview = new Review("McDonalds", 1, 2);
      firstReview.Save();
      Review secondReview = new Review("Dennys", 2, 2);
      secondReview.Save();

      //Act
      secondReview.Delete();
      List<Review> expectedReview = new List<Review> {firstReview};
      List<Review> testReview= Review.GetAll();

      //Assert
      Assert.Equal(expectedReview, testReview);
    }
    [Fact]
    public void Test_SetContentAndId_AdjustsDatabaseCorrectly()
    {
      // Arrange
      Review firstReview = new Review("McDonalds", 1, 2);
      firstReview.Save();

      //Act
      firstReview.SetContent("Chipotle");
      firstReview.SetRestaurantId(2);
      Review resultReview = Review.Find(firstReview.GetId());

      //Assert
      Assert.Equal(firstReview.GetContent(), resultReview.GetContent());
      Assert.Equal(firstReview.GetRestaurantId(), resultReview.GetRestaurantId());
    }
    public void Dispose()
    {
      Review.DeleteAll();
    }
  }
}
