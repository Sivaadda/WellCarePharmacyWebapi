using System.ComponentModel.DataAnnotations;
using WellCarePharmacyWebapi.Models.Entities.Base;

namespace WellCarePharmacyWebapi.Models.Entities
{
    public class Orders:BaseEntity
    {
        [Required]
        public virtual Users Users { get; set; }

        [Required]
        public virtual ICollection<Products> Products { get; set; }
                
        [Required]
        public int Quantity { get; set; }

        [Required]
        [Range(18,2)]
        public decimal TotalPrice { get; set; }

    }
}
