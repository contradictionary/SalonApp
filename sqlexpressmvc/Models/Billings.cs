using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sqlexpressmvc.Models
{
    public class Billing
    {
        public Billing()
        {
            this.Services = new HashSet<RateList>();
        }
        
        [Key]
        public int BillingsID { get; set; }
        [Required]
        public string  Name { get; set; }

        //public float Discount { get; set; }
        [Required]
        public int EmployeesID { get; set; }

        public virtual Employees Employeee { get; set; }

        //public DiscountType DiscountType { get; set; }

        [DataType(DataType.Currency)]
        public float? Amount { get; set; }
        [Required]
        public virtual ICollection<RateList> Services { get; set; }

        //[ScaffoldColumn(false)]
        //public DateTime dateSold { get; set; }

    }

    public enum DiscountType
    {
        PERCENT,AMOUNT
    }
}