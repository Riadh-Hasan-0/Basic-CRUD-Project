using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext Db;
        public CategoryController(ApplicationDbContext db)
        {
            this.Db = db;            
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList=this.Db.Categories;
            return View(objCategoryList);
        }

        //Get
        public IActionResult Create()
        {

            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category categoryObj)
        {
            if (categoryObj.Name == categoryObj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder and Name can not be same.");
            }
            if (ModelState.IsValid)
            {
                Db.Categories.Add(categoryObj);
                Db.SaveChanges();
                TempData["success"] = "Category Create Successfully";
                return RedirectToAction("Index");
            }
            return View(categoryObj);

        }
        //Get
        public IActionResult Edit(int? id)
        {
            if(id == null) { return NotFound(); }
            var categoryFormDb = Db.Categories.Find(id);
            if(categoryFormDb == null) { return NotFound(); }
            return View(categoryFormDb);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category categoryObj)
        {

            if (categoryObj.Name == categoryObj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder and Name can not be same.");
            }
            if (ModelState.IsValid)
            {
                Db.Categories.Update(categoryObj);
                Db.SaveChanges();
                TempData["success"] = "Category Edit Successfully";
                return RedirectToAction("Index");
            }
            return View(categoryObj);

        }
        public IActionResult Delete(int? id)
        {
            if (id == null) { return NotFound(); }
            var categoryFormDb = Db.Categories.Find(id);
            if (categoryFormDb == null) { return NotFound(); }
            return View(categoryFormDb);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category categoryObj)
        {

           
                Db.Categories.Remove(categoryObj);
                Db.SaveChanges();
                TempData["success"] = "Category Delete Successfully";
                return RedirectToAction("Index");

        }

    }
}
