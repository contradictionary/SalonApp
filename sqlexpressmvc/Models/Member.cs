using System;
using System.ComponentModel.DataAnnotations;

namespace sqlexpressmvc.Models
{
    public class Member
    {
        [Key]

        public int MemberID { get; set; }

        [Display(Name = "Membership ID")]
        public int MembershipID { get; set; }

        [Display(Name = "First Name")]
        public string fname { get; set; }

        [Display(Name = "Last Name")]
        public string lname { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Enter A Valid Mobile Number!")]
        [RegularExpression("^[789]\\d{9}$", ErrorMessage = "Enter A Valid Indian Mobile Number!")]
        public string mobile { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public string dob { get; set; }

        [Display(Name = "Anniversary")]
        [DataType(DataType.Date)]
        public string anniversary { get; set; }

        [Display(Name = "E-mail Address")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        //[Display(Name = "Address")]
        //[DataType(DataType.MultilineText)]
        //public string address { get; set; }

        [Display(Name = "Membership Expire")]
        [DataType(DataType.DateTime)]
        public DateTime? mexpirydate { get; set; }

       
    
        [Display(Name = "Added on")]
        [DataType(DataType.DateTime)]
        public DateTime? dateAdded { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Modified on")]
        [DataType(DataType.DateTime)]
        public DateTime? dateModified { get; set; }

       public virtual string FullName { get { return "[" + MembershipID + "] - " + fname + " " + lname; } }
    }
}