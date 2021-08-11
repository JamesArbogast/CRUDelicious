using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            //Get all users
            ViewBag.AllDishes = dbContext.Dishes.ToList();
            return View();
        }

        [HttpGet("/home")]
        public IActionResult Home()
        {
            //Get all users
            ViewBag.AllDishes = dbContext.Dishes.ToList();
            return View("Index");
        }
        [HttpGet("/new")]
        public IActionResult NewDish(Dish newDish)
        {
            ViewBag.Dish = newDish;
            return View("AddDish");
        }
        [HttpGet("/dish/{dishID}")]
        public IActionResult ViewDish(Dish dish)
        {
            ViewBag.Dish = dbContext.Dishes.FirstOrDefault(d => d.DishId == dish.DishId);
            return View("NewDish", dish);
        }
        [HttpPost("/create")]
        public IActionResult CreateDish(Dish newDish)
        {
            dbContext.Add(newDish);
            ViewBag.Dish = newDish;
            dbContext.SaveChanges();
            return View("NewDish", newDish);
        }
        [HttpGet("/delete/{dishID}")]
        public IActionResult DeleteDish(int dishID)
        {
            Dish RetrievedDish = dbContext.Dishes
                .SingleOrDefault(d => d.DishId == dishID);
            
            // Then pass the object we queried for to .Remove() on Users
            dbContext.Dishes.Remove(RetrievedDish);
            
            // Finally, .SaveChanges() will remove the corresponding row representing this User from DB 
            dbContext.SaveChanges();
            ViewBag.AllDishes = dbContext.Dishes.ToList();
            return RedirectToAction("");
        }
        [HttpGet("/edit/{dishID}")]
        public IActionResult EditDish(int dishId)
        {
            Dish RetrievedDish = dbContext.Dishes
            .FirstOrDefault(dish => dish.DishId == dishId);
            // Then we may modify properties of this tracked model object
            RetrievedDish.Name = "New name";
            RetrievedDish.UpdatedAt = DateTime.Now;
            
            // Finally, .SaveChanges() will update the DB with these new values
            dbContext.SaveChanges();

            return RedirectToAction("/dish/{dishID}");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
