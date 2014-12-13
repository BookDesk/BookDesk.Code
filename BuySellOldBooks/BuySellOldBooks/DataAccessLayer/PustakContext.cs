using System;
using BuySellOldBooks.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BuySellOldBooks.DataAccessLayer
{
    //Main class that coordinates Entity Framework functionality for a given data model  is Database Context class.
    //and we are specifying here to which entities to include in data models.
    public class PustakContext : DbContext
    {
        public PustakContext()
            : base("PustakContext")
        {
        }
  
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        /// <summary>
        /// The modelBuilder.Conventions.Remove statement in the OnModelCreating method prevents table names from being pluralized. 
        /// If you didn't do this, the generated tables would be named Books, Users, and Messages. Instead, the table names will be 
        /// Book, User, and Message. Developers disagree about whether table names should be pluralized or not. But we uses the singular form,
        /// the important point is that you can select whichever form you prefer by including or omitting this line of code.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}