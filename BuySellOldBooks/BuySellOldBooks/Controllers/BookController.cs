using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuySellOldBooks.Models;
using BuySellOldBooks.DataAccessLayer;
using PagedList;
using WebMatrix.WebData;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace BuySellOldBooks.Controllers
{
    public class BookController : Controller
    {
        private PustakContext db = new PustakContext();
        //Window edit 5 
        //TEST EDIT on 12-Dec 1:42
        //Web Edit2
        //Web edit4
        //Win edit 6

        //Search Books Mathod //GET Method 
        public ActionResult SearchBooks(string searchKeyword, string cities, string categories, string SortKey, string currentFilter, string userName, int? page, string SpecialStatus)
        {
            ViewBag.CurrentSort = SortKey;

            var categoryList = new List<string>();
            var categoryQry = from cat in db.Categories orderby cat.CategoryName select cat.CategoryName;   //Retrive all categories from DB
            categoryList.AddRange(categoryQry.Distinct());
            ViewBag.bookCategory = new SelectList(categoryList);
            ViewBag.categoriesList = categoryList;  //Add distinct catagories to view bag object

            var cityList = new List<string>();                      //Retrive all Cities from DB
            var cityQry = from cit in db.Cities orderby cit.CityName select cit.CityName;
            cityList.AddRange(cityQry.Distinct());                  //Add distinct Cities to view bag object
            ViewBag.bookCity = new SelectList(cityList);

            //Add sorting keywords to list and add to view bag object
            var sortingList = new List<string>();
            sortingList.Add("Title"); sortingList.Add("Author"); sortingList.Add("Price"); sortingList.Add("Publisher");
            ViewBag.sortingItem = new SelectList(sortingList);

            //Implement Paging
            if (searchKeyword != null)
                page = 1;
            else
                searchKeyword = currentFilter;

            ViewBag.CurrentFilter = searchKeyword;

            //Retrive all books from DB.
            var books = from book in db.Books where book.IsActive == true select book;

            //Select Specials books only uploaded by Admin (If requests comes from Special link)
            ViewBag.SpecialStatus = "0";
            if (SpecialStatus == "1")
            {
                books = books.Where(i => (i.Locality == "admin"));
                ViewBag.SpecialStatus = "1";
            }

            //Implement search functionality
            if ((!String.IsNullOrEmpty(searchKeyword)) && (searchKeyword != "Enter Book Title or Author to Search"))
                books = books.Where(i => (i.Title.Contains(searchKeyword)) || (i.Author.Contains(searchKeyword)) || (i.Publisher.Contains(searchKeyword)));

            //Display book by selected catagories
            if (!String.IsNullOrEmpty(categories) && (!(categories == "Select")))
                books = books.Where(catg => catg.Category == categories);

            //Display books by selected Cities.
            if (!String.IsNullOrEmpty(cities) && (!(cities == "Select City")))
                books = books.Where(cit => cit.City == cities);

            //Implement Sorting functionality.
            switch (SortKey)
            {
                case "Title":
                    books = books.OrderBy(s => s.Title);
                    break;
                case "Author":
                    books = books.OrderBy(s => s.Author);
                    break;
                case "Price":
                    books = books.OrderBy(s => s.Price);
                    break;
                case "Publisher":
                    books = books.OrderBy(s => s.Publisher);
                    break;
                default:
                    books = books.OrderByDescending(s => s.EntryDateTime);
                    break;
            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(books.ToPagedList(pageNumber, pageSize));
        }
        //End Search Books Mathod

        [Authorize]     //Prevent access from unorthorised user.
        public ActionResult AddBooks()
        {
            //Add book editions
            var editionList = new List<string>();
            editionList.Add("NA");
            for (int i = 0; i < 19; i++)
                editionList.Add(i.ToString());

            ViewBag.editionList = new SelectList(editionList);

            //Retrive all categories from DB
            var categoryList = new List<string>();
            var categoryQry = from cat in db.Categories orderby cat.CategoryName select cat.CategoryName;
            categoryList.AddRange(categoryQry.Distinct());
            ViewBag.bookCategory = new SelectList(categoryList);

            var cityList = new List<string>();                      //Retrive all Cities from DB
            var cityQry = from cit in db.Cities orderby cit.CityName select cit.CityName;
            cityList.AddRange(cityQry.Distinct());                  //Add distinct Cities to view bag object
            ViewBag.bookCity = new SelectList(cityList);

            return View();
        }

        [Authorize]     //To Prevent Access from nont-registerd user
        [HttpPost]
        [ValidateAntiForgeryToken]      //Useing Token Veriﬁcation here to prevent- Threat: Cross-Site Request Forgery
        public ActionResult AddBooks(List<Book> book, string cities, HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3
            , HttpPostedFileBase file4, HttpPostedFileBase file5, HttpPostedFileBase file6, HttpPostedFileBase file7, HttpPostedFileBase file8
            , HttpPostedFileBase file9, HttpPostedFileBase file10)
        {
            //Check for bogus inputs from user
            string[] chkForBougsInput = { "Some Bad Word", "Bogus word"};

            if (book[0].Title == null || book[0].Author == null || book[0].Category == null || book[0].Price == 0 || cities == "" || book[0].Locality == null || book[0].Publisher == null || book[0].Year == null
                || chkForBougsInput.Contains(book[0].Title.ToLower()) || chkForBougsInput.Contains(book[0].Author.ToLower()) || chkForBougsInput.Contains(book[0].Locality.ToLower())
                || chkForBougsInput.Contains(book[0].Publisher.ToLower()) || chkForBougsInput.Contains(book[0].Year.ToLower()))
            {
                return RedirectToAction("AddBooks");
            }
            else
            {
                var filledBook = from b in book where b.Title != null && b.Author != null && b.Category != null && b.Publisher != null && b.Year != null select b;
                ModelState.Clear();
                if (ModelState.IsValid)
                {
                    foreach (Book newBook in filledBook)
                    {
                        newBook.City = cities;
                        newBook.Locality = book[0].Locality;
                        newBook.UserId = WebMatrix.WebData.WebSecurity.CurrentUserId;
                        newBook.IsActive = true;
                        newBook.EntryDateTime = DateTime.Now;

                        if (book[0].Title == newBook.Title)
                        {
                            if (file != null)
                            {
                                /*Geting the file name*/
                                string filename = System.IO.Path.GetFileName(file.FileName);
                                /*Saving the file in server folder*/

                                string fileNameToSaveImgWid = System.Text.RegularExpressions.Regex.Replace((newBook.EntryDateTime.ToString()), @"[^0-9a-zA-Z]+", "").Replace("AM", "").Replace("PM", "").Replace(" ", "");
                                file.SaveAs(Server.MapPath("~/Images/BookImages/" + fileNameToSaveImgWid + ".jpg"));
                                string filepathtosave = "~/Images/BookImages/" + fileNameToSaveImgWid + ".jpg";

                                newBook.State = fileNameToSaveImgWid;
                            }
                            else
                            {
                                newBook.State = "40404";
                            }
                        }
                        else if (book[1].Title == newBook.Title)
                        {
                            if (file2 != null)
                            {
                                /*Geting the file name*/
                                string filename = System.IO.Path.GetFileName(file2.FileName);
                                /*Saving the file in server folder*/

                                string fileNameToSaveImgWid = System.Text.RegularExpressions.Regex.Replace((newBook.EntryDateTime.ToString()), @"[^0-9a-zA-Z]+", "").Replace("AM", "").Replace("PM", "").Replace(" ", "");
                                file2.SaveAs(Server.MapPath("~/Images/BookImages/" + fileNameToSaveImgWid + ".jpg"));
                                string filepathtosave = "Images/BookImages/" + fileNameToSaveImgWid + ".jpg";

                                newBook.State = fileNameToSaveImgWid;
                            }
                            else
                            {
                                newBook.State = "40404";
                            }
                        }
                        else if (book[2].Title == newBook.Title)
                        {
                            if (file3 != null)
                            {
                                /*Geting the file name*/
                                string filename = System.IO.Path.GetFileName(file3.FileName);
                                /*Saving the file in server folder*/

                                string fileNameToSaveImgWid = System.Text.RegularExpressions.Regex.Replace((newBook.EntryDateTime.ToString()), @"[^0-9a-zA-Z]+", "").Replace("AM", "").Replace("PM", "").Replace(" ", "");
                                file3.SaveAs(Server.MapPath("~/Images/BookImages/" + fileNameToSaveImgWid + ".jpg"));
                                string filepathtosave = "Images/BookImages/" + fileNameToSaveImgWid + ".jpg";

                                newBook.State = fileNameToSaveImgWid;
                            }
                            else
                            {
                                newBook.State = "40404";
                            }
                        }
                        else if (book[3].Title == newBook.Title)
                        {
                            if (file4 != null)
                            {
                                /*Geting the file name*/
                                string filename = System.IO.Path.GetFileName(file4.FileName);
                                /*Saving the file in server folder*/

                                string fileNameToSaveImgWid = System.Text.RegularExpressions.Regex.Replace((newBook.EntryDateTime.ToString()), @"[^0-9a-zA-Z]+", "").Replace("AM", "").Replace("PM", "").Replace(" ", "");
                                file4.SaveAs(Server.MapPath("~/Images/BookImages/" + fileNameToSaveImgWid + ".jpg"));
                                string filepathtosave = "Images/BookImages/" + fileNameToSaveImgWid + ".jpg";

                                newBook.State = fileNameToSaveImgWid;
                            }
                            else
                            {
                                newBook.State = "40404";
                            }
                        }
                        else if (book[4].Title == newBook.Title)
                        {
                            if (file5 != null)
                            {
                                /*Geting the file name*/
                                string filename = System.IO.Path.GetFileName(file5.FileName);
                                /*Saving the file in server folder*/

                                string fileNameToSaveImgWid = System.Text.RegularExpressions.Regex.Replace((newBook.EntryDateTime.ToString()), @"[^0-9a-zA-Z]+", "").Replace("AM", "").Replace("PM", "").Replace(" ", "");
                                file5.SaveAs(Server.MapPath("~/Images/BookImages/" + fileNameToSaveImgWid + ".jpg"));
                                string filepathtosave = "Images/BookImages/" + fileNameToSaveImgWid + ".jpg";

                                newBook.State = fileNameToSaveImgWid;
                            }
                            else
                            {
                                newBook.State = "40404";
                            }
                        }
                        else if (book[5].Title == newBook.Title)
                        {
                            if (file6 != null)
                            {
                                /*Geting the file name*/
                                string filename = System.IO.Path.GetFileName(file6.FileName);
                                /*Saving the file in server folder*/

                                string fileNameToSaveImgWid = System.Text.RegularExpressions.Regex.Replace((newBook.EntryDateTime.ToString()), @"[^0-9a-zA-Z]+", "").Replace("AM", "").Replace("PM", "").Replace(" ", "");
                                file6.SaveAs(Server.MapPath("~/Images/BookImages/" + fileNameToSaveImgWid + ".jpg"));
                                string filepathtosave = "Images/BookImages/" + fileNameToSaveImgWid + ".jpg";

                                newBook.State = fileNameToSaveImgWid;
                            }
                            else
                            {
                                newBook.State = "40404";
                            }
                        }
                        else if (book[6].Title == newBook.Title)
                        {
                            if (file7 != null)
                            {
                                /*Geting the file name*/
                                string filename = System.IO.Path.GetFileName(file7.FileName);
                                /*Saving the file in server folder*/

                                string fileNameToSaveImgWid = System.Text.RegularExpressions.Regex.Replace((newBook.EntryDateTime.ToString()), @"[^0-9a-zA-Z]+", "").Replace("AM", "").Replace("PM", "").Replace(" ", "");
                                file7.SaveAs(Server.MapPath("~/Images/BookImages/" + fileNameToSaveImgWid + ".jpg"));
                                string filepathtosave = "Images/BookImages/" + fileNameToSaveImgWid + ".jpg";

                                newBook.State = fileNameToSaveImgWid;
                            }
                            else
                            {
                                newBook.State = "40404";
                            }
                        }
                        else if (book[7].Title == newBook.Title)
                        {
                            if (file8 != null)
                            {
                                /*Geting the file name*/
                                string filename = System.IO.Path.GetFileName(file8.FileName);
                                /*Saving the file in server folder*/

                                string fileNameToSaveImgWid = System.Text.RegularExpressions.Regex.Replace((newBook.EntryDateTime.ToString()), @"[^0-9a-zA-Z]+", "").Replace("AM", "").Replace("PM", "").Replace(" ", "");
                                file8.SaveAs(Server.MapPath("~/Images/BookImages/" + fileNameToSaveImgWid + ".jpg"));
                                string filepathtosave = "Images/BookImages/" + fileNameToSaveImgWid + ".jpg";

                                newBook.State = fileNameToSaveImgWid;
                            }
                            else
                            {
                                newBook.State = "40404";
                            }
                        }
                        else if (book[8].Title == newBook.Title)
                        {
                            if (file9 != null)
                            {
                                /*Geting the file name*/
                                string filename = System.IO.Path.GetFileName(file9.FileName);
                                /*Saving the file in server folder*/

                                string fileNameToSaveImgWid = System.Text.RegularExpressions.Regex.Replace((newBook.EntryDateTime.ToString()), @"[^0-9a-zA-Z]+", "").Replace("AM", "").Replace("PM", "").Replace(" ", "");
                                file9.SaveAs(Server.MapPath("~/Images/BookImages/" + fileNameToSaveImgWid + ".jpg"));
                                string filepathtosave = "Images/BookImages/" + fileNameToSaveImgWid + ".jpg";

                                newBook.State = fileNameToSaveImgWid;
                            }
                            else
                            {
                                newBook.State = "40404";
                            }
                        }
                        else if (book[9].Title == newBook.Title)
                        {
                            if (file10 != null)
                            {
                                /*Geting the file name*/
                                string filename = System.IO.Path.GetFileName(file10.FileName);
                                /*Saving the file in server folder*/

                                string fileNameToSaveImgWid = System.Text.RegularExpressions.Regex.Replace((newBook.EntryDateTime.ToString()), @"[^0-9a-zA-Z]+", "").Replace("AM", "").Replace("PM", "").Replace(" ", "");
                                file10.SaveAs(Server.MapPath("~/Images/BookImages/" + fileNameToSaveImgWid + ".jpg"));
                                string filepathtosave = "Images/BookImages/" + fileNameToSaveImgWid + ".jpg";

                                newBook.State = fileNameToSaveImgWid;
                            }
                            else
                            {
                                newBook.State = "40404";
                            }
                        }
                        db.Books.Add(newBook);
                    }
                    db.SaveChanges();

                    //Notify admin(send an email when user add any book)
                    //SendMailNotifyAdminAboutAddedBooks(book,User.Identity.Name);

                    return RedirectToAction("SearchBooks");
                }
            }
            return RedirectToAction("SearchBooks");
        }

        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            //Retrive all categories from DB
            var editionList = new List<string>();
            for (int i = 0; i < 19; i++)
                editionList.Add(i.ToString());

            ViewBag.editionList = new SelectList(editionList);

            //Retrive all categories from DB
            var categoryList = new List<string>();
            var categoryQry = from cat in db.Categories orderby cat.CategoryName select cat.CategoryName;
            categoryList.AddRange(categoryQry.Distinct());
            ViewBag.bookCategory = new SelectList(categoryList);

            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound("No Book Found to Edit");
            }
            return View(book);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]      //To prevent from CSRF attacket -Toket Validation here.
        public ActionResult Edit(Book book, int BookID)
        {
            if (ModelState.IsValid)
            {
                //Bind value Bind attribute do not specify to pass BookID, UserID and Is Active from model valuse,
                //thats wahy we are passing BookID, useId and IsActive value manually from here.
                book.UserId = WebSecurity.CurrentUserId;
                book.BookID = BookID;
                book.IsActive = true;

                book.EntryDateTime = DateTime.Now;
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("MyAccount", "Account");
        }

        //GET
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            book.IsActive = false;
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("MyAccount", "Account");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public void SendMailNotifyAdminAboutAddedBooks(List<Book> book, string userName)
        {
            string formattedBody = null;
            foreach (var bookAvbl in book)
            {
                if (bookAvbl != null || bookAvbl != null || bookAvbl != null)
                {
                    formattedBody += "<table><tr><th>Title</th><th>Author</th><th>Category</th><th>Price</th><th>Locality</th><th>Publisher</th><th>Year</th></tr>" +

                       "<tr><td>" + bookAvbl.Title + "</td><td>" + bookAvbl.Author + "</td><td>" + bookAvbl.Category + "</td><td>" + bookAvbl.Price + "</td>" +
                   "<td>" + bookAvbl.Locality + "</td><td>" + bookAvbl.Publisher + "</td><td>" + bookAvbl.Year + "</td></tr>" +

                   "</table>";
                }
            }

            //Send Mail Logic Here. 
            string email = ConfigurationSettings.AppSettings["HosterEmailAddress"];
            string password = ConfigurationSettings.AppSettings["HosterEmailPassword"];
            string host = ConfigurationSettings.AppSettings["HosterEmailHost"];
            int port = Convert.ToInt16(ConfigurationSettings.AppSettings["HosterEmailPort"]);
            string emailSubject = "Notification! Book(s) uploaded on BookDesk by USER- " + userName;

            string emailsToNotify = ConfigurationSettings.AppSettings["adminEmailID"];

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient(host, port);

            msg.From = new MailAddress(email);
            msg.To.Add(emailsToNotify);
            msg.Subject = emailSubject; //"Notification! from Book Desk - Sell Your Old Book";
            msg.Body = formattedBody;
            msg.IsBodyHtml = true;

            //TRACE- Set to False When Deploy
            smtpClient.EnableSsl = false;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }
    }
}
