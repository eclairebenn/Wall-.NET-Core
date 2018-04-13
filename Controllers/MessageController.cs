using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using the_wall.Models;

namespace the_wall.Controllers
{
    public class MessageController : Controller
    {
        private readonly DbConnector _dbConnector;
 
        public MessageController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            int? user_id = HttpContext.Session.GetInt32("user_id");
            var User = _dbConnector.Query($"SELECT * FROM users WHERE id={user_id}");
            ViewBag.User = User[0];

            List<Dictionary<string, object>> messages = _dbConnector.Query($"SELECT users.firstname, users.lastname, messages.* FROM users JOIN messages on users.id = messages.user_id ORDER BY messages.created_at DESC;");
            List<Dictionary<string, object>> comments = _dbConnector.Query($"SELECT users.firstname, users.lastname, comments.* FROM users JOIN comments on users.id = comments.user_id;");
            ViewBag.messages = messages;
            ViewBag.comments = comments;
            return View("Success");
        }

        [HttpPost]
        [Route("post/message")]
        public IActionResult PostMessage(Message message, int user_id)
        {
            if(ModelState.IsValid)
            {
                _dbConnector.Execute($"INSERT INTO messages (content, user_id, created_at, updated_at) VALUES ('{message.content}', {user_id}, NOW(), NOW())");
            }
            return RedirectToAction("Success");
        }
        [HttpPost]
        [Route("post/comment")]
        public IActionResult PostComment(string comment, int user_id, int message_id)
        {
            if(comment.Length>2)
            {
                _dbConnector.Execute($"INSERT INTO comments (content, user_id, message_id, created_at, updated_at) VALUES ('{comment}', {user_id}, {message_id}, NOW(), NOW())");
            }
            return RedirectToAction("Success");
        }
    }
}