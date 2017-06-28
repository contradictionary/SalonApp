using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sqlexpressmvc.Models
{
    public class Stock
    {
        [Key]
        public int StockID { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Display(Name = "Cost")]
        [DataType(DataType.Currency)]
        [Required]
        public float cost { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        [Required]
        public float price { get; set; }

        [Display(Name = "Quantity")]        
        [Required(AllowEmptyStrings =false)]
        public int qty { get; set; }

        [Display(Name = "Volume")]
        [Required(AllowEmptyStrings = false)]
        public int volume { get; set; }

        [Display(Name = "Added on")]
        [DataType(DataType.DateTime)]
        public DateTime? dateAdded { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Modified on")]
        [DataType(DataType.DateTime)]
        public DateTime? dateModified { get; set; }

        public virtual string FullName { get { return "Name : " + Title + ", Price : " + price; } }
    }
}