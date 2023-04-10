using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext Db;
        public LoginController(ApplicationDbContext db)
        {
            this.Db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(User obj)
        {
            var userObj= Db.Users.SingleOrDefault(x=> x.Name==obj.Name);
            if (userObj == null) { return NotFound(); }
            if (userObj.Password != obj.Password)
            {
                TempData["error"] = "Worng username/password";
                return View(obj);
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
