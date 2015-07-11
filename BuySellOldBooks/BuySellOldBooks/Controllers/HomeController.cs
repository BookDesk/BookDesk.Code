using BuySellOldBooks.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;
using System.Web.Mvc;
using PagedList;

//for baq 11 july
namespace BuySellOldBooks.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string name, string phoneNumber, string email, string message)
        {
            //SEND USER QUERY AS AN E-MAIL TO ADMIN
            //****START** SENDING MAIL
            string adminEmail   = ConfigurationSettings.AppSettings["HosterEmailAddress"];
            string password     = ConfigurationSettings.AppSettings["HosterEmailPassword"];
            string host         = ConfigurationSettings.AppSettings["HosterEmailHost"];
            int port            = Convert.ToInt16(ConfigurationSettings.AppSettings["HosterEmailPort"]);

            string emailsToRecvUserFeedback = ConfigurationSettings.AppSettings["adminEmailID"];

            var loginInfo = new NetworkCredential(adminEmail, password);
            var msg = new System.Net.Mail.MailMessage();
            var smtpClient = new System.Net.Mail.SmtpClient(host, port);

            //Attachment attachment = new System.Net.Mail.Attachment(fileToAttach);
            //msg.Attachments.Add(attachment);

            //TRACE ContctUs Send email to user address that is wrong it should send feedback orQuery to admin email-id
            msg.From = new MailAddress(adminEmail);
            msg.To.Add(new MailAddress(emailsToRecvUserFeedback));
            msg.Subject = "User Feedback/Query from BookDesk.in";
            msg.Body = "<table><tr><td><b>Name:</b> " + name + "</td></tr>  <tr><td><b>Phone Number:</b> " + phoneNumber + "</td></tr>  <tr><td><b>E-mail:</b> " + email + "</td></tr></tr>  <tr><td><b>Message:</b> " + message + "</td></tr></table>";
            msg.IsBodyHtml = true;

            //smtpClient.Timeout = int.Parse(ConfigurationSettings.AppSettings["smtpClientTimeout"]);     // 100000;       
            smtpClient.EnableSsl = false;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);

            TempData["MsgSentFlag"] = "1";
            //****END** SEND MAIL

            return RedirectToAction("SearchBooks", "Book");
        }

        [HttpGet]
        public ActionResult FrequentlyAskedQuestions()
        {
            ViewBag.Title = "Frequently Asked Questions";
            return View();
        }

        [HttpGet]
        public ActionResult PrivacyPolicy()
        {
            ViewBag.Title = "Privacy Policy";
            return View();
        }

        [HttpGet]
        public ActionResult SiteMap()
        {
            ViewBag.Title = "Site Map";
            return View();
        }
    }
}
