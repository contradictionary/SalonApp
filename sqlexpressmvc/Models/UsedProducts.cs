using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sqlexpressmvc.Models
{
    public class UsedProducts
    {
     
        [Key]

        public int UsedProductsID { get; set; }

        [Required]

        public int StockID { get; set; }
        public virtual Stock Stock { get; set; }

        /// <summary>
        /// Validation Disabled as by defaul only qty will be allowed to be used.
        /// </summary>
       // [Remote("ValidateNewQty", "UsedProducts",AdditionalFields ="StockID",ErrorMessage ="Not Valid")]
        public int Usedqty { get; set; }

        [Required]
        public int EmployeesID { get; set; }
        public virtual Employees Employeee { get; set; }


        public int Usedvolume { get; set; }

        public DateTime? dateAdded { get; set; }
        public DateTime? dateModified { get; set; }

    }
}