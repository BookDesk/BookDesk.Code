using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace BuySellOldBooks.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("PustakContext")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        //[StringLength(16, MinimumLength = 4, ErrorMessage = "User Name must be a string with a minimum length of 4 and a maximum length of 15.")]
        [RegularExpression(@"^[a-zA-Z]\w{4,19}$", ErrorMessage = "User Name only allows a-z, A-Z, 0-9, and a minimum length of 5 and a maximum length of 20.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]\w{4,19}$", ErrorMessage = "User Name only allows a-z, A-Z, 0-9, and a minimum length of 5 and a maximum length of 20.")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        //^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$
        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter a valid Email address.")]  //TRACE
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }
        

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]\w{4,19}$", ErrorMessage = "Nick Name only allows a-z, A-Z, 0-9, and a minimum length of 5 and a maximum length of 20.")]
        [Display(Name = "Nick Name")]
        public string NickName { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter valid phone number (Do not include 0).")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        //[Required]
        //[RegularExpression(@"^[a-zA-Z]\w{4,39}$", ErrorMessage = "Institution Name only allows a-z, A-Z, 0-9, and a minimum length of 5 and a maximum length of 40.")]
        [Display(Name = "Institution Name")]
        public string Institution { get; set; }     
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
