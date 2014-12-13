using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuySellOldBooks.Models
{
    public class Message
    {
        //The MessageID property will become the primary key column of the database table that corresponds to this class. 
        //By default, the Entity Framework interprets a property that's named ID or classnameID as the primary key.
        public string MessageID { get; set; }

        //Unlike the User.Messages navigation property you saw earlier, which can hold multiple Message entities, A Message entity is associated with one User entity, so the property can only hold a single User entity
        /// <summary>
        /// The UserId property is a foreign key, and the corresponding navigation property is User. 
        /// A Message entity is associated with one User entity, so the property can only hold a single User entity
        /// </summary>
        public int UserId { get; set; }

        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool     IsActive { get; set; }
        public DateTime EntryDateTime { get; set; }

        /// <summary>
        /// The User property of a Message entity can only hold a single User entity because A Message entity is associated with one User entity.
        /// </summary>
        public virtual ICollection<User> User { get; set; }
    }
}