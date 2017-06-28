using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace sqlexpressmvc.Models
{
    public class RateList
    {
        public RateList()
        {
            this.Billings = new HashSet<Billing>();
        }
        [Key]
        public int RateListID { get; set; }
       
        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        [Required]
        public float price { get; set; }
        
        [Display(Name = "Added on")]
        [DataType(DataType.DateTime)]
        public DateTime? dateAdded { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Modified on")]
        [DataType(DataType.DateTime)]
        public DateTime? dateModified { get; set; }

        public virtual ICollection<Billing> Billings { get; set; }
        public virtual String FullName { get { return Title + "- [" + price+"]"; } }
    }
}