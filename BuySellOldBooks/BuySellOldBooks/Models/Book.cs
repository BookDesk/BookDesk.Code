using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BuySellOldBooks.Models
{
    //Threat: Over-Posting
    //Preventing Over-Posting with the Bind Attribut
    //Bind attribute Specify here that how many properties(columns) can a user bind from, it prevent us from Model over posting sequrity flaw.
    [Bind(Include = "Title, Publisher, Author, Edition, Year, Category, Locality, City, State, Price")]
    public class Book
    {
        //The BookID property will become the primary key column of the database table that corresponds to this class. 
        //By default, the Entity Framework interprets a property that's named ID or classnameID as the primary key.
        
        public int BookID { get; set; }

        /// <summary>
        /// The UserId property is a foreign key, and the corresponding navigation property is User. 
        /// A Book entity is associated with one User entity, so the property can only hold a single User entity
        /// </summary>
        public int UserId { get; set; }

        //"/[^\s]*@[a-z0-9.-]*/i"
        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string   Title { get; set; }

        //TRACE
        //Add a new field Publisher
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Publisher { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string   Author { get; set; }

        [Required]
        public string      Edition { get; set; }

        //TRACE
        //Add a new field Year
        [Required]
        [StringLength(4, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Year { get; set; }

        [Required]
        public string   Category { get; set; }

        //[Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string   Locality { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string   City { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string State { get; set; }

        [Required]
        ////[RegularExpression(@"^\d$", ErrorMessage = "Please enter valid Price.")]
        //[StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public int      Price { get; set; }

        //[Required]
        //[StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string   Review { get; set; }

        //[Required]
        public bool     IsActive { get; set; }
        //[Required]
        public DateTime EntryDateTime { get; set; }

        /// <summary>
        /// The User property of a Book entity can only hold a single User entity because A Book entity is associated with one User entity.
        /// </summary>
        public virtual ICollection<User> User { get; set; }
    }
}