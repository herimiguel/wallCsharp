using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using wall.Models;
using DbConnection;

namespace wall.Controllers
{
    public class WallController : Controller
    {
        
        [HttpGet]
        [Route("wall")]
        public IActionResult Wall()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            List<Dictionary<string,object>> AllMessages = DbConnector.Query("select concat(first_name, ' ', last_name) as name, date_format(messages.created_at, '%b %D %Y') as date, message, messages.created_at, messages.id, messages.user_id from messages join users on user_id=users.id order by created_at desc");
            ViewBag.AllMessages = AllMessages;
            List<Dictionary<string,object>> AllComments = DbConnector.Query("select concat(first_name, ' ', last_name) as name, date_format(messages.created_at, '%b %D %Y') as date, comment, comments.created_at, message_id, comments.user_id from comments join messages on message_id=messages.id join users on comments.user_id= users.id order by created_at");
            ViewBag.AllComments = AllComments;
            return View();
        }

        [HttpPost]
        [Route("postMessage")]
        public IActionResult PostMessage(MessageViewModel model)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            DbConnector.Execute($"INSERT INTO messages (user_id, message, created_at, updated_at) VALUES ({UserId}, '{model.message}', NOW(), NOW())");
            return RedirectToAction("Wall");
        }

        [HttpPost]
        [Route("postComment")]
        public IActionResult PostComment(string comment, int message_id)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            DbConnector.Execute($"INSERT INTO comments (user_id, message_id, comment, created_at, updated_at) VALUES ({UserId}, {message_id}, '{comment}', NOW(), NOW())");
            return RedirectToAction("Wall");
        }

    }
}