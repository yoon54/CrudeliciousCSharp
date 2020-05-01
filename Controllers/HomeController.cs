using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using crudCSharp.Models;

namespace crudCSharp.Controllers
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
            List<Dish> AllDishes = dbContext.Dishes.OrderByDescending(d=>d.UpdatedAt).ToList();

            return View(AllDishes);
        }
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("create");
        }

        [HttpPost("addDish")]
        public IActionResult AddDish(Dish newDish)
        {
            dbContext.Add(newDish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost("{DishId}/editDish")]
        public IActionResult EditDish(Dish dish,int dishId)
        {
            Dish editDish = dbContext.Dishes.FirstOrDefault( d => d.DishId == dishId);
            editDish.Name = dish.Name;
            editDish.Chef = dish.Chef;
            editDish.Calories = dish.Calories;
            editDish.Tastiness = dish.Tastiness;
            editDish.Description = dish.Description;
            editDish.UpdatedAt = DateTime.Now;
            dbContext.SaveChanges();
            return View("ViewDish", editDish);
        }
        
        [HttpGet("/{DishId}")]
        public IActionResult ViewDish(int dishId)
        {
            Dish viewDish = dbContext.Dishes.FirstOrDefault( d => d.DishId == dishId);
            return View(viewDish);
        }

        [HttpGet("/{DishId}/edit")]
        public IActionResult Edit(int dishId)
        {
            Dish viewDish = dbContext.Dishes.FirstOrDefault( d => d.DishId == dishId);
            return View("edit" , viewDish);
        }
        [HttpGet("delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            Dish get = dbContext.Dishes.FirstOrDefault (i =>i.DishId == Id);
            dbContext.Dishes.Remove(get);
            dbContext.SaveChanges();
            return Redirect("/");
        }
        
    }
}