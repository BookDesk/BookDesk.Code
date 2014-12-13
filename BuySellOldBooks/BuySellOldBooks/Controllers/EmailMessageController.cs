using BuySellOldBooks.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace BuySellOldBooks.Controllers
{
    public class EmailMessageController : Controller
    {
        private PustakContext db = new PustakContext();

        //GET
        public ActionResult SendEmailMessage(string buyerContact, string temp, int id = 0)
        {
            int userID = 0;
            string emailIdBookOwner = string.Empty;
            string subject = string.Empty;
            string messageBody = string.Empty;
            BuySellOldBooks.Models.Book bookDetailToMail=null;

            using (PustakContext db = new PustakContext())
            {
                var book = from bookDetail in db.Books where bookDetail.BookID == id select bookDetail;

                foreach (var bookInfo in book)
                {
                    userID = bookInfo.UserId;
                    bookDetailToMail = bookInfo;                   
                }
 
                //TRACE -can we use virtual property of Book Model here to find associated User.
                var bookUser = from userDetail in db.Users where userDetail.UserId == userID select userDetail;
                foreach (var userInfo in bookUser)
                {
                    emailIdBookOwner = userInfo.Email;
                }

                ViewBag.UserMailId = emailIdBookOwner;
                TempData["EmailIdBookOwner"] = emailIdBookOwner;
                ViewBag.BookDetailToSend = bookDetailToMail;
            }

            return View();
        }
               
        //Post
        [HttpPost]
        public ActionResult SendEmailMessage(string buyerContact,  int id = 0)
        {
            string formattedMailBody = string.Empty;
            using (PustakContext db = new PustakContext())
            {
                var bookInfo= from bookDetail in db.Books where bookDetail.BookID==id select bookDetail;
                foreach (var bookToSell in bookInfo)
                {
                    string bookTitle = bookToSell.Title;
                    string bookAuthor = bookToSell.Author;
                    string bookEdition = bookToSell.Edition;

                    formattedMailBody = "<table>  <tr><td>Hello,</td></tr><tr><td>I want to purchase your book that you have uploaded on <i>BookDesk.in</i></td></tr>  <tr><td><b>Book Detail</b></td></tr> <tr> <td><b>Title:</b>" + bookTitle + "</td><td><b>Author:</b>" + bookAuthor + "</td><td><b>Edition</b>" + bookEdition + "</td>  </tr> <tr><td>Please Contact me On:</td><td>" + buyerContact + "</td></tr> </table>";
                }
            }

            //Send Mail Logic Here. 
            string email = ConfigurationSettings.AppSettings["HosterEmailAddress"];       
            string password = ConfigurationSettings.AppSettings["HosterEmailPassword"];   
            string host = ConfigurationSettings.AppSettings["HosterEmailHost"];
            int port = Convert.ToInt16(ConfigurationSettings.AppSettings["HosterEmailPort"]);
            string emailSubject = ConfigurationSettings.AppSettings["HosterEmailSubject"];

            string emailsToNotifyWhenSomebodySendMail = ConfigurationSettings.AppSettings["adminEmailID"];

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient(host,port);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(TempData["EmailIdBookOwner"].ToString()));
            msg.CC.Add(emailsToNotifyWhenSomebodySendMail);
            msg.Subject = emailSubject; //"Notification! from Book Desk - Sell Your Old Book";
            msg.Body = formattedMailBody;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = false;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);

            TempData["MsgSentFlag"] = "1";

            //EmailMessage/MailAcknowledgement
            return RedirectToAction("SearchBooks", "Book");
        }

        public ActionResult Cancel()
        {
            return RedirectToAction("SearchBooks","Book");
        }

        public ActionResult MailAcknowledgement()
        {
            ViewBag.message = "This is a partial view";
            return PartialView();
        }
    }
}
