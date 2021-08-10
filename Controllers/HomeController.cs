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
        [HttpGet("/new")]
        public IActionResult NewDish(Dish newDish)
        {
            ViewBag.Dish = newDish;
            return View("AddDish");
        }
        [HttpPost("/create")]
        public IActionResult CreateDish(Dish newDish)
        {
            dbContext.Add(newDish);
            ViewBag.Dish = newDish;
            dbContext.SaveChanges();
            return View("NewDish", newDish);
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
