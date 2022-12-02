using MessengerApplication.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MessengerApplication.Controllers
{
    public class HomeController : Controller
    {
        private MessengerContext db = new MessengerContext();

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(User user)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.All(u => u.Name != user.Name))
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                Session["userId"] = db.Users.First(u => u.Name == user.Name).Id;
                return RedirectToAction("MyMessages");
            }
            return View();
        }

        public ActionResult Send()
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return RedirectToAction("SignIn");
            ViewBag.Receivers = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Send(Message message)
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return RedirectToAction("SignIn");
            if (!ModelState.IsValid)
            {
                ViewBag.Receivers = new SelectList(db.Users, "Id", "Name");
                return View(message);
            }
            message.Sender_Id = userId;
            db.Messages.Add(message);
            db.Users.First(u => u.Id == message.Receiver_Id).HasNewMessages = true;
            db.SaveChanges();
            return RedirectToAction("MyMessages");
        }

        public ActionResult MyMessages()
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return RedirectToAction("SignIn");
            ViewBag.User = userId;
            return View();
        }

        public ActionResult ReceivedMessages()
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return RedirectToAction("SignIn");
            var receivedMessages = db.Messages.Where(m => m.Receiver_Id == userId);
            return PartialView(receivedMessages);
        }

        public ActionResult SentMessages()
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return RedirectToAction("SignIn");
            var sentMessages = db.Messages.Where(m => m.Sender_Id == userId);
            return PartialView(sentMessages);
        }

        public int GetCurrentUserId()
        {
            var user = Session["userId"];
            if (user == null) return 0;
            return Int32.Parse(user.ToString());
        }
    }
}