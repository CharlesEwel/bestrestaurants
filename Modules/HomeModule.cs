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
      Get["/"]=_=>View["index.cshtml"];

      Get["/cuisines"]=_=>{
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["cuisines.cshtml", allCuisines];
      };

      Get["/cuisine/new"]=_=>View["cuisine_new.cshtml"];

      Post["/cuisine/success"]=_=>{
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine-name"]);
        newCuisine.Save();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["cuisines.cshtml", allCuisines];
      };

      Get["/restaurants"]=_=>{
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        return View["restaurants.cshtml", allRestaurants];
      };

      Get["/restaurant/new"]=_=>{
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["restaurant_new.cshtml", allCuisines];
      };

      Post["/restaurant/success"]=_=>{
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"], Request.Form["cuisine-id"]);
        newRestaurant.Save();
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        return View["restaurants.cshtml", allRestaurants];
      };

    }
  }
}
