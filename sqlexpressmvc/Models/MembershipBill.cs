using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sqlexpressmvc.Models
{
    public class MembershipBill
    {
        [Key]
        public int MembershipBillID { get; set; }

        [Required]
        [Display(Name ="Member")]
        public int MemberID { get; set; }
        public virtual Member Member{ get; set; }

        [Required]
        [Display(Name ="Employee")]
        public int EmployeesID { get; set; }
        public virtual Employees Employee { get; set; }
        
        [DataType(DataType.Currency)]
        public float? Amount { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name ="Bill Date")]
        public DateTime? datesold { get; set; }
              

    }
}