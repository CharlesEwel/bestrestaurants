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
      Get["/"]=_=>{
        if (User.GetAll().Count == 0)
        {
          User admin = new User("Admin", 1);
          admin.Save();
          User guest = new User("Guest", 1);
          guest.Save();
          guest.SetCurrentUser();
        }
        User currentUser = User.GetCurrentUser();
        return View["index.cshtml", currentUser];
      };

      Delete["/"]=_=>{
        Cuisine.DeleteAll();
        Restaurant.DeleteAll();
        Review.DeleteAll();
        User.DeleteAll();
        User admin = new User("Admin", 1);
        admin.Save();
        User guest = new User("Guest", 1);
        guest.Save();
        guest.SetCurrentUser();
        User currentUser = User.GetCurrentUser();
        return View["index.cshtml", currentUser];
      };

      Get["/login"]=_=>{
        List<User> allUsers = User.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>();
        User currentUser = User.GetCurrentUser();
        model.Add("currentUser", currentUser);
        model.Add("allUsers", allUsers);
        return View["log_in.cshtml", model];
      };

      Post["/login/success"]=_=>{
        User currentUser = User.Find(Request.Form["user-id"]);
        currentUser.SetCurrentUser();
        return View["index.cshtml", currentUser];
      };

      Get["/cuisines"]=_=>{
        List<Cuisine> allCuisines = Cuisine.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>();
        User currentUser = User.GetCurrentUser();
        model.Add("currentUser", currentUser);
        model.Add("allCuisines", allCuisines);
        return View["cuisines.cshtml", model];
      };

      Get["/cuisine/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedCuisine = Cuisine.Find(parameters.id);
        var cuisineRestaurants = selectedCuisine.GetRestaurants();
        var currentUser= User.GetCurrentUser();
        model.Add("currentUser", currentUser);
        model.Add("cuisine", selectedCuisine);
        model.Add("restaurants", cuisineRestaurants);
        return View ["cuisine.cshtml", model];
      };

      Delete["/cuisine/deleted/{id}"]=parameters=>{
        Cuisine selectedCuisine = Cuisine.Find(parameters.id);
        selectedCuisine.Delete();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>();
        User currentUser = User.GetCurrentUser();
        model.Add("currentUser", currentUser);
        model.Add("allCuisines", allCuisines);
        return View["cuisines.cshtml", model];
      };

      Get["/cuisine/new"]=_=>{
        User currentUser = User.GetCurrentUser();
        return View["cuisine_new.cshtml", currentUser];
      };

      Post["/cuisine/success"]=_=>{
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine-name"]);
        newCuisine.Save();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>();
        User currentUser = User.GetCurrentUser();
        model.Add("currentUser", currentUser);
        model.Add("allCuisines", allCuisines);
        return View["cuisines.cshtml", model];
      };

      Get["/restaurants"]=_=>{
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>();
        User currentUser = User.GetCurrentUser();
        model.Add("currentUser", currentUser);
        model.Add("allRestaurants", allRestaurants);
        return View["restaurants.cshtml", model];
      };

      Get["/restaurant/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedRestaurant = Restaurant.Find(parameters.id);
        var selectedReviews = selectedRestaurant.GetReviews();
        var allUsers = User.GetAll();
        Dictionary<int, string> usernames = new Dictionary<int, string>{};
        foreach(User reviewer in allUsers)
        {
          usernames.Add(reviewer.GetId(), reviewer.GetName());
        }
        var currentUser= User.GetCurrentUser();
        model.Add("currentUser", currentUser);
        model.Add("users", usernames);
        model.Add("restaurant", selectedRestaurant);
        model.Add("reviews", selectedReviews);
        return View["restaurant.cshtml", model];
      };

      Get["/restaurant/edit/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedRestaurant = Restaurant.Find(parameters.id);
        var allCuisines = Cuisine.GetAll();
        var currentUser= User.GetCurrentUser();
        model.Add("currentUser", currentUser);
        model.Add("restaurant", selectedRestaurant);
        model.Add("cuisines", allCuisines);
        return View["restaurant_edit.cshtml", model];
      };

      Delete["/restaurant/deleted/{id}"]=parameters=>{
        Restaurant selectedRestaurant = Restaurant.Find(parameters.id);
        selectedRestaurant.Delete();
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>();
        User currentUser = User.GetCurrentUser();
        model.Add("currentUser", currentUser);
        model.Add("allRestaurants", allRestaurants);
        return View["restaurants.cshtml", model];
      };

      Get["/restaurant/new"]=_=>{
        List<Cuisine> allCuisines = Cuisine.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>();
        User currentUser = User.GetCurrentUser();
        model.Add("currentUser", currentUser);
        model.Add("allCuisines", allCuisines);
        return View["restaurant_new.cshtml", model];
      };

      Post["/restaurant/success"]=_=>{
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"], Request.Form["cuisine-id"]);
        newRestaurant.Save();
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>();
        User currentUser = User.GetCurrentUser();
        model.Add("currentUser", currentUser);
        model.Add("allRestaurants", allRestaurants);
        return View["restaurants.cshtml", model];
      };

      Patch["/restaurant/success/{id}"]=parameters=>{
        var selectedRestaurant = Restaurant.Find(Request.Form["restaurant-id"]);
        selectedRestaurant.SetName(Request.Form["restaurant-name"]);
        selectedRestaurant.SetCuisineId(Request.Form["cuisine-id"]);
        Dictionary<string, object> model = new Dictionary<string, object>();
        var currentUser= User.GetCurrentUser();
        model.Add("currentUser", currentUser);
        var selectedReviews = selectedRestaurant.GetReviews();
        var allUsers = User.GetAll();
        Dictionary<int, string> usernames = new Dictionary<int, string>{};
        foreach(User reviewer in allUsers)
        {
          usernames.Add(reviewer.GetId(), reviewer.GetName());
        }
        model.Add("users", usernames);
        model.Add("restaurant", selectedRestaurant);
        model.Add("reviews", selectedReviews);
        return View["restaurant.cshtml", model];
      };

      Get["/review/new/{id}"]= parameters =>{
        Dictionary<string, object> model = new Dictionary<string, object>();
        var allUsers = User.GetAll();
        var restaurantToBeReviewed = Restaurant.Find(parameters.id);
        var currentUser= User.GetCurrentUser();
        model.Add("currentUser", currentUser);
        model.Add("users", allUsers);
        model.Add("restaurant", restaurantToBeReviewed);
        return View["review_new.cshtml", model];
      };

      Post["/review/success"]=_=>{
        Review newReview = new Review(Request.Form["review-content"], Request.Form["restaurant-id"], Request.Form["user-id"], Request.Form["rating"]);
        newReview.Save();
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedRestaurant = Restaurant.Find(Request.Form["restaurant-id"]);
        double newRating = selectedRestaurant.CalculateAverageRating();
        selectedRestaurant.SetAverageRating(newRating);
        selectedRestaurant = Restaurant.Find(Request.Form["restaurant-id"]);
        var selectedReviews = selectedRestaurant.GetReviews();
        var currentUser= User.GetCurrentUser();
        model.Add("currentUser", currentUser);
        var allUsers = User.GetAll();
        Dictionary<int, string> usernames = new Dictionary<int, string>{};
        foreach(User reviewer in allUsers)
        {
          usernames.Add(reviewer.GetId(), reviewer.GetName());
        }
        model.Add("users", usernames);
        model.Add("restaurant", selectedRestaurant);
        model.Add("reviews", selectedReviews);
        return View["restaurant.cshtml", model];
      };

      Get["/user/new"]=_=>{
        User currentUser = User.GetCurrentUser();
        return View["user_new.cshtml", currentUser];
      };

      Post["/user/success"]=_=>{
        bool isNameTaken = User.IsUserNameTaken(Request.Form["user-name"]);
        User currentUser = User.GetCurrentUser();
        if(isNameTaken)
        {
          return View["user_taken.cshtml", currentUser];
        }
        else
        {
          User newUser = new User(Request.Form["user-name"], Request.Form["display-preference"]);
          newUser.Save();
          newUser.SetCurrentUser();
          User currentUser2 = User.GetCurrentUser();
          return View["index.cshtml", currentUser2];
        }
      };

    }
  }
}
