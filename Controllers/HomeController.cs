using MessageBoard.Data;
using MessageBoard.Models;
using MessageBoard.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MessageBoard.Controllers
{
    public class HomeController : Controller
    {
        private IMailService            _mail;
        private IMessageBoardRepository _repo;

        public HomeController(IMailService mail, IMessageBoardRepository repo)
        {
            _mail = mail;
            _repo = repo;
        }

        public ActionResult Index()
        {
            var topics = _repo.GetTopics()
                              .OrderByDescending(t => t.CreatedDate)
                              .Take(25)
                              .ToList();
            return View(topics);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            var msg = string.Format("Comment From: {1}{0}Email:{2}{0}Website: {3}{0}Comment", 
                Environment.NewLine, model.Name, model.Email, model.Website, model.Comment);
            if (_mail.SendMail("noreply@yourdomain.com", "wgimson@gmail.com", "Website Contact", msg))
            {
                ViewBag.MailSent = true;
            }
            return View();
        }

        [Authorize]
        public ActionResult MyMessages()
        {
            return View();
        }
    }
}