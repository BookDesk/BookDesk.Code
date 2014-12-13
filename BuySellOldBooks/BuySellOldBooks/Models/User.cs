using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuySellOldBooks.Models
{
    //Threat: Over-Posting
    //Preventing Over-Posting with the Bind Attribut
    //Bind attribute Specify here that how many properties(columns) can a user bind from, it prevent us from Model over posting sequrity flaw.
    [Bind(Include = "Password, NickName, Phone, Email, Institution")]
    public class User
    {
        //The UserId property will become the primary key column of the database table that corresponds to this class. 
        //By default, the Entity Framework interprets a property that's named ID or classnameID as the primary key.
        [Key]
        public int UserId { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]\w{4,19}$", ErrorMessage = "Nick Name only allows a-z, A-Z, 0-9, and a minimum length of 5 and a maximum length of 20.")]
        [Display(Name = "Nick Name")]
        public string   NickName { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter valid phone number (Do not include 0).")]
        public string   Phone { get; set; }

        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter a valid Email address.")]
        public string   Email { get; set; }
        public string   Institution { get; set; }
        public bool     IsActive { get; set; }
        public DateTime EntryDateTime { get; set; }

        /// <summary>
        /// The Books property is a navigation property. Navigation properties hold other entities that are rel ated to this entity. 
        /// In this case, the Books property of a User entity will hold all of the Book entities that are related to that User entity. 
        /// In other words, if a given User row in the database has two related Book rows (rows that contain that User's primary key value in their 
        /// UserId foreign key column), that User entity's Books navigation property will contain those two Book entities.
        /// </summary>
        public virtual ICollection<Book> Books { get; set; }    //Navigation properties are typically defined as virtual so that they can take advantage of certain Entity Framework functionality such as lazy loading.
        /// <summary>
        /// The Messages property of a User entity will hold all of the Message entities that are related to that User entity. 
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; }
    }
}