using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using wall.Models;
using DbConnection;

namespace wall.Controllers
{
    public class UserController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("log")]
        public IActionResult Log()
        {
            return View("IndexLog");
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid){
                DbConnector.Execute($"INSERT INTO users (first_name, last_name, email, password, created_at, updated_at) VALUES ('{model.first_name}', '{model.last_name}', '{model.email}', '{model.password}', NOW(), NOW())");
                List<Dictionary<string,object>> NewUser = DbConnector.Query("SELECT LAST_INSERT_ID()");
                object newUserId = NewUser[0]["LAST_INSERT_ID()"];
                HttpContext.Session.SetInt32("UserId", Convert.ToInt32(newUserId));
                HttpContext.Session.SetString("UserName", model.first_name);
                return RedirectToAction("Wall", "Wall");
            }
            return View("Index", model);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel model)
        {
            List<Dictionary<string, object>> found = DbConnector.Query($"SELECT * FROM users WHERE email = '{model.email}'");
            if (found.Count == 0){
                ViewBag.error = "User not found";
                return View("RegisterForm");
            } else if ((string)found[0]["password"] != model.password) {
                ViewBag.error = "Incorrect password";
                return View("RegisterForm");
            } else {
                HttpContext.Session.SetInt32("UserId", Convert.ToInt32(found[0]["id"]));
                HttpContext.Session.SetString("UserName", (string)found[0]["first_name"]);
                return RedirectToAction("Wall", "Wall");
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}