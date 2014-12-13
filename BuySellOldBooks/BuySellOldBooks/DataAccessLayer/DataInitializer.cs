using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BuySellOldBooks.Models;

namespace BuySellOldBooks.DataAccessLayer
{
    public class DataInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PustakContext>
    {
        //CREENTLY DATA IS SEEDING FROM cONFIGURATION.CS FILE USE mIGRATION FOLDER.

        protected override void Seed(PustakContext context)
        {
            //var books = new List<Book>
            //{
            ////new Book{BookID="444", UserId="11",Title="JAVA", Author="SUN Microsystem", Edition=7, Category="Computers", Locality="Tilak Nagar", City="New Delhi",
            ////    State="Delhi", Price=300, Review="Average", IsActive=true, EntryDateTime=DateTime.Parse("2005-09-01")}
            //    //new Book{BookID="222",Title="Indian Constitution", Author="Bhimrav Ambedkar", Edition=1, Category="Arts", Locality="Pared", City="Kanpur",
            //    //State="Uttar Pradesh", Price=900, Review="Good", IsActive=true, EntryDateTime=DateTime.Parse("2005-09-01")},
            //    //new Book{BookID="999",Title="Let Us C", Author="Yashvant Kanitkar", Edition=5, Category="Computers", Locality="Uppa Nagar", City="Banglore",
            //    //State="Karnatka", Price=120, Review="Good", IsActive=true, EntryDateTime=DateTime.Parse("2005-09-01")},
            //    //new Book{BookID="777",Title="Morel Rights", Author="Ghandi Ji", Edition=2, Category="Arts", Locality="Pared", City="Kanpur",
            //    //State="Uttar Pradesh", Price=20, Review="Good", IsActive=true, EntryDateTime=DateTime.Parse("2005-09-01")},
            //    //new Book{BookID="666",Title="MVC in .Net", Author="Bill Gates", Edition=1, Category="Computers", Locality="Sector-63", City="Gurgaon",
            //    //State="Haryana", Price=400, Review="Good", IsActive=true, EntryDateTime=DateTime.Parse("2005-09-01")}
            
            //};

            //books.ForEach(s => context.Books.Add(s));
            //context.SaveChanges();
            ////var courses = new List<Course>
            ////{
            ////new Course{CourseID=1050,Title="Chemistry",Credits=3,},
            ////new Course{CourseID=4022,Title="Microeconomics",Credits=3,},
            ////new Course{CourseID=4041,Title="Macroeconomics",Credits=3,},
            ////new Course{CourseID=1045,Title="Calculus",Credits=4,},
            ////new Course{CourseID=3141,Title="Trigonometry",Credits=4,},
            ////new Course{CourseID=2021,Title="Composition",Credits=3,},
            ////new Course{CourseID=2042,Title="Literature",Credits=4,}
            ////};
            ////courses.ForEach(s => context.Courses.Add(s));
            ////context.SaveChanges();
            ////var enrollments = new List<Enrollment>
            ////{
            ////new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            ////new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            ////new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            ////new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            ////new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            ////new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            ////new Enrollment{StudentID=3,CourseID=1050},
            ////new Enrollment{StudentID=4,CourseID=1050,},
            ////new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            ////new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            ////new Enrollment{StudentID=6,CourseID=1045},
            ////new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            ////};
            ////enrollments.ForEach(s => context.Enrollments.Add(s));
            ////context.SaveChanges();
        }
    }
}