using System;
using System.ComponentModel.DataAnnotations;

namespace sqlexpressmvc.Models
{
    public class UpdateVolume
    {
        [Key]
        public int UpdateVolumeID { get; set; }

        [Required]
        public int EmployeesID { get; set; }
        public virtual Employees Employee { get; set; }

        [Required]
        public int UsedProductsID { get; set; }
        public virtual UsedProducts UsedProducts { get; set; }

        [Required]
        public int newVolume { get; set; }
        
        [ScaffoldColumn(false)]
        public DateTime dateUpdated { get; set; }

    }
}