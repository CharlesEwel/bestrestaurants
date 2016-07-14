using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using BestRestaurant.Object;
using System;

namespace BestRestaurant
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      // Get["/"]=_=>View["index.cshtml"];
      //
      // Delete["/"]=_=>{
      //   Cuisine.DeleteAll();
      //   Restaurant.DeleteAll();
      //   return View["index.cshtml"];
      // };
      //
      // Get["/cuisines"]=_=>{
      //   List<Cuisine> allCuisines = Cuisine.GetAll();
      //   return View["cuisines.cshtml", allCuisines];
      // };
      //
      // Get["/cuisine/{id}"] = parameters => {
      //   Dictionary<string, object> model = new Dictionary<string, object>();
      //   var selectedCuisine = Cuisine.Find(parameters.id);
      //   var cuisineRestaurants = selectedCuisine.GetRestaurants();
      //   model.Add("cuisine", selectedCuisine);
      //   model.Add("restaurants", cuisineRestaurants);
      //   return View ["cuisine.cshtml", model];
      // };
      //
      // Delete["/cuisine/deleted/{id}"]=parameters=>{
      //   Cuisine selectedCuisine = Cuisine.Find(parameters.id);
      //   selectedCuisine.Delete();
      //   List<Cuisine> allCuisines = Cuisine.GetAll();
      //   return View["cuisines.cshtml", allCuisines];
      // };
      //
      // Get["/cuisine/new"]=_=>View["cuisine_new.cshtml"];
      //
      // Post["/cuisine/success"]=_=>{
      //   Cuisine newCuisine = new Cuisine(Request.Form["cuisine-name"]);
      //   newCuisine.Save();
      //   List<Cuisine> allCuisines = Cuisine.GetAll();
      //   return View["cuisines.cshtml", allCuisines];
      // };
      //
      // Get["/restaurants"]=_=>{
      //   List<Restaurant> allRestaurants = Restaurant.GetAll();
      //   return View["restaurants.cshtml", allRestaurants];
      // };
      //
      // Get["/restaurant/{id}"] = parameters => {
      //   Dictionary<string, object> model = new Dictionary<string, object>();
      //   var selectedRestaurant = Restaurant.Find(parameters.id);
      //   var selectedReviews = selectedRestaurant.GetReviews();
      //   model.Add("restaurant", selectedRestaurant);
      //   model.Add("reviews", selectedReviews);
      //   return View["restaurant.cshtml", model];
      // };
      //
      // Get["/restaurant/edit/{id}"] = parameters => {
      //   Dictionary<string, object> model = new Dictionary<string, object>();
      //   var selectedRestaurant = Restaurant.Find(parameters.id);
      //   var allCuisines = Cuisine.GetAll();
      //   model.Add("restaurant", selectedRestaurant);
      //   model.Add("cuisines", allCuisines);
      //   return View["restaurant_edit.cshtml", model];
      // };
      //
      // Delete["/restaurant/deleted/{id}"]=parameters=>{
      //   Restaurant selectedRestaurant = Restaurant.Find(parameters.id);
      //   selectedRestaurant.Delete();
      //   List<Restaurant> allRestaurants = Restaurant.GetAll();
      //   return View["restaurants.cshtml", allRestaurants];
      // };
      //
      // Get["/restaurant/new"]=_=>{
      //   List<Cuisine> allCuisines = Cuisine.GetAll();
      //   return View["restaurant_new.cshtml", allCuisines];
      // };
      //
      // Post["/restaurant/success"]=_=>{
      //   Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"], Request.Form["cuisine-id"]);
      //   newRestaurant.Save();
      //   List<Restaurant> allRestaurants = Restaurant.GetAll();
      //   return View["restaurants.cshtml", allRestaurants];
      // };
      //
      // Patch["/restaurant/success/{id}"]=parameters=>{
      //   var selectedRestaurant = Restaurant.Find(Request.Form["restaurant-id"]);
      //   selectedRestaurant.SetName(Request.Form["restaurant-name"]);
      //   selectedRestaurant.SetCuisineId(Request.Form["cuisine-id"]);
      //   Dictionary<string, object> model = new Dictionary<string, object>();
      //   var selectedReviews = selectedRestaurant.GetReviews();
      //   model.Add("restaurant", selectedRestaurant);
      //   model.Add("reviews", selectedReviews);
      //   return View["restaurant.cshtml", model];
      // };
      //
      // Get["/review/new/{id}"]= parameters =>{
      //   Restaurant restaurantToBeReviewed = Restaurant.Find(parameters.id);
      //   return View["review_new.cshtml", restaurantToBeReviewed];
      // };
      //
      // Post["/review/success"]=_=>{
      //   Review newReview = new Review(Request.Form["review-content"], Request.Form["restaurant-id"]);
      //   newReview.Save();
      //   Dictionary<string, object> model = new Dictionary<string, object>();
      //   var selectedRestaurant = Restaurant.Find(Request.Form["restaurant-id"]);
      //   var selectedReviews = selectedRestaurant.GetReviews();
      //   model.Add("restaurant", selectedRestaurant);
      //   model.Add("reviews", selectedReviews);
      //   return View["restaurant.cshtml", model];
      // };
    }
  }
}
